﻿namespace ImGuiNET {
    /// <summary>
    /// Flags for ImGui::Begin()
    /// </summary>
    public enum ImGuiWindowFlags : int {
        Default = 0,
        /// <summary>
        /// Disable title-bar
        /// </summary>
        NoTitleBar = 1 << 0,
        /// <summary>
        /// Disable user resizing with the lower-right grip
        /// </summary>
        NoResize = 1 << 1,
        /// <summary>
        /// Disable user moving the window
        /// </summary>
        NoMove = 1 << 2,
        /// <summary>
        /// Disable scrollbar (window can still scroll with mouse or programatically)
        /// </summary>
        NoScrollbar = 1 << 3,
        /// <summary>
        /// Disable user scrolling with mouse wheel
        /// </summary>
        NoScrollWithMouse = 1 << 4,
        /// <summary>
        /// Disable user collapsing window by double-clicking on it
        /// </summary>
        NoCollapse = 1 << 5,
        /// <summary>
        /// Resize every window to its content every frame
        /// </summary>
        AlwaysAutoResize = 1 << 6,
        /// <summary>
        /// Show borders around windows and items
        /// </summary>
        ShowBorders = 1 << 7,
        /// <summary>
        /// Never load/save settings in .ini file
        /// </summary>
        NoSavedSettings = 1 << 8,
        /// <summary>
        /// Disable catching mouse or keyboard inputs
        /// </summary>
        NoInputs = 1 << 9,
        /// <summary>
        /// Has a menu-bar
        /// </summary>
        MenuBar = 1 << 10,
        /// <summary>
        /// Enable horizontal scrollbar (off by default).
        /// You need to use SetNextWindowContentSize(ImVec2(width,0.0f)); prior to calling Begin() to specify width.
        /// </summary>
        HorizontalScrollbar = 1 << 11,
        /// <summary>
        /// Disable taking focus when transitioning from hidden to visible state
        /// </summary>
        NoFocusOnAppearing = 1 << 12,
        /// <summary>
        /// Disable bringing window to front when taking focus (e.g. clicking on it or programatically giving it focus)
        /// </summary>
        NoBringToFrontOnFocus = 1 << 13,
    }
}
