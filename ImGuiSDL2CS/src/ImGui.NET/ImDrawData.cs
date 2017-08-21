using System.Runtime.InteropServices;

namespace ImGuiNET {
    public unsafe struct ImDrawData {

        public readonly NativeImDrawData* Native;

        public ImDrawData(NativeImDrawData* native) {
            Native = native;
        }

        /// <summary>
        /// Only valid after Render() is called and before the next NewFrame() is called.
        /// </summary>
        public byte Valid {
            get { return Native->Valid; }
            set { Native->Valid = value; }
        }

        public NativeImDrawList** CmdLists {
            get { return Native->CmdLists; }
            set { Native->CmdLists = value; }
        }

        public int CmdListsCount {
            get { return Native->CmdListsCount; }
            set { Native->CmdListsCount = value; }
        }

        /// <summary>
        /// For convenience, sum of all cmd_lists vtx_buffer.Size
        /// </summary>
        public int TotalVtxCount {
            get { return Native->TotalVtxCount; }
            set { Native->TotalVtxCount = value; }
        }

        /// <summary>
        /// For convenience, sum of all cmd_lists idx_buffer.Size
        /// </summary>
        public int TotalIdxCount {
            get { return Native->TotalIdxCount; }
            set { Native->TotalIdxCount = value; }
        }

        public ImDrawList this[int i] {
            get {
                return new ImDrawList(Native->CmdLists[i]);
            }
            set {
                Native->CmdLists[i] = value.Native;
            }
        }

    }

    /// <summary>
    /// All draw data to render an ImGui frame
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct NativeImDrawData {
        /// <summary>
        /// Only valid after Render() is called and before the next NewFrame() is called.
        /// </summary>
        public byte Valid;
        public NativeImDrawList** CmdLists;
        public int CmdListsCount;
        /// <summary>
        /// For convenience, sum of all cmd_lists vtx_buffer.Size
        /// </summary>
        public int TotalVtxCount;
        /// <summary>
        /// For convenience, sum of all cmd_lists idx_buffer.Size
        /// </summary>
        public int TotalIdxCount;
    }
}
