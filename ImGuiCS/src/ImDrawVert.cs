using System.Runtime.InteropServices;

namespace ImGuiNET {
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImDrawVert {
        public ImVec2 pos;
        public ImVec2 uv;
        public uint col;

        public const int PosOffset = 0;
        public const int UVOffset = 8;
        public const int ColOffset = 16;
        public readonly static int Size = sizeof(ImDrawVert);
    };
}
