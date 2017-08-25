using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImGuiNET;
using System.IO;
using System.Runtime.InteropServices;

namespace ImGuiSDL2CS {
    public static class ImGuiSDL2CSHelper {

        private static bool _Initialized = false;
        public static bool Initialized => _Initialized;

        public static void Init() {
            if (_Initialized)
                return;
            _Initialized = true;

            ImGuiIO io = ImGui.GetIO();

            io.KeyMap[ImGuiKey.Tab] = (int) SDL.SDL_Keycode.SDLK_TAB;
            io.KeyMap[ImGuiKey.LeftArrow] = (int) SDL.SDL_Scancode.SDL_SCANCODE_LEFT;
            io.KeyMap[ImGuiKey.RightArrow] = (int) SDL.SDL_Scancode.SDL_SCANCODE_RIGHT;
            io.KeyMap[ImGuiKey.UpArrow] = (int) SDL.SDL_Scancode.SDL_SCANCODE_UP;
            io.KeyMap[ImGuiKey.DownArrow] = (int) SDL.SDL_Scancode.SDL_SCANCODE_DOWN;
            io.KeyMap[ImGuiKey.PageUp] = (int) SDL.SDL_Scancode.SDL_SCANCODE_PAGEUP;
            io.KeyMap[ImGuiKey.PageDown] = (int) SDL.SDL_Scancode.SDL_SCANCODE_PAGEDOWN;
            io.KeyMap[ImGuiKey.Home] = (int) SDL.SDL_Scancode.SDL_SCANCODE_HOME;
            io.KeyMap[ImGuiKey.End] = (int) SDL.SDL_Scancode.SDL_SCANCODE_END;
            io.KeyMap[ImGuiKey.Delete] = (int) SDL.SDL_Keycode.SDLK_DELETE;
            io.KeyMap[ImGuiKey.Backspace] = (int) SDL.SDL_Keycode.SDLK_BACKSPACE;
            io.KeyMap[ImGuiKey.Enter] = (int) SDL.SDL_Keycode.SDLK_RETURN;
            io.KeyMap[ImGuiKey.Escape] = (int) SDL.SDL_Keycode.SDLK_ESCAPE;
            io.KeyMap[ImGuiKey.A] = (int) SDL.SDL_Keycode.SDLK_a;
            io.KeyMap[ImGuiKey.C] = (int) SDL.SDL_Keycode.SDLK_c;
            io.KeyMap[ImGuiKey.V] = (int) SDL.SDL_Keycode.SDLK_v;
            io.KeyMap[ImGuiKey.X] = (int) SDL.SDL_Keycode.SDLK_x;
            io.KeyMap[ImGuiKey.Y] = (int) SDL.SDL_Keycode.SDLK_y;
            io.KeyMap[ImGuiKey.Z] = (int) SDL.SDL_Keycode.SDLK_z;

            io.SetGetClipboardTextFn((userData) => SDL.SDL_GetClipboardText());
            io.SetSetClipboardTextFn((userData, text) => SDL.SDL_SetClipboardText(text));

            // If no font added, add default font.
            if (io.FontAtlas.Fonts.Size == 0)
                io.FontAtlas.AddDefaultFont();
        }

        /*
        public static int OnTextEdited(ImGuiTextEditCallbackData* data) {
            char currentEventChar = (char) data->EventChar;
            return 0;
        }
        */

        public static void NewFrame(ImVec2 size, ImVec2 scale, ImVec2 mousePosition, uint mouseMask, ref float mouseWheel, bool[] mousePressed, ref double g_Time) {
            ImGuiIO io = ImGui.GetIO();
            io.DisplaySize = size;
            io.DisplayFramebufferScale = scale;

            double currentTime = SDL.SDL_GetTicks() / 1000D;
            io.DeltaTime = g_Time > 0D ? (float) (currentTime - g_Time) : (1f/60f);
            g_Time = currentTime;

            io.MousePosition = mousePosition;

            io.MouseDown[0] = mousePressed[0] || (mouseMask & SDL.SDL_BUTTON(SDL.SDL_BUTTON_LEFT)) != 0;
            io.MouseDown[1] = mousePressed[1] || (mouseMask & SDL.SDL_BUTTON(SDL.SDL_BUTTON_RIGHT)) != 0;
            io.MouseDown[2] = mousePressed[2] || (mouseMask & SDL.SDL_BUTTON(SDL.SDL_BUTTON_MIDDLE)) != 0;
            mousePressed[0] = mousePressed[1] = mousePressed[2] = false;

            io.MouseWheel = mouseWheel;
            mouseWheel = 0f;

            SDL.SDL_ShowCursor(io.MouseDrawCursor ? 0 : 1);

            ImGui.NewFrame();
        }

