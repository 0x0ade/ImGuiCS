using System.Runtime.InteropServices;

namespace ImGuiNET {
    public unsafe class ImGuiStyle {

        public readonly NativeImGuiStyle* Native;

        public ImGuiStyle(NativeImGuiStyle* native) {
            Native = native;
        }

        /// <summary>
        /// Global alpha applies to everything in ImGui.
        /// </summary>
        public float Alpha {
            get { return Native->Alpha; }
            set { Native->Alpha = value; }
        }

        /// <summary>
        /// Padding within a window.
        /// </summary>
        public ImVec2 WindowPadding {
            get { return Native->WindowPadding; }
            set { Native->WindowPadding = value; }
        }

        /// <summary>
        /// Minimum window size.
        /// </summary>
        public ImVec2 WindowMinSize {
            get { return Native->WindowMinSize; }
            set { Native->WindowMinSize = value; }
        }

        /// <summary>
        /// Radius of window corners rounding. Set to 0.0f to have rectangular windows.
        /// </summary>
        public float WindowRounding {
            get { return Native->WindowRounding; }
            set { Native->WindowRounding = value; }
        }

        /// <summary>
        /// Alignment for title bar text.
        /// </summary>
        public ImVec2 WindowTitleAlign {
            get { return Native->WindowTitleAlign; }
            set { Native->WindowTitleAlign = value; }
        }

        /// <summary>
        /// Radius of child window corners rounding. Set to 0.0f to have rectangular windows.
        /// </summary>
        public float ChildWindowRounding {
            get { return Native->ChildWindowRounding; }
            set { Native->ChildWindowRounding = value; }
        }

        /// <summary>
        /// Padding within a framed rectangle (used by most widgets).
        /// </summary>
        public ImVec2 FramePadding {
            get { return Native->FramePadding; }
            set { Native->FramePadding = value; }
        }

        /// <summary>
        /// Radius of frame corners rounding. Set to 0.0f to have rectangular frame (used by most widgets). 
        /// </summary>
        public float FrameRounding {
            get { return Native->FrameRounding; }
            set { Native->FrameRounding = value; }
        }

        /// <summary>
        /// Horizontal and vertical spacing between widgets/lines.
        /// </summary>
        public ImVec2 ItemSpacing {
            get { return Native->ItemSpacing; }
            set { Native->ItemSpacing = value; }
        }

        /// <summary>
        /// Horizontal and vertical spacing between within elements of a composed widget (e.g. a slider and its label).
        /// </summary>
        public ImVec2 ItemInnerSpacing {
            get { return Native->ItemInnerSpacing; }
            set { Native->ItemInnerSpacing = value; }
        }

        /// <summary>
        /// Expand reactive bounding box for touch-based system where touch position is not accurate enough. Unfortunately we don't sort widgets so priority on overlap will always be given to the first widget. So don't grow this too much!
        /// </summary>
        public ImVec2 TouchExtraPadding {
            get { return Native->TouchExtraPadding; }
            set { Native->TouchExtraPadding = value; }
        }

        /// <summary>
        /// Horizontal indentation when e.g. entering a tree node
        /// </summary>
        public float IndentSpacing {
            get { return Native->IndentSpacing; }
            set { Native->IndentSpacing = value; }
        }

        /// <summary>
        /// Minimum horizontal spacing between two columns
        /// </summary>
        public float ColumnsMinSpacing {
            get { return Native->ColumnsMinSpacing; }
            set { Native->ColumnsMinSpacing = value; }
        }

        /// <summary>
        /// Width of the vertical scrollbar, Height of the horizontal scrollbar
        /// </summary>
        public float ScrollbarSize {
            get { return Native->ScrollbarSize; }
            set { Native->ScrollbarSize = value; }
        }

        /// <summary>
        /// Radius of grab corners for scrollbar
        /// </summary>
        public float ScrollbarRounding {
            get { return Native->ScrollbarRounding; }
            set { Native->ScrollbarRounding = value; }
        }

        /// <summary>
        /// Minimum width/height of a grab box for slider/scrollbar
        /// </summary>
        public float GrabMinSize {
            get { return Native->GrabMinSize; }
            set { Native->GrabMinSize = value; }
        }

        /// <summary>
        /// Radius of grabs corners rounding. Set to 0.0f to have rectangular slider grabs.
        /// </summary>
        public float GrabRounding {
            get { return Native->GrabRounding; }
            set { Native->GrabRounding = value; }
        }

        /// <summary>
        /// Window positions are clamped to be visible within the display area by at least this amount. Only covers regular windows.
        /// </summary>
        public ImVec2 DisplayWindowPadding {
            get { return Native->DisplayWindowPadding; }
            set { Native->DisplayWindowPadding = value; }
        }

        /// <summary>
        /// If you cannot see the edge of your screen (e.g. on a TV) increase the safe area padding. Covers popups/tooltips as well regular windows.
        /// </summary>
        public ImVec2 DisplaySafeAreaPadding {
            get { return Native->DisplaySafeAreaPadding; }
            set { Native->DisplaySafeAreaPadding = value; }
        }

