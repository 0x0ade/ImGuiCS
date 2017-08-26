using System;
using System.Runtime.InteropServices;

namespace ImGuiNET {
    public unsafe struct ImDrawChannel {

        public readonly NativeImDrawChannel* Native;

        public ImDrawChannel(NativeImDrawChannel* native) {
            Native = native;
        }

        public ImVector<ImDrawCmd> CmdBuffer {
            get {
                return &Native->CmdBuffer;
            }
            set {
                Native->CmdBuffer = value;
            }
        }

        public ImVector<ushort> IdxBuffer {
            get {
                return &Native->IdxBuffer;
            }
            set {
                Native->IdxBuffer = value;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct NativeImDrawChannel {
        public ImVector CmdBuffer;
        public ImVector IdxBuffer;
    }
}