        public static void Render(ImVec2 size) {
            ImGui.Render();
            if (ImGui.IO.RenderDrawListsFn == IntPtr.Zero)
                RenderDrawData(ImGui.GetDrawData(), (int) Math.Round(size.X), (int) Math.Round(size.Y));
        }
        
        public static void RenderDrawData(ImDrawData drawData, int displayW, int displayH) {
            // We are using the OpenGL fixed pipeline to make the example code simpler to read!
            // Setup render state: alpha-blending enabled, no face culling, no depth testing, scissor enabled, vertex/texcoord/color pointers.
            int lastTexture; GL.GetIntegerv(GL.Enum.GL_TEXTURE_BINDING_2D, out lastTexture);
            Int4 lastViewport; GL.GetIntegerv4(GL.Enum.GL_VIEWPORT, out lastViewport);
            Int4 lastScissorBox; GL.GetIntegerv4(GL.Enum.GL_SCISSOR_BOX, out lastScissorBox);

            GL.PushAttrib(GL.Enum.GL_ENABLE_BIT | GL.Enum.GL_COLOR_BUFFER_BIT | GL.Enum.GL_TRANSFORM_BIT);
            GL.Enable(GL.Enum.GL_BLEND);
            GL.BlendFunc(GL.Enum.GL_SRC_ALPHA, GL.Enum.GL_ONE_MINUS_SRC_ALPHA);
            GL.Disable(GL.Enum.GL_CULL_FACE);
            GL.Disable(GL.Enum.GL_DEPTH_TEST);
            GL.Enable(GL.Enum.GL_SCISSOR_TEST);
            GL.EnableClientState(GL.Enum.GL_VERTEX_ARRAY);
            GL.EnableClientState(GL.Enum.GL_TEXTURE_COORD_ARRAY);
            GL.EnableClientState(GL.Enum.GL_COLOR_ARRAY);
            GL.Enable(GL.Enum.GL_TEXTURE_2D);

            GL.UseProgram(0);

            // Handle cases of screen coordinates != from framebuffer coordinates (e.g. retina displays)
            ImGuiIO io = ImGui.GetIO();
            ImGui.ScaleClipRects(drawData, io.DisplayFramebufferScale);

            // Setup orthographic projection matrix
            GL.Viewport(0, 0, displayW, displayH);
            GL.MatrixMode(GL.Enum.GL_PROJECTION);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho(
                0.0f,
                io.DisplaySize.X / io.DisplayFramebufferScale.X,
                io.DisplaySize.Y / io.DisplayFramebufferScale.Y,
                0.0f,
                -1.0f,
                1.0f
            );
            GL.MatrixMode(GL.Enum.GL_MODELVIEW);
            GL.PushMatrix();
            GL.LoadIdentity();

            // Render command lists

            for (int n = 0; n < drawData.CmdListsCount; n++) {
                ImDrawList cmdList = drawData[n];
                ImVector<ImDrawVert> vtxBuffer = cmdList.VtxBuffer;
                ImVector<ushort> idxBuffer = cmdList.IdxBuffer;

                GL.VertexPointer(2, GL.Enum.GL_FLOAT, ImDrawVert.Size, new IntPtr((long) vtxBuffer.Data + ImDrawVert.PosOffset));
                GL.TexCoordPointer(2, GL.Enum.GL_FLOAT, ImDrawVert.Size, new IntPtr((long) vtxBuffer.Data + ImDrawVert.UVOffset));
                GL.ColorPointer(4, GL.Enum.GL_UNSIGNED_BYTE, ImDrawVert.Size, new IntPtr((long) vtxBuffer.Data + ImDrawVert.ColOffset));

                long idxBufferOffset = 0;
                for (int cmdi = 0; cmdi < cmdList.CmdBuffer.Size; cmdi++) {
                    ImDrawCmd pcmd = cmdList.CmdBuffer[cmdi];
                    if (pcmd.UserCallback != IntPtr.Zero) {
                        pcmd.InvokeUserCallback(ref cmdList, ref pcmd);
                    } else {
                        GL.BindTexture(GL.Enum.GL_TEXTURE_2D, (int) pcmd.TextureId);
                        GL.Scissor(
                            (int) pcmd.ClipRect.X,
                            (int) (io.DisplaySize.Y - pcmd.ClipRect.W),
                            (int) (pcmd.ClipRect.Z - pcmd.ClipRect.X),
                            (int) (pcmd.ClipRect.W - pcmd.ClipRect.Y)
                        );
                        GL.DrawElements(GL.Enum.GL_TRIANGLES, (int) pcmd.ElemCount, GL.Enum.GL_UNSIGNED_SHORT, new IntPtr((long) idxBuffer.Data + idxBufferOffset));
                    }
                    idxBufferOffset += pcmd.ElemCount * 2 /*sizeof(ushort)*/;
                }
            }

            // Restore modified state
            GL.DisableClientState(GL.Enum.GL_COLOR_ARRAY);
            GL.DisableClientState(GL.Enum.GL_TEXTURE_COORD_ARRAY);
            GL.DisableClientState(GL.Enum.GL_VERTEX_ARRAY);
            GL.BindTexture(GL.Enum.GL_TEXTURE_2D, lastTexture);
            GL.MatrixMode(GL.Enum.GL_MODELVIEW);
            GL.PopMatrix();
            GL.MatrixMode(GL.Enum.GL_PROJECTION);
            GL.PopMatrix();
            GL.PopAttrib();
            GL.Viewport(lastViewport.X, lastViewport.Y, lastViewport.Z, lastViewport.W);
            GL.Scissor(lastScissorBox.X, lastScissorBox.Y, lastScissorBox.Z, lastScissorBox.W);
        }

