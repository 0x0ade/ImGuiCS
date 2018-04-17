using System.Runtime.InteropServices;
using System;

namespace ImGuiNET {
    /// <summary>
    /// Contains all of the exported functions from the native (c)imGui module.
    /// </summary>
    public static unsafe class ImGuiNative {
        private const string cimguiLib = "cimgui";

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern NativeImGuiIO* igGetIO();

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern NativeImGuiStyle* igGetStyle();

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern NativeImDrawData* igGetDrawData();

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igNewFrame();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igNewLine();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igRender();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igShutdown();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igShowUserGuide();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igShowStyleEditor(NativeImGuiStyle* @ref);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igShowTestWindow(ref bool opened);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igShowMetricsWindow(ref bool opened);

        // Window
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igBegin(string name, ref bool p_opened, ImGuiWindowFlags flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igBegin2(string name, ref bool p_opened, ImVec2 size_on_first_use, float bg_alpha, ImGuiWindowFlags flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igEnd();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igBeginChild(string str_id, ImVec2 size, bool border, ImGuiWindowFlags extra_flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igBeginChildEx(uint id, ImVec2 size, bool border, ImGuiWindowFlags extra_flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igEndChild();

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetContentRegionMax(out ImVec2 @out);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetContentRegionAvail(out ImVec2 @out);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetContentRegionAvailWidth();

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetWindowContentRegionMin(out ImVec2 @out);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetWindowContentRegionMax(out ImVec2 @out);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetWindowContentRegionWidth();

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern NativeImDrawList* igGetWindowDrawList();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetWindowFontScale(float scale);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetWindowPos(out ImVec2 @out);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetWindowSize(out ImVec2 @out);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetWindowWidth();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetWindowHeight();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsWindowCollapsed();

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetNextWindowPos(ImVec2 pos, ImGuiCond cond);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetNextWindowPosCenter(ImGuiCond cond);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetNextWindowSize(ImVec2 size, ImGuiCond cond);
        public delegate void ImGuiSizeConstraintCallback(IntPtr data);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetNextWindowSizeConstraints(ImVec2 size_min, ImVec2 size_max, ImGuiSizeConstraintCallback custom_callback, void* custom_callback_data);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetNextWindowContentSize(ImVec2 size);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetNextWindowContentWidth(float width);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetNextWindowCollapsed(bool collapsed, ImGuiCond cond);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetNextWindowFocus();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetWindowPos(ImVec2 pos, ImGuiCond cond);  //(not recommended)
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetWindowSize(ImVec2 size, ImGuiCond cond); //(not recommended)
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetWindowCollapsed(bool collapsed, ImGuiCond cond); //(not recommended)
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetWindowFocus(); //(not recommended)
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetWindowPosByName(string name, ImVec2 pos, ImGuiCond cond);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetWindowSize2(string name, ImVec2 size, ImGuiCond cond);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetWindowCollapsed2(string name, bool collapsed, ImGuiCond cond);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetWindowFocus2(string name);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetScrollX();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetScrollY();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetScrollMaxX();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetScrollMaxY();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetScrollX(float scroll_x);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetScrollY(float scroll_y);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetScrollHere(float center_y_ratio = 0.5f);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetScrollFromPosY(float pos_y, float center_y_ratio = 0.5f);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetKeyboardFocusHere(int offset);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetStateStorage(ref ImGuiStorage tree);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiStorage* igGetStateStorage();

        // Parameters stacks (shared)
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPushFont(NativeImFont* font);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPopFont();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPushStyleColor(ImGuiCol idx, ImVec4 col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPopStyleColor(int count);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPushStyleVar(ImGuiStyleVar idx, float val);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPushStyleVarVec(ImGuiStyleVar idx, ImVec2 val);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPopStyleVar(int count);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern NativeImFont* igGetFont();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetFontSize();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetFontTexUvWhitePixel(ImVec2* pOut);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint igGetColorU32(ImGuiCol idx, float alpha_mul);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint igGetColorU32Vec(ImVec4* col);

        // Parameters stacks (current window)
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPushItemWidth(float item_width);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPopItemWidth();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igCalcItemWidth();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPushTextWrapPos(float wrap_pos_x);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPopTextWrapPos();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPushAllowKeyboardFocus(bool v);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPopAllowKeyboardFocus();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPushButtonRepeat(bool repeat);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPopButtonRepeat();

        // Layout
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSeparator();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSameLine(float local_pos_x, float spacing_w);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSpacing();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igDummy(ImVec2* size);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igIndent(float indent_w = 0.0f);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igUnindent(float indent_w = 0.0f);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igBeginGroup();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igEndGroup();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetCursorPos(ImVec2* pOut);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetCursorPosX();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetCursorPosY();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetCursorPos(ImVec2 local_pos);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetCursorPosX(float x);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetCursorPosY(float y);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetCursorStartPos(out ImVec2 pOut);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetCursorScreenPos(ImVec2* pOut);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetCursorScreenPos(ImVec2 pos);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igAlignFirstTextHeightToWidgets();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetTextLineHeight();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetTextLineHeightWithSpacing();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetItemsLineHeightWithSpacing();

        // Columns
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igColumns(int count, string id, bool border);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igNextColumn();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern int igGetColumnIndex();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetColumnOffset(int column_index);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetColumnOffset(int column_index, float offset_x);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetColumnWidth(int column_index);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern int igGetColumnsCount();


        // ID scopes
        // If you are creating widgets in a loop you most likely want to push a unique identifier so ImGui can differentiate them
        // You can also use "##extra" within your widget name to distinguish them from each others (see 'Programmer Guide')
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPushIdStr(string str_id);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPushIdStrRange(string str_begin, string str_end);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPushIdPtr(void* ptr_id);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPushIdInt(int int_id);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPopId();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint igGetIdStr(string str_id);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint igGetIdStrRange(string str_begin, string str_end);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint igGetIdPtr(void* ptr_id);

        // Widgets
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igText(string fmt);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igTextColored(ImVec4 col, string fmt);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igTextDisabled(string fmt);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igTextWrapped(string fmt);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igTextUnformatted(byte* text, byte* text_end);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igLabelText(string label, string fmt);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igBullet();

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igBulletText(string fmt);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igButton(string label, ImVec2 size);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igSmallButton(string label);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igInvisibleButton(string str_id, ImVec2 size);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igImage(IntPtr user_texture_id, ImVec2 size, ImVec2 uv0, ImVec2 uv1, ImVec4 tint_col, ImVec4 border_col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igImageButton(IntPtr user_texture_id, ImVec2 size, ImVec2 uv0, ImVec2 uv1, int frame_padding, ImVec4 bg_col, ImVec4 tint_col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igCheckbox(string label, ref bool v);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igCheckboxFlags(string label, UIntPtr* flags, uint flags_value);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igRadioButtonBool(string label, bool active);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igRadioButton(string label, int* v, int v_button);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igCombo(string label, ref int current_item, string[] items, int items_count, int height_in_items);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igCombo2(string label, ref int current_item, string items_separated_by_zeros, int height_in_items);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igCombo3(string label, ref int current_item, ItemSelectedCallback items_getter, IntPtr data, int items_count, int height_in_items);

        public delegate IntPtr ImGuiContextAllocationFunction(UIntPtr size);
        public delegate void ImGuiContextFreeFunction(IntPtr ptr);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr igCreateContext(ImGuiContextAllocationFunction malloc_fn, ImGuiContextFreeFunction free_fn);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igDestroyContext(IntPtr ctx);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr igGetCurrentContext();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetCurrentContext(IntPtr ctx);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igColorButton(ImVec4 col, bool small_height, bool outline_border);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igColorEdit3(string label, ImVec3* col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igColorEdit4(string label, ImVec4* col, bool show_alpha);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igColorEditMode(ImGuiColorEditMode mode);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPlotLines(string label, float* values, int values_count, int values_offset, string overlay_text, float scale_min, float scale_max, ImVec2 graph_size, int stride);
        public delegate float ImGuiPlotHistogramValuesGetter(IntPtr data, int idx);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPlotLines2(string label, ImGuiPlotHistogramValuesGetter values_getter, void* data, int values_count, int values_offset, string overlay_text, float scale_min, float scale_max, ImVec2 graph_size);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPlotHistogram(string label, float* values, int values_count, int values_offset, string overlay_text, float scale_min, float scale_max, ImVec2 graph_size, int stride);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPlotHistogram2(string label, ImGuiPlotHistogramValuesGetter values_getter, void* data, int values_count, int values_offset, string overlay_text, float scale_min, float scale_max, ImVec2 graph_size);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igProgressBar(float fraction, ImVec2* size_arg, string overlay);
        // Widgets: Sliders (tip: ctrl+click on a slider to input text)
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igSliderFloat(string label, float* v, float v_min, float v_max, string display_format, float power);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igSliderFloat(string label, ref float v, float v_min, float v_max, string display_format, float power);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igSliderFloat2(string label, ref ImVec2 v, float v_min, float v_max, string display_format, float power);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igSliderFloat3(string label, ref ImVec3 v, float v_min, float v_max, string display_format, float power);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igSliderFloat4(string label, ref ImVec4 v, float v_min, float v_max, string display_format, float power);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igSliderAngle(string label, ref float v_rad, float v_degrees_min, float v_degrees_max);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igSliderInt(string label, ref int v, int v_min, int v_max, string display_format);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igSliderInt2(string label, ref Int2 v, int v_min, int v_max, string display_format);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igSliderInt3(string label, ref Int3 v, int v_min, int v_max, string display_format);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igSliderInt4(string label, ref Int4 v, int v_min, int v_max, string display_format);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igVSliderFloat(string label, ImVec2 size, float* v, float v_min, float v_max, string display_format, float power);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igVSliderInt(string label, ImVec2 size, int* v, int v_min, int v_max, string display_format);

        // Widgets: Drags (tip: ctrl+click on a drag box to input text)
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igDragFloat(string label, ref float v, float v_speed, float v_min, float v_max, string display_format, float power);     // If v_max >= v_max we have no bound
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igDragFloat2(string label, ref ImVec2 v, float v_speed, float v_min, float v_max, string display_format, float power);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igDragFloat3(string label, ref ImVec3 v, float v_speed, float v_min, float v_max, string display_format, float power);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igDragFloat4(string label, ref ImVec4 v, float v_speed, float v_min, float v_max, string display_format, float power);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igDragFloatRange2(string label, ref float v_current_min, ref float v_current_max, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string display_format = "%.3f", string display_format_max = null, float power = 1.0f);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igDragInt(string label, ref int v, float v_speed, int v_min, int v_max, string display_format);                                       // If v_max >= v_max we have no bound
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igDragInt2(string label, ref Int2 v, float v_speed, int v_min, int v_max, string display_format);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igDragInt3(string label, ref Int3 v, float v_speed, int v_min, int v_max, string display_format);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igDragInt4(string label, ref Int4 v, float v_speed, int v_min, int v_max, string display_format);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igDragIntRange2(string label, ref int v_current_min, ref int v_current_max, float v_speed = 1.0f, int v_min = 0, int v_max = 0, string display_format = "%.0f", string display_format_max = null);


        // Widgets: Input
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igInputText(string label, IntPtr buffer, uint buf_size, ImGuiInputTextFlags flags, ImGuiTextEditCallback callback, void* user_data);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igInputTextMultiline(string label, IntPtr buffer, uint buf_size, ImVec2 size, ImGuiInputTextFlags flags, ImGuiTextEditCallback callback, void* user_data);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igInputFloat(string label, float* v, float step, float step_fast, int decimal_precision, ImGuiInputTextFlags extra_flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igInputFloat2(string label, ImVec2 v, int decimal_precision, ImGuiInputTextFlags extra_flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igInputFloat3(string label, ImVec3 v, int decimal_precision, ImGuiInputTextFlags extra_flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igInputFloat4(string label, ImVec4 v, int decimal_precision, ImGuiInputTextFlags extra_flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igInputInt(string label, int* v, int step, int step_fast, ImGuiInputTextFlags extra_flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igInputInt2(string label, Int2 v, ImGuiInputTextFlags extra_flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igInputInt3(string label, Int3 v, ImGuiInputTextFlags extra_flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igInputInt4(string label, Int4 v, ImGuiInputTextFlags extra_flags);

        // Widgets: Trees
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igTreeNode(string str_label_id);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igTreeNodeEx(string label, ImGuiTreeNodeFlags flags = 0);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igTreeNodeStr(string str_id, string fmt);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igTreeNodePtr(void* ptr_id, string fmt);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igTreePushStr(string str_id);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igTreePushPtr(void* ptr_id);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igTreePop();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igTreeAdvanceToLabelPos();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetTreeNodeToLabelSpacing();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetNextTreeNodeOpen(bool opened, ImGuiCond cond);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igCollapsingHeader(string label, ImGuiTreeNodeFlags flags = 0);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igCollapsingHeaderEx(string label, ref bool p_open, ImGuiTreeNodeFlags flags = 0);

        // Widgets: Selectable / Lists
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igSelectable(string label, bool selected, ImGuiSelectableFlags flags, ImVec2 size);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igSelectableEx(string label, ref bool p_selected, ImGuiSelectableFlags flags, ImVec2 size);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igListBox(string label, int* current_item, char** items, int items_count, int height_in_items);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igListBox2(string label, ref int currentItem, ItemSelectedCallback items_getter, IntPtr data, int items_count, int height_in_items);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igListBoxHeader(string label, ImVec2 size);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igListBoxHeader2(string label, int items_count, int height_in_items);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igListBoxFooter();

        // Widgets: Value() Helpers. Output single value in "name: value" format (tip: freely declare your own within the ImGui namespace!)
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igValueBool(string prefix, bool b);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igValueInt(string prefix, int v);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igValueUInt(string prefix, uint v);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igValueFloat(string prefix, float v, string float_format);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igValueColor(string prefix, ImVec4 v);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igValueColor2(string prefix, uint v);

        // Tooltip
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetTooltip(string fmt);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igBeginTooltip();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igEndTooltip();

        // Widgets: Menus
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igBeginMainMenuBar();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igEndMainMenuBar();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igBeginMenuBar();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igEndMenuBar();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igBeginMenu(string label, bool enabled);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igEndMenu();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igMenuItem(string label, string shortcut, bool selected, bool enabled);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igMenuItemPtr(string label, string shortcut, bool* p_selected, bool enabled);

        // Popup
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igOpenPopup(string str_id);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igBeginPopup(string str_id);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igBeginPopupModal(string name, byte* p_opened, ImGuiWindowFlags extra_flags);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igBeginPopupContextItem(string str_id, int mouse_button);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igBeginPopupContextWindow(bool also_over_items, string str_id, int mouse_button);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igBeginPopupContextVoid(string str_id, int mouse_button);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igEndPopup();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igCloseCurrentPopup();

        // Logging: all text output from interface is redirected to tty/file/clipboard. Tree nodes are automatically opened.
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igLogToTTY(int max_depth);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igLogToFile(int max_depth, string filename);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igLogToClipboard(int max_depth);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igLogFinish();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igLogButtons();

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void igLogText(string fmt, ...);
        public static extern void igLogText(string fmt);

        // Clipping
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPushClipRect(ImVec2 clip_rect_min, ImVec2 clip_rect_max, byte intersect_with_current_clip_rect);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igPopClipRect();

        // Utilities
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsItemHovered();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsItemHoveredRect();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsItemActive();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsItemClicked(int mouse_button);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsItemVisible();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsAnyItemHovered();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsAnyItemActive();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetItemRectMin(out ImVec2 pOut);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetItemRectMax(out ImVec2 pOut);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetItemRectSize(out ImVec2 pOut);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetItemAllowOverlap();

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsWindowHovered();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsWindowFocused();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsRootWindowFocused();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsRootWindowOrAnyChildFocused();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsRootWindowOrAnyChildHovered();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsRectVisible(ImVec2 item_size);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsRectVisible2(ImVec2* rect_min, ImVec2* rect_max);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsPosHoveringAnyWindow(ImVec2 pos);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float igGetTime();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern int igGetFrameCount();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern string igGetStyleColName(ImGuiCol idx);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igCalcItemRectClosestPoint(out ImVec2 pOut, ImVec2 pos, bool on_edge, float outward);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igCalcTextSize(out ImVec2 pOut, char* text, char* text_end, bool hide_text_after_double_hash, float wrap_width);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igCalcListClipping(int items_count, float items_height, ref int out_items_display_start, ref int out_items_display_end);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igBeginChildFrame(uint id, ImVec2 size, ImGuiWindowFlags extra_flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igEndChildFrame();

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igColorConvertU32ToFloat4(ImVec4* pOut, uint @in);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint igColorConvertFloat4ToU32(ImVec4 @in);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igColorConvertRGBtoHSV(float r, float g, float b, float* out_h, float* out_s, float* out_v);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igColorConvertHSVtoRGB(float h, float s, float v, float* out_r, float* out_g, float* out_b);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern int igGetKeyIndex(ImGuiKey imgui_key);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsKeyDown(int user_key_index);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsKeyPressed(int user_key_index, bool repeat);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsKeyReleased(int user_key_index);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsMouseDown(int button);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsMouseClicked(int button, bool repeat);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsMouseDoubleClicked(int button);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsMouseReleased(int button);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsMouseHoveringWindow();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsMouseHoveringAnyWindow();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsMouseHoveringRect(ImVec2 pos_min, ImVec2 pos_max, bool clip);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool igIsMouseDragging(int button, float lock_threshold);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetMousePos(out ImVec2 pOut);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetMousePosOnOpeningCurrentPopup(out ImVec2 pOut);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igGetMouseDragDelta(out ImVec2 pOut, int button, float lock_threshold);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igResetMouseDragDelta(int button);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiMouseCursor igGetMouseCursor();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetMouseCursor(ImGuiMouseCursor type);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igCaptureKeyboardFromApp();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igCaptureMouseFromApp();

        // Helpers functions to access functions pointers @in ImGui::GetIO()
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void* igMemAlloc(uint sz);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igMemFree(void* ptr);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern string igGetClipboardText();
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void igSetClipboardText(string text);

        // public state access - if you want to share ImGui state between modules (e.g. DLL) or allocate it yourself
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern string igGetVersion();
        /*
        CIMGUI_API struct ImGuiContext*    igCreateContext(void* (*malloc_fn)(size_t), void (*free_fn)(void*));
        CIMGUI_API void                    igDestroyContext(struct ImGuiContext* ctx);
        CIMGUI_API struct ImGuiContext*    igGetCurrentContext();
        CIMGUI_API void                    igSetCurrentContext(struct ImGuiContext* ctx);
        */

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImFontConfig_DefaultConstructor(ImFontConfig* config);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImFontAtlas_GetTexDataAsRGBA32(NativeImFontAtlas* atlas, byte** out_pixels, int* out_width, int* out_height, int* out_bytes_per_pixel);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImFontAtlas_GetTexDataAsAlpha8(NativeImFontAtlas* atlas, byte** out_pixels, int* out_width, int* out_height, int* out_bytes_per_pixel);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImFontAtlas_SetTexID(NativeImFontAtlas* atlas, void* id);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern NativeImFont* ImFontAtlas_AddFont(NativeImFontAtlas* atlas, ref ImFontConfig font_cfg);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern NativeImFont* ImFontAtlas_AddFontDefault(NativeImFontAtlas* atlas, IntPtr font_cfg);
        public static NativeImFont* ImFontAtlas_AddFontDefault(NativeImFontAtlas* atlas) { return ImFontAtlas_AddFontDefault(atlas, IntPtr.Zero); }

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern NativeImFont* ImFontAtlas_AddFontFromFileTTF(NativeImFontAtlas* atlas, string filename, float size_pixels, IntPtr font_cfg, char* glyph_ranges);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern NativeImFont* ImFontAtlas_AddFontFromMemoryTTF(NativeImFontAtlas* atlas, void* ttf_data, int ttf_size, float size_pixels, IntPtr font_cfg, char* glyph_ranges);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern NativeImFont* ImFontAtlas_AddFontFromMemoryCompressedTTF(NativeImFontAtlas* atlas, void* compressed_ttf_data, int compressed_ttf_size, float size_pixels, ImFontConfig* font_cfg, char* glyph_ranges);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern NativeImFont* ImFontAtlas_AddFontFromMemoryCompressedBase85TTF(NativeImFontAtlas* atlas, string compressed_ttf_data_base85, float size_pixels, ImFontConfig* font_cfg, char* glyph_ranges);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImFontAtlas_ClearTexData(NativeImFontAtlas* atlas);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImFontAtlas_Clear(NativeImFontAtlas* atlas);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern char* ImFontAtlas_GetGlyphRangesDefault(NativeImFontAtlas* atlas);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern char* ImFontAtlas_GetGlyphRangesKorean(NativeImFontAtlas* atlas);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern char* ImFontAtlas_GetGlyphRangesJapanese(NativeImFontAtlas* atlas);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern char* ImFontAtlas_GetGlyphRangesChinese(NativeImFontAtlas* atlas);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern char* ImFontAtlas_GetGlyphRangesCyrillic(NativeImFontAtlas* atlas);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern char* ImFontAtlas_GetGlyphRangesThai(NativeImFontAtlas* atlas);

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiIO_AddInputCharacter(ushort c);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiIO_AddInputCharactersUTF8(string utf8_chars);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiIO_AddInputCharactersUTF8(byte* utf8_chars);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiIO_ClearInputCharacters();

        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImDrawList_GetVertexBufferSize(NativeImDrawList* list);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImDrawVert* ImDrawList_GetVertexPtr(NativeImDrawList* list, int n);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImDrawList_GetIndexBufferSize(NativeImDrawList* list);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort* ImDrawList_GetIndexPtr(NativeImDrawList* list, int n);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImDrawList_GetCmdSize(NativeImDrawList* list);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImDrawCmd* ImDrawList_GetCmdPtr(NativeImDrawList* list, int n);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawData_DeIndexAllBuffers(NativeImDrawData* drawData);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawData_ScaleClipRects(NativeImDrawData* drawData, ImVec2 sc);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_Clear(NativeImDrawList* list);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_ClearFreeMemory(NativeImDrawList* list);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PushClipRect(NativeImDrawList* list, ImVec2 clip_rect_min, ImVec2 clip_rect_max, byte intersect_with_current_clip_rect);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PushClipRectFullScreen(NativeImDrawList* list);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PopClipRect(NativeImDrawList* list);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PushTextureID(NativeImDrawList* list, void* texture_id);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PopTextureID(NativeImDrawList* list);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddLine(NativeImDrawList* list, ImVec2 a, ImVec2 b, uint col, float thickness);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddRect(NativeImDrawList* list, ImVec2 a, ImVec2 b, uint col, float rounding, int rounding_corners_flags, float thickness);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddRectFilled(NativeImDrawList* list, ImVec2 a, ImVec2 b, uint col, float rounding, int rounding_corners_flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddRectFilledMultiColor(NativeImDrawList* list, ImVec2 a, ImVec2 b, uint col_upr_left, uint col_upr_right, uint col_bot_right, uint col_bot_left);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddQuad(NativeImDrawList* list, ImVec2 a, ImVec2 b, ImVec2 c, ImVec2 d, uint col, float thickness);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddQuadFilled(NativeImDrawList* list, ImVec2 a, ImVec2 b, ImVec2 c, ImVec2 d, uint col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddTriangle(NativeImDrawList* list, ImVec2 a, ImVec2 b, ImVec2 c, uint col, float thickness);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddTriangleFilled(NativeImDrawList* list, ImVec2 a, ImVec2 b, ImVec2 c, uint col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddCircle(NativeImDrawList* list, ImVec2 centre, float radius, uint col, int num_segments, float thickness);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddCircleFilled(NativeImDrawList* list, ImVec2 centre, float radius, uint col, int num_segments);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddText(NativeImDrawList* list, ImVec2 pos, uint col, byte* text_begin, byte* text_end);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddTextExt(NativeImDrawList* list, NativeImFont* font, float font_size, ImVec2 pos, uint col, byte* text_begin, byte* text_end, float wrap_width, ImVec4* cpu_fine_clip_rect);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddImage(NativeImDrawList* list, void* user_texture_id, ImVec2 a, ImVec2 b, ImVec2 uv_a, ImVec2 uv_b, uint col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddImageQuad(NativeImDrawList* list, void* user_texture_id, ImVec2 a, ImVec2 b, ImVec2 c, ImVec2 d, ImVec2 uv_a, ImVec2 uv_b, ImVec2 uv_c, ImVec2 uv_d, uint col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddPolyline(NativeImDrawList* list, ImVec2* points, int num_points, uint col, byte closed, float thickness, byte anti_aliased);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddConvexPolyFilled(NativeImDrawList* list, ImVec2* points, int num_points, uint col, byte anti_aliased);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddBezierCurve(NativeImDrawList* list, ImVec2 pos0, ImVec2 cp0, ImVec2 cp1, ImVec2 pos1, uint col, float thickness, int num_segments);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PathClear(NativeImDrawList* list);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PathLineTo(NativeImDrawList* list, ImVec2 pos);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PathLineToMergeDuplicate(NativeImDrawList* list, ImVec2 pos);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PathFillConvex(NativeImDrawList* list, uint col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PathStroke(NativeImDrawList* list, uint col, byte closed, float thickness);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PathArcTo(NativeImDrawList* list, ImVec2 centre, float radius, float a_min, float a_max, int num_segments);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PathArcToFast(NativeImDrawList* list, ImVec2 centre, float radius, int a_min_of_12, int a_max_of_12);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PathBezierCurveTo(NativeImDrawList* list, ImVec2 p1, ImVec2 p2, ImVec2 p3, int num_segments);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PathRect(NativeImDrawList* list, ImVec2 rect_min, ImVec2 rect_max, float rounding, int rounding_corners_flags);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_ChannelsSplit(NativeImDrawList* list, int channels_count);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_ChannelsMerge(NativeImDrawList* list);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_ChannelsSetCurrent(NativeImDrawList* list, int channel_index);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddCallback(NativeImDrawList* list, NativeImDrawCallback callback, void* callback_data);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_AddDrawCmd(NativeImDrawList* list);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PrimReserve(NativeImDrawList* list, int idx_count, int vtx_count);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PrimRect(NativeImDrawList* list, ImVec2 a, ImVec2 b, uint col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PrimRectUV(NativeImDrawList* list, ImVec2 a, ImVec2 b, ImVec2 uv_a, ImVec2 uv_b, uint col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PrimQuadUV(NativeImDrawList* list, ImVec2 a, ImVec2 b, ImVec2 c, ImVec2 d, ImVec2 uv_a, ImVec2 uv_b, ImVec2 uv_c, ImVec2 uv_d, uint col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PrimVtx(NativeImDrawList* list, ImVec2 pos, ImVec2 uv, uint col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PrimWriteVtx(NativeImDrawList* list, ImVec2 pos, ImVec2 uv, uint col);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_PrimWriteIdx(NativeImDrawList* list, ushort idx);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_UpdateClipRect(NativeImDrawList* list);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImDrawList_UpdateTextureID(NativeImDrawList* list);

        // List Clipper
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiListClipper_Begin(ImGuiListClipper* clipper, int count, float items_height);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiListClipper_End(ImGuiListClipper* clipper);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool ImGuiListClipper_Step(ImGuiListClipper* clipper);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImGuiListClipper_GetDisplayStart(ImGuiListClipper* clipper);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImGuiListClipper_GetDisplayEnd(ImGuiListClipper* clipper);

        // ImGuiTextFilter
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiTextFilter_Init(ImGuiTextFilter* filter, char* default_filter);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiTextFilter_Clear(ImGuiTextFilter* filter);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGuiTextFilter_Draw(ImGuiTextFilter* filter, char* label, float width);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGuiTextFilter_PassFilter(ImGuiTextFilter* filter, char* text, char* text_end);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGuiTextFilter_IsActive(ImGuiTextFilter* filter);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiTextFilter_Build(ImGuiTextFilter* filter);

        // ImGuiTextEditCallbackData
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiTextEditCallbackData_DeleteChars(void* data, int pos, int bytes_count);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiTextEditCallbackData_InsertChars(void* data, int pos, char* text, char* text_end);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGuiTextEditCallbackData_HasSelection(void* data);

        // ImGuiStorage
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiStorage_Init(void* storage);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiStorage_Clear(void* storage);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImGuiStorage_GetInt(void* storage, uint key, int default_val);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiStorage_SetInt(void* storage, uint key, int val);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGuiStorage_GetBool(void* storage, uint key, bool default_val);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiStorage_SetBool(void* storage, uint key, bool val);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGuiStorage_GetFloat(void* storage, uint key, float default_val);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiStorage_SetFloat(void* storage, uint key, float val);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void* ImGuiStorage_GetVoidPtr(void* storage, uint key);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiStorage_SetVoidPtr(void* storage, uint key, void* val);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern int* ImGuiStorage_GetIntRef(void* storage, uint key, int default_val);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool* ImGuiStorage_GetBoolRef(void* storage, uint key, bool default_val);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern float* ImGuiStorage_GetFloatRef(void* storage, uint key, float default_val);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void** ImGuiStorage_GetVoidPtrRef(void* storage, uint key, void* default_val);
        [DllImport(cimguiLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiStorage_SetAllInt(void* storage, int val);
    }

    public static unsafe class ImGuiNativeHelper {

        public static void CopyData(void* from, void* to, uint length) {
            uint ptrSize = (uint) IntPtr.Size;
            uint lengthAlign = (length / ptrSize) * ptrSize;
            if (ptrSize == 4) {
                for (uint i = 0; i < lengthAlign; i += ptrSize)
                    *((uint*) to + i) = *((uint*) from + i);
            } else {
                for (ulong i = 0; i < lengthAlign; i += ptrSize)
                    *((ulong*) to + i) = *((ulong*) from + i);
            }
            for (uint i = lengthAlign; i < length; i++)
                *((byte*) to + i) = *((byte*) from + i);
        }

        public static void ClearData(void* to, uint length) {
            uint ptrSize = (uint) IntPtr.Size;
            uint lengthAlign = (length / ptrSize) * ptrSize;
            if (ptrSize == 4) {
                for (uint i = 0; i < lengthAlign; i += ptrSize)
                    *((uint*) ((uint) to + i)) = 0;
            } else {
                for (ulong i = 0; i < lengthAlign; i += ptrSize)
                    *((ulong*) ((ulong) to + i)) = 0;
            }
            for (uint i = lengthAlign; i < length; i++)
                *((byte*) to + i) = 0;
        }

    }

    public delegate bool ItemSelectedCallback(IntPtr data, int index, string out_text);
    public unsafe delegate void ImDrawCallback(ref NativeImDrawList parent_list, ref ImDrawCmd cmd);
    public unsafe delegate void NativeImDrawCallback(NativeImDrawList* parent_list, ImDrawCmd* cmd);
    public unsafe delegate int ImGuiTextEditCallback(ref ImGuiTextEditCallbackData data);
}