        /// <summary>
        /// Enable anti-aliasing on lines/borders. Disable if you are really tight on CPU/GPU.
        /// </summary>
        public bool AntiAliasedLines {
            get { return Native->AntiAliasedLines == 1; }
            set { Native->AntiAliasedLines = value ? (byte) 1 : (byte) 0; }
        }

        /// <summary>
        /// Enable anti-aliasing on filled shapes (rounded rectangles, circles, etc.)
        /// </summary>
        public bool AntiAliasedShapes {
            get { return Native->AntiAliasedShapes == 1; }
            set { Native->AntiAliasedShapes = value ? (byte) 1 : (byte) 0; }
        }

        /// <summary>
        /// Tessellation tolerance. Decrease for highly tessellated curves (higher quality, more polygons), increase to reduce quality.
        /// </summary>
        public float CurveTessellationTolerance {
            get { return Native->CurveTessellationTol; }
            set { Native->CurveTessellationTol = value; }
        }

        /// <summary>
        /// Gets the current style color for the given UI element type.
        /// </summary>
        /// <param name="target">The type of UI element.</param>
        /// <returns>The element's color as currently configured.</returns>
        public ImVec4 GetColor(ImGuiCol target) => *(ImVec4*) &Native->Colors[(int) target * 4];

        /// <summary>
        /// Sets the style color for a particular UI element type.
        /// </summary>
        /// <param name="target">The type of UI element.</param>
        /// <param name="value">The new color.</param>
        public void SetColor(ImGuiCol target, ImVec4 value) {
            Native->Colors[(int) target * 4 + 0] = value.X;
            Native->Colors[(int) target * 4 + 1] = value.Y;
            Native->Colors[(int) target * 4 + 2] = value.Z;
            Native->Colors[(int) target * 4 + 3] = value.W;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct NativeImGuiStyle {
        /// <summary>
        /// Global alpha applies to everything in ImGui.
        /// </summary>
        public float Alpha;
        /// <summary>
        /// Padding within a window.
        /// </summary>
        public ImVec2 WindowPadding;
        /// <summary>
        /// Minimum window size.
        /// </summary>
        public ImVec2 WindowMinSize;
        /// <summary>
        /// Radius of window corners rounding. Set to 0.0f to have rectangular windows.
        /// </summary>
        public float WindowRounding;
        /// <summary>
        /// Alignment for title bar text.
        /// </summary>
        public ImVec2 WindowTitleAlign;
        /// <summary>
        /// Radius of child window corners rounding. Set to 0.0f to have rectangular windows.
        /// </summary>
        public float ChildWindowRounding;
        /// <summary>
        /// Padding within a framed rectangle (used by most widgets).
        /// </summary>
        public ImVec2 FramePadding;
        /// <summary>
        /// Radius of frame corners rounding. Set to 0.0f to have rectangular frame (used by most widgets). 
        /// </summary>
        public float FrameRounding;
        /// <summary>
        /// Horizontal and vertical spacing between widgets/lines.
        /// </summary>
        public ImVec2 ItemSpacing;
        /// <summary>
        /// Horizontal and vertical spacing between within elements of a composed widget (e.g. a slider and its label).
        /// </summary>
        public ImVec2 ItemInnerSpacing;
        /// <summary>
        /// Expand reactive bounding box for touch-based system where touch position is not accurate enough. Unfortunately we don't sort widgets so priority on overlap will always be given to the first widget. So don't grow this too much!
        /// </summary>
        public ImVec2 TouchExtraPadding;
        /// <summary>
        /// Horizontal indentation when e.g. entering a tree node
        /// </summary>
        public float IndentSpacing;
        /// <summary>
        /// Minimum horizontal spacing between two columns
        /// </summary>
        public float ColumnsMinSpacing;
        /// <summary>
        /// Width of the vertical scrollbar, Height of the horizontal scrollbar
        /// </summary>
        public float ScrollbarSize;
        /// <summary>
        /// Radius of grab corners for scrollbar
        /// </summary>
        public float ScrollbarRounding;
        /// <summary>
        /// Minimum width/height of a grab box for slider/scrollbar
        /// </summary>
        public float GrabMinSize;
        /// <summary>
        /// Radius of grabs corners rounding. Set to 0.0f to have rectangular slider grabs.
        /// </summary>
        public float GrabRounding;
        /// <summary>
        /// Window positions are clamped to be visible within the display area by at least this amount. Only covers regular windows.
        /// </summary>
        public ImVec2 DisplayWindowPadding;
        /// <summary>
        /// If you cannot see the edge of your screen (e.g. on a TV) increase the safe area padding. Covers popups/tooltips as well regular windows.
        /// </summary>
        public ImVec2 DisplaySafeAreaPadding;
        /// <summary>
        /// Enable anti-aliasing on lines/borders. Disable if you are really tight on CPU/GPU.
        /// </summary>
        public byte AntiAliasedLines;
        /// <summary>
        /// Enable anti-aliasing on filled shapes (rounded rectangles, circles, etc.)
        /// </summary>
        public byte AntiAliasedShapes;
        /// <summary>
        /// Tessellation tolerance. Decrease for highly tessellated curves (higher quality, more polygons), increase to reduce quality.
        /// </summary>
        public float CurveTessellationTol;
        public fixed float Colors[(int) ImGuiCol.Count * 4];
    }
}
