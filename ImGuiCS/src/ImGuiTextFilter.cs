using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImGuiNET {
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImGuiTextFilter {

        [StructLayout(LayoutKind.Sequential)]
        public struct TextRange {
            public byte* b;
            public byte* e;
            public string Value => Marshal.PtrToStringAnsi((IntPtr) b, (int) (e - b));

            public TextRange(byte* _b, byte* _e) { b = _b; e = _e; }
            public bool empty() => b == e;
            public byte front() { return *b; }
            static bool is_blank(byte c) { return c == ' ' || c == '\t'; }
            public void trim_blanks() { while (b < e && is_blank(*b)) b++; while (e > b && is_blank(*(e - 1))) e--; }
            // cimgui doesn't wrap this
            // IMGUI_API void split(char separator, ImVector<TextRange>& out)
            public void split(char separator, ref List<string> @out)
                => @out = new List<string>(Value.Split(separator));
        }

        public fixed byte InputBuf[256];
        public ImVector Filters;
        public int CountGrep;

        public ImGuiTextFilter Init(string defaultFilter = "") {
            IntPtr defaultFilterPtr = Marshal.StringToHGlobalAnsi(defaultFilter);
            fixed (ImGuiTextFilter* ptr = &this)
                ImGuiNative.ImGuiTextFilter_Init(ptr, (char*) defaultFilterPtr);
            Marshal.FreeHGlobal(defaultFilterPtr);
            return this;
        }

        public void Clear() {
            fixed (ImGuiTextFilter* ptr = &this)
                ImGuiNative.ImGuiTextFilter_Clear(ptr);
        }

        // Helper calling InputText+Build
        public bool Draw(string label = "Filter (inc,-exc)", float width = 0.0f) {
            IntPtr labelPtr = Marshal.StringToHGlobalAnsi(label);
            bool rv;
            fixed (ImGuiTextFilter* ptr = &this)
                rv = ImGuiNative.ImGuiTextFilter_Draw(ptr, (char*) labelPtr, width);
            Marshal.FreeHGlobal(labelPtr);
            return rv;
        }

        public bool PassFilter(string text) {
            IntPtr textPtr = Marshal.StringToHGlobalAnsi(text);
            bool rv;
            fixed (ImGuiTextFilter* ptr = &this)
                rv = ImGuiNative.ImGuiTextFilter_PassFilter(ptr, (char*) textPtr, (char*) ((long) textPtr + text.Length));
            Marshal.FreeHGlobal(textPtr);
            return rv;
        }

        public bool IsActive() {
            fixed (ImGuiTextFilter* ptr = &this)
                return ImGuiNative.ImGuiTextFilter_IsActive(ptr);
        }

        public void Build() {
            fixed (ImGuiTextFilter* ptr = &this)
                ImGuiNative.ImGuiTextFilter_Build(ptr);
        }

    }

}
