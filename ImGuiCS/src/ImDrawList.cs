using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ImGuiNET {
    public unsafe struct ImDrawList {

        public readonly NativeImDrawList* Native;

        public ImDrawList(NativeImDrawList* native) {
            Native = native;
        }

        public void AddLine(ImVec2 a, ImVec2 b, uint color, float thickness) {
            ImGuiNative.ImDrawList_AddLine(Native, a, b, color, thickness);
        }

        public void AddRect(ImVec2 a, ImVec2 b, uint color, float rounding, int rounding_corners, float thickness) {
            ImGuiNative.ImDrawList_AddRect(Native, a, b, color, rounding, rounding_corners, thickness);
        }

        public void AddRectFilled(ImVec2 a, ImVec2 b, uint color, float rounding, int rounding_corners = ~0) {
            ImGuiNative.ImDrawList_AddRectFilled(Native, a, b, color, rounding, rounding_corners);
        }

        public void AddRectFilledMultiColor(
            ImVec2 a,
            ImVec2 b,
            uint colorUpperLeft,
            uint colorUpperRight,
            uint colorBottomRight,
            uint colorBottomLeft) {
            ImGuiNative.ImDrawList_AddRectFilledMultiColor(
                Native,
                a,
                b,
                colorUpperLeft,
                colorUpperRight,
                colorBottomRight,
                colorBottomLeft);
        }

        public void AddCircle(ImVec2 center, float radius, uint color, int numSegments, float thickness) {
            ImGuiNative.ImDrawList_AddCircle(Native, center, radius, color, numSegments, thickness);
        }

        public unsafe void AddText(ImVec2 position, string text, uint color) {
            int count = Encoding.UTF8.GetByteCount(text);
            byte* data = (byte*) Marshal.AllocHGlobal(count);
            char[] chars = text.ToCharArray();
            fixed (char* charsPtr = &chars[0])
                Encoding.UTF8.GetBytes(charsPtr, chars.Length, data, count);
            ImGuiNative.ImDrawList_AddText(Native, position, color, data, data + count);
            Marshal.FreeHGlobal((IntPtr) data);
        }

        public unsafe void AddImage(int userTextureID, ImVec2 a, ImVec2 b, ImVec2 uv_a, ImVec2 uv_b, uint col) {
            ImGuiNative.ImDrawList_AddImage(Native, (void*) userTextureID, a, b, uv_a, uv_b, col);
        }

        public void PushClipRect(ImVec2 min, ImVec2 max, bool intersectWithCurrentClipRect) {
            ImGuiNative.ImDrawList_PushClipRect(Native, min, max, intersectWithCurrentClipRect ? (byte) 1 : (byte) 0);
        }

        public void PushClipRectFullScreen() {
            ImGuiNative.ImDrawList_PushClipRectFullScreen(Native);
        }

        public void PopClipRect() {
            ImGuiNative.ImDrawList_PopClipRect(Native);
        }

        public void AddDrawCmd() {
            ImGuiNative.ImDrawList_AddDrawCmd(Native);
        }

        public void AddCallback(NativeImDrawCallback callback) {
            ImGuiNative.ImDrawList_AddCallback(Native, callback, (void*) IntPtr.Zero);
        }

        public void AddCallback(ImDrawCallback callback) {
            ImGuiNative.ImDrawList_AddCallback(Native, (parent_list, cmd) => {
                callback(ref (*parent_list), ref (*cmd));
            }, (void*) IntPtr.Zero);
        }

        /// <summary>
        /// ImVector(ImDrawCmd).
        /// Commands. Typically 1 command = 1 gpu draw call.
        /// </summary>
        public ImVector<ImDrawCmd> CmdBuffer {
            get {
                return &Native->CmdBuffer;
            }
            set {
                Native->CmdBuffer = value;
            }
        }

        /// <summary>
        /// ImVector(ImDrawIdx being ushort).
        /// Index buffer. Each command consume ImDrawCmd::ElemCount of those
        /// </summary>
        public ImVector<ushort> IdxBuffer {
            get {
                return &Native->IdxBuffer;
            }
            set {
                Native->IdxBuffer = value;
            }
        }

        /// <summary>
        /// ImVector(ImDrawIdx being uint).
        /// Index buffer. Each command consume ImDrawCmd::ElemCount of those
        /// </summary>
        public ImVector<uint> IdxBuffer32 {
            get {
                return &Native->IdxBuffer;
            }
            set {
                Native->IdxBuffer = value;
            }
        }

        /// <summary>
        /// ImVector(ImDrawVert)
        /// </summary>
        public ImVector<ImDrawVert> VtxBuffer {
            get {
                return &Native->VtxBuffer;
            }
            set {
                Native->VtxBuffer = value;
            }
        }

        // [Internal, used while building lists]
        /// <summary>
        /// Pointer to owner window's name (if any) for debugging
        /// </summary>
        public string _OwnerName {
            get {
                return Marshal.PtrToStringAnsi(Native->_OwnerName);
            }
            // No need to set _OwnerName...
        }

        /// <summary>
        /// [Internal] == VtxBuffer.Size
        /// </summary>
        public uint _VtxCurrentIdx {
            get {
                return Native->_VtxCurrentIdx;
            }
            set {
                Native->_VtxCurrentIdx = value;
            }
        }

        /// <summary>
        /// [Internal] point within VtxBuffer.Data after each add command (to avoid using the ImVector operators too much)
        /// </summary>
        public IntPtr _VtxWritePtr {
            get {
                return Native->_VtxWritePtr;
            }
            set {
                Native->_VtxWritePtr = value;
            }
        }
        /// <summary>
        /// [Internal] point within IdxBuffer.Data after each add command (to avoid using the ImVector operators too much)
        /// </summary>
        public IntPtr _IdxWritePtr {
            get {
                return Native->_IdxWritePtr;
            }
            set {
                Native->_IdxWritePtr = value;
            }
        }

        /// <summary>
        /// [Internal]
        /// </summary>
        public ImVector<ImVec4> _ClipRectStack {
            get {
                return &Native->_ClipRectStack;
            }
            set {
                Native->_ClipRectStack = value;
            }
        }

        /// <summary>
        /// [Internal]
        /// </summary>
        public ImVector<IntPtr> _TextureIdStack {
            get {
                return &Native->_TextureIdStack;
            }
            set {
                Native->_TextureIdStack = value;
            }
        }

        /// <summary>
        /// [Internal] current path building
        /// </summary>
        public ImVector<ImVec2> _Path {
            get {
                return &Native->_Path;
            }
            set {
                Native->_Path = value;
            }
        }

        /// <summary>
        /// [Internal] current channel number (0)
        /// </summary>
        public int _ChannelsCurrent {
            get {
                return Native->_ChannelsCurrent;
            }
            set {
                Native->_ChannelsCurrent = value;
            }
        }

        /// <summary>
        /// [Internal] number of active channels (1+)
        /// </summary>
        public int _ChannelsCount {
            get {
                return Native->_ChannelsCount;
            }
            set {
                Native->_ChannelsCount = value;
            }
        }

        /// <summary>
        /// [Internal] draw channels for columns API (not resized down so _ChannelsCount may be smaller than _Channels.Size)
        /// </summary>
        public ImVector<ImDrawChannel> _Channels {
            get {
                return &Native->_Channels;
            }
            set {
                Native->_Channels = value;
            }
        }

    }

    /// <summary>
    /// Draw command list
    /// This is the low-level list of polygons that ImGui functions are filling. At the end of the frame, all command lists are passed to your ImGuiIO::RenderDrawListFn function for rendering.
    /// At the moment, each ImGui window contains its own ImDrawList but they could potentially be merged in the future.
    /// If you want to add custom rendering within a window, you can use ImGui::GetWindowDrawList() to access the current draw list and add your own primitives.
    /// You can interleave normal ImGui:: calls and adding primitives to the current draw list.
    /// All positions are in screen coordinates (0,0=top-left, 1 pixel per unit). Primitives are always added to the list and not culled (culling is done at render time and at a higher-level by ImGui:: functions).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct NativeImDrawList {
        // This is what you have to render

        /// <summary>
        /// ImVector(ImDrawCmd).
        /// Commands. Typically 1 command = 1 gpu draw call.
        /// </summary>
        public ImVector CmdBuffer;
        /// <summary>
        /// ImVector(ImDrawIdx).
        /// Index buffer. Each command consume ImDrawCmd::ElemCount of those
        /// </summary>
        public ImVector IdxBuffer;
        /// <summary>
        /// ImVector(ImDrawVert)
        /// </summary>
        public ImVector VtxBuffer;

        // [Internal, used while building lists]
        /// <summary>
        /// Pointer to owner window's name (if any) for debugging
        /// </summary>
        public IntPtr _OwnerName;
        /// <summary>
        /// [Internal] == VtxBuffer.Size
        /// </summary>
        public uint _VtxCurrentIdx;

        /// <summary>
        /// [Internal] point within VtxBuffer.Data after each add command (to avoid using the ImVector operators too much)
        /// </summary>
        public IntPtr _VtxWritePtr;
        /// <summary>
        /// [Internal] point within IdxBuffer.Data after each add command (to avoid using the ImVector operators too much)
        /// </summary>
        public IntPtr _IdxWritePtr;

        /// <summary>
        /// [Internal]
        /// </summary>
        public ImVector _ClipRectStack;
        /// <summary>
        /// [Internal]
        /// </summary>
        public ImVector _TextureIdStack;
        /// <summary>
        /// [Internal] current path building
        /// </summary>
        public ImVector _Path;

        /// <summary>
        /// [Internal] current channel number (0)
        /// </summary>
        public int _ChannelsCurrent;
        /// <summary>
        /// [Internal] number of active channels (1+)
        /// </summary>
        public int _ChannelsCount;

        /// <summary>
        /// [Internal] draw channels for columns API (not resized down so _ChannelsCount may be smaller than _Channels.Size)
        /// </summary>
        public ImVector _Channels;
    }
}
