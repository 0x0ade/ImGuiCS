using System;
using System.Collections.Generic;
using SDL2;
using ImGuiNET;
using ImGuiSDL2CS;

namespace YourGameNamespace {
    public class YourGameWindow : ImGuiSDL2CSWindow {

        private TextInputBuffer[] _TextInputBuffers;

        private MemoryEditor _MemoryEditor = new MemoryEditor();
        private byte[] _MemoryEditorData;

        public YourGameWindow()
            : base("Your Game Window Title") {

            //////// OPTIONAL ////////
            // This affects the "underlying" SDL2Window and can be used for a quick game loop sketch.
            // Don't set those and only ImGui gets rendered / handled.
            // They're delegate fields so that one can override those from outside.
            OnEvent = MyEventHandler;
            OnLoop = MyGameLoop;

            _MemoryEditorData = new byte[1024];
            Random rnd = new Random();
            for (int i = 0; i < _MemoryEditorData.Length; i++) {
                _MemoryEditorData[i] = (byte) rnd.Next(255);
            }

        }

        // Create any possibly unmanaged resources (textures, buffers) here.
        protected override void Create() {
            base.Create();

            _TextInputBuffers = TextInputBuffer.CreateBuffers(2);
        }

        // Dispose any possibly unmanaged resources (textures, buffers) here.
        protected override void Dispose(bool disposing) {
            TextInputBuffer.DisposeBuffers(_TextInputBuffers);

            base.Dispose(disposing);
        }

        // This runs between ImGuiSDL2CSHelper.NewFrame and ImGuiSDL2CSHelper.Render.
        // Direct port of the example at https://github.com/ocornut/imgui/blob/master/examples/sdl_opengl2_example/main.cpp
        float f = 0.0f;
        bool show_test_window = true;
        bool show_another_window = false;
        ImVec3 clear_color = new ImVec3(114f/255f, 144f/255f, 154f/255f);
        public unsafe override void ImGuiLayout() {
            // 1. Show a simple window
            // Tip: if we don't call ImGui.Begin()/ImGui.End() the widgets appears in a window automatically called "Debug"
            {
                ImGui.Text("Hello, world!");
                ImGui.SliderFloat("float", ref f, 0.0f, 1.0f, null, 1f);
                ImGui.ColorEdit3("clear color", ref clear_color, false);
                if (ImGui.Button("Test Window")) show_test_window = !show_test_window;
                if (ImGui.Button("Another Window")) show_another_window = !show_another_window;
                ImGui.Text(string.Format("Application average {0:F3} ms/frame ({1:F1} FPS)", 1000f / ImGui.GetIO().Framerate, ImGui.GetIO().Framerate));
                ImGui.InputText("Text Input 1", _TextInputBuffers[0].Buffer, _TextInputBuffers[0].Length, ImGuiInputTextFlags.Default);
                ImGui.InputText("Text Input 2", _TextInputBuffers[1].Buffer, _TextInputBuffers[1].Length, ImGuiInputTextFlags.Default);
            }

            // 2. Show another simple window, this time using an explicit Begin/End pair
            if (show_another_window) {
                ImGui.SetNextWindowSize(new ImVec2(200, 100), ImGuiSetCond.FirstUseEver);
                ImGui.Begin("Another Window", ref show_another_window, ImGuiWindowFlags.Default);
                ImGui.Text("Hello");
                ImGui.Endow();
            }

            // 3. Show the ImGui test window. Most of the sample code is in ImGui.ShowTestWindow()
            if (show_test_window) {
                ImGui.SetNextWindowPos(new ImVec2(650, 20), ImGuiSetCond.FirstUseEver);
                ImGui.ShowTestWindow(ref show_test_window);
            }

            // 4. Show the memory editor, just as an example.
            _MemoryEditor.Draw("Memory editor", _MemoryEditorData, _MemoryEditorData.Length);

        }

        //////// OPTIONAL ////////

        // Processs any SDL2 events manually if required.
        // Return false to not allow the default event handler to process it.
        public bool MyEventHandler(SDL2Window _self, SDL.SDL_Event e) {
            // We're replacing OnEvent and thus call ImGuiSDL2CSHelper.OnEvent manually.
            if (!ImGuiSDL2CSHelper.OnEvent(e, ref g_MouseWheel, g_MousePressed))
                return false;

            // Any custom event handling can happen here.

            return true;
        }

        // Any custom game loop should end up here.
        // Setting the window.IsActive = false stops the loop.
        public void MyGameLoop(SDL2Window _self) {
            // This is the default implementation, except for the ClearColor not being 0.1f, 0.125f, 0.15f, 1f

            // Using MiniTK (not OpenTK) to provide access to OpenGL methods.
            // Alternatively, use SDL_GL_GetProcAddress on your own.
            OpenTK.Graphics.OpenGL.GL.ClearColor(clear_color.X, clear_color.Y, clear_color.Z, 1f);
            OpenTK.Graphics.OpenGL.GL.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.ColorBufferBit);

            // This calls ImGuiSDL2CSHelper.NewFrame, the overridden ImGuiLayout, ImGuiSDL2CSHelper.Render and renders it.
            // ImGuiSDL2CSHelper.NewFrame properly sets up ImGuiIO and ImGuiSDL2CSHelper.Render renders the draw data.
            ImGuiRender();

            // Finally, swap.
            Swap();
        }

    }
}
