using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using ImGuiNET;
using System.IO;
using System.Runtime.InteropServices;

namespace ImGuiSDL2CS {
    public class ImGuiSDL2CSWindow : SDL2Window {

        protected readonly bool _IsSuperClass;

        protected double g_Time = 0.0f;
        protected readonly bool[] g_MousePressed = { false, false, false };
        protected float g_MouseWheel = 0.0f;
        protected int g_FontTexture = 0;

        public ImVec2 Position {
            get {
                int x, y;
                SDL.SDL_GetWindowPosition(Handle, out x, out y);
                return new ImVec2(x, y);
            }
            set {
                SDL.SDL_SetWindowPosition(Handle, (int) Math.Round(value.X), (int) Math.Round(value.Y));
            }
        }

        public ImVec2 Size {
            get {
                int x, y;
                SDL.SDL_GetWindowSize(Handle, out x, out y);
                return new ImVec2(x, y);
            }
            set {
                SDL.SDL_SetWindowSize(Handle, (int) Math.Round(value.X), (int) Math.Round(value.Y));
            }
        }

        public ImGuiSDL2CSWindow(
            string title = "ImGui.NET-SDL2-CS Window",
            int x = SDL.SDL_WINDOWPOS_CENTERED, int y = SDL.SDL_WINDOWPOS_CENTERED,
            int width = 800, int height = 600,
            SDL.SDL_WindowFlags flags = SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL | SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE | SDL.SDL_WindowFlags.SDL_WINDOW_HIDDEN
        ) : base(title, x, y, width, height, flags) {
            _IsSuperClass = GetType() == typeof(ImGuiSDL2CSWindow);
            ImGuiSDL2CSHelper.Init();
            OnEvent = ImGuiOnEvent;
            OnLoop = ImGuiOnLoop;

        }

        public override void Run() {
            if (!File.Exists("imgui.ini"))
                File.WriteAllText("imgui.ini", "");

            Create();

            base.Run();
        }

        public bool ImGuiOnEvent(SDL2Window window, SDL.SDL_Event e)
            => ImGuiSDL2CSHelper.OnEvent(e, ref g_MouseWheel, g_MousePressed);

        public void ImGuiOnLoop(SDL2Window window) {
            GL.ClearColor(0.1f, 0.125f, 0.15f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            ImGuiRender();

            Swap();
        }

        public virtual void ImGuiRender() {
            int mouseX, mouseY;
            uint mouseMask = SDL.SDL_GetMouseState(out mouseX, out mouseY);
            if ((SDL.SDL_GetWindowFlags(Handle) & (uint) SDL.SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS) == 0)
                mouseX = mouseY = -1;
            ImGuiSDL2CSHelper.NewFrame(Size, ImVec2.One, new ImVec2(mouseX, mouseY), mouseMask, ref g_MouseWheel, g_MousePressed, ref g_Time);

            ImGuiLayout();

            ImGuiSDL2CSHelper.Render(Size);
        }

        public virtual void ImGuiLayout() {
            if (_IsSuperClass)
                ImGui.Text($"Create a new class inheriting {GetType().FullName}, overriding {nameof(ImGuiLayout)}!");
            else
                ImGui.Text($"Override {nameof(ImGuiLayout)} in {GetType().FullName}!");
        }

        protected virtual void Create() {
            ImGuiIO io = ImGui.GetIO();

            // Build texture atlas
            ImFontTextureData texData = io.FontAtlas.GetTexDataAsAlpha8();

            int lastTexture;
            GL.GetInteger(GetPName.TextureBinding2D, out lastTexture);

            // Create OpenGL texture
            g_FontTexture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, g_FontTexture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear);
            GL.PixelStore(PixelStoreParameter.UnpackRowLength, 0);
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Alpha,
                texData.Width,
                texData.Height,
                0,
                PixelFormat.Alpha,
                PixelType.UnsignedByte,
                texData.Pixels
            );

            // Store the texture identifier in the ImFontAtlas substructure.
            io.FontAtlas.SetTexID(g_FontTexture);
            io.FontAtlas.ClearTexData(); // Clears CPU side texture data.
            GL.BindTexture(TextureTarget.Texture2D, lastTexture);
        }

        protected override void Dispose(bool disposing) {
            ImGuiIO io = ImGui.GetIO();

            if (disposing) {
                // Dispose managed state (managed objects).
            }

            // Free unmanaged resources (unmanaged objects) and override a finalizer below.
            // Set large fields to null.
            if (g_FontTexture != 0) {
                // Texture gets deleted with the context.
                // GL.DeleteTexture(g_FontTexture);
                if ((int) io.FontAtlas.TexID == g_FontTexture)
                    io.FontAtlas.TexID = IntPtr.Zero;
                g_FontTexture = 0;
            }

            base.Dispose(disposing);
        }

        ~ImGuiSDL2CSWindow() {
            Dispose(false);
        }

    }
}