        public static bool OnEvent(SDL.SDL_Event e, ref float mouseWheel, bool[] mousePressed) {
            ImGuiIO io = ImGui.GetIO();
            switch (e.type) {
                case SDL.SDL_EventType.SDL_MOUSEWHEEL:
                    if (e.wheel.y > 0)
                        mouseWheel = 1;
                    if (e.wheel.y < 0)
                        mouseWheel = -1;
                    return true;
                case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    if (mousePressed == null)
                        return true;
                    if (e.button.button == SDL.SDL_BUTTON_LEFT && mousePressed.Length > 0)
                        mousePressed[0] = true;
                    if (e.button.button == SDL.SDL_BUTTON_RIGHT && mousePressed.Length > 1)
                        mousePressed[1] = true;
                    if (e.button.button == SDL.SDL_BUTTON_MIDDLE && mousePressed.Length > 2)
                        mousePressed[2] = true;
                    return true;
                case SDL.SDL_EventType.SDL_TEXTINPUT:
                    unsafe
                    {
                        // THIS IS THE ONLY UNSAFE THING LEFT!
                        ImGui.AddInputCharactersUTF8(e.text.text);
                    }
                    return true;
                case SDL.SDL_EventType.SDL_KEYDOWN:
                case SDL.SDL_EventType.SDL_KEYUP:
                    int key = (int) e.key.keysym.sym & ~SDL.SDLK_SCANCODE_MASK;
                    io.KeysDown[key] = e.type == SDL.SDL_EventType.SDL_KEYDOWN;
                    SDL.SDL_Keymod keyModState = SDL.SDL_GetModState();
                    io.ShiftPressed = (keyModState & SDL.SDL_Keymod.KMOD_SHIFT) != 0;
                    io.CtrlPressed = (keyModState & SDL.SDL_Keymod.KMOD_CTRL) != 0;
                    io.AltPressed = (keyModState & SDL.SDL_Keymod.KMOD_ALT) != 0;
                    io.SuperPressed = (keyModState & SDL.SDL_Keymod.KMOD_GUI) != 0;
                    return true;
            }

            return true;
        }

    }
}
