using System.Runtime.InteropServices;

namespace ImGuiNET {
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImGuiListClipper {
        public float StartPosY;
        public float ItemsHeight;
        public int ItemsCount, StepNo;
        private int _DisplayStart, _DisplayEnd;

        public int DisplayStart {
            get {
                fixed (ImGuiListClipper* ptr = &this) {
                    return ImGuiNative.ImGuiListClipper_GetDisplayStart(ptr);
                }
            }
        }

        public int DisplayEnd {
            get {
                fixed (ImGuiListClipper* ptr = &this) {
                    return ImGuiNative.ImGuiListClipper_GetDisplayEnd(ptr);
                }
            }
        }

        public ImGuiListClipper(int items_count = -1, float items_height = -1f) {
            fixed (ImGuiListClipper* ptr = &this) {
                ImGuiNative.ImGuiListClipper_Begin(ptr, items_count, items_height);
            }
        }

        public bool Step() {
            fixed (ImGuiListClipper* ptr = &this) {
                return ImGuiNative.ImGuiListClipper_Step(ptr);
            }
        }

        public void Begin(int items_count = -1, float items_height = -1f) {
            fixed (ImGuiListClipper* ptr = &this) {
                ImGuiNative.ImGuiListClipper_Begin(ptr, items_count, items_height);
            }
        }

        public void End() {
            fixed (ImGuiListClipper* ptr = &this) {
                ImGuiNative.ImGuiListClipper_End(ptr);
            }
        }

    }
}
