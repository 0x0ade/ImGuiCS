using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ImGuiNET {
    public static class ImGui {
        public static void NewFrame()
            => ImGuiNative.igNewFrame();

        public static void NewLine()
            => ImGuiNative.igNewLine();

        public static void Render()
            => ImGuiNative.igRender();

        public static void Shutdown()
            => ImGuiNative.igShutdown();

        public static void ShowUserGuide()
            => ImGuiNative.igShowUserGuide();

        public static unsafe void ShowStyleEditor(ImGuiStyle style)
            => ImGuiNative.igShowStyleEditor(style.Native);

        public static void ShowTestWindow(ref bool opened)
            => ImGuiNative.igShowTestWindow(ref opened);

        public static void ShowMetricsWindow(ref bool opened)
            => ImGuiNative.igShowMetricsWindow(ref opened);

        public static unsafe readonly ImGuiIO IO = new ImGuiIO(ImGuiNative.igGetIO());

        public static unsafe ImGuiIO GetIO()
            => IO;

        public static unsafe readonly ImGuiStyle Style = new ImGuiStyle(ImGuiNative.igGetStyle());

        public static unsafe ImGuiStyle GetStyle()
            => Style;

        public static void PushID(string id)
            => ImGuiNative.igPushIdStr(id);

        public static void PushID(int id)
            => ImGuiNative.igPushIdInt(id);

        public static void PushIDRange(string idBegin, string idEnd)
            => ImGuiNative.igPushIdStrRange(idBegin, idEnd);

        public static void PushItemWidth(float width)
            => ImGuiNative.igPushItemWidth(width);

        public static void PopItemWidth()
            => ImGuiNative.igPopItemWidth();

        public static void PopID()
            => ImGuiNative.igPopId();

        public static uint GetID(string id)
            => ImGuiNative.igGetIdStr(id);

        public static uint GetID(string idBegin, string idEnd)
            => ImGuiNative.igGetIdStrRange(idBegin, idEnd);

        public static void Text(string message)
            => ImGuiNative.igText(message);

        public static void TextColored(ImVec4 color, string message)
            => ImGuiNative.igTextColored(color, message);

        public static void TextDisabled(string text)
            => ImGuiNative.igTextDisabled(text);

        public static void TextWrapped(string text)
            => ImGuiNative.igTextWrapped(text);

        public static unsafe void TextUnformatted(string message) {
            fixed (byte* bytes = System.Text.Encoding.UTF8.GetBytes(message))
                ImGuiNative.igTextUnformatted(bytes, null);
        }

        public static void LabelText(string label, string text)
            => ImGuiNative.igLabelText(label, text);

        public static void Bullet()
            => ImGuiNative.igBullet();

        public static void BulletText(string text)
            => ImGuiNative.igBulletText(text);

        public static bool InvisibleButton(string id)
            => InvisibleButton(id, ImVec2.Zero);

        public static bool InvisibleButton(string id, ImVec2 size)
            => ImGuiNative.igInvisibleButton(id, size);

        public static void Image(int userTextureID, ImVec2 size, ImVec2 uv0, ImVec2 uv1, ImVec4 tintColor, ImVec4 borderColor)
            => ImGuiNative.igImage((IntPtr) userTextureID, size, uv0, uv1, tintColor, borderColor);

        public static bool ImageButton(
            int userTextureID,
            ImVec2 size,
            ImVec2 uv0,
            ImVec2 uv1,
            int framePadding,
            ImVec4 backgroundColor,
            ImVec4 tintColor)
            => ImGuiNative.igImageButton((IntPtr) userTextureID, size, uv0, uv1, framePadding, backgroundColor, tintColor);


        public static bool CollapsingHeader(string label, ImGuiTreeNodeFlags flags = 0)
            => ImGuiNative.igCollapsingHeader(label, flags);

        public static bool Checkbox(string label, ref bool value)
            => ImGuiNative.igCheckbox(label, ref value);

        public static unsafe bool RadioButton(string label, ref int target, int buttonValue) {
            int targetCopy = target;
            bool result = ImGuiNative.igRadioButton(label, &targetCopy, buttonValue);
            target = targetCopy;
            return result;
        }

        public static bool RadioButtonBool(string label, bool active)
            => ImGuiNative.igRadioButtonBool(label, active);

        public unsafe static bool Combo(string label, ref int current_item, string[] items)
            => ImGuiNative.igCombo(label, ref current_item, items, items.Length, 5);

        public unsafe static bool Combo(string label, ref int current_item, string[] items, int heightInItems)
            => ImGuiNative.igCombo(label, ref current_item, items, items.Length, heightInItems);

        public static bool ColorButton(ImVec4 color, bool smallHeight, bool outlineBorder)
            => ImGuiNative.igColorButton(color, smallHeight, outlineBorder);

        public static unsafe bool ColorEdit3(string label, ref float r, ref float g, ref float b, bool showAlpha) {
            ImVec3 localColor = new ImVec3(r, g, b);
            bool result = ImGuiNative.igColorEdit3(label, &localColor);
            if (result) {
                r = localColor.X;
                g = localColor.Y;
                b = localColor.Z;
            }

            return result;
        }

        public static unsafe bool ColorEdit3(string label, ref ImVec3 color, bool showAlpha) {
            ImVec3 localColor = color;
            bool result = ImGuiNative.igColorEdit3(label, &localColor);
            if (result)
                color = localColor;

            return result;
        }

        public static unsafe bool ColorEdit4(string label, ref float r, ref float g, ref float b, ref float a, bool showAlpha) {
            ImVec4 localColor = new ImVec4(r, g, b, a);
            bool result = ImGuiNative.igColorEdit4(label, &localColor, showAlpha);
            if (result) {
                r = localColor.X;
                g = localColor.Y;
                b = localColor.Z;
                a = localColor.W;
            }

            return result;
        }

        public static unsafe bool ColorEdit4(string label, ref ImVec4 color, bool showAlpha) {
            ImVec4 localColor = color;
            bool result = ImGuiNative.igColorEdit4(label, &localColor, showAlpha);
            if (result)
                color = localColor;

            return result;
        }

        public static void ColorEditMode(ImGuiColorEditMode mode)
            => ImGuiNative.igColorEditMode(mode);

        public unsafe static void PlotLines(
            string label,
            float[] values,
            int valuesOffset,
            string overlayText,
            float scaleMin,
            float scaleMax,
            ImVec2 graphSize,
            int stride) {
            fixed (float* valuesBasePtr = values) {
                ImGuiNative.igPlotLines(
                    label,
                    valuesBasePtr,
                    values.Length,
                    valuesOffset,
                    overlayText,
                    scaleMin,
                    scaleMax,
                    graphSize,
                    stride);
            }
        }

        public unsafe static void PlotHistogram(string label, float[] values, int valuesOffset, string overlayText, float scaleMin, float scaleMax, ImVec2 graphSize, int stride) {
            fixed (float* valuesBasePtr = values) {
                ImGuiNative.igPlotHistogram(
                    label,
                    valuesBasePtr,
                    values.Length,
                    valuesOffset,
                    overlayText,
                    scaleMin,
                    scaleMax,
                    graphSize,
                    stride);
            }
        }

        public static bool SliderFloat(string sliderLabel, ref float value, float min, float max, string displayText, float power)
            => ImGuiNative.igSliderFloat(sliderLabel, ref value, min, max, displayText, power);

        public static bool SliderImVec2(string label, ref ImVec2 value, float min, float max, string displayText, float power)
            => ImGuiNative.igSliderFloat2(label, ref value, min, max, displayText, power);

        public static bool SliderImVec3(string label, ref ImVec3 value, float min, float max, string displayText, float power)
            => ImGuiNative.igSliderFloat3(label, ref value, min, max, displayText, power);

        public static bool SliderImVec4(string label, ref ImVec4 value, float min, float max, string displayText, float power)
            => ImGuiNative.igSliderFloat4(label, ref value, min, max, displayText, power);

        public static bool SliderAngle(string label, ref float radians, float minDegrees, float maxDegrees)
            => ImGuiNative.igSliderAngle(label, ref radians, minDegrees, maxDegrees);

        public static bool SliderInt(string sliderLabel, ref int value, int min, int max, string displayText)
            => ImGuiNative.igSliderInt(sliderLabel, ref value, min, max, displayText);

        public static bool SliderInt2(string label, ref Int2 value, int min, int max, string displayText)
            => ImGuiNative.igSliderInt2(label, ref value, min, max, displayText);

        public static bool SliderInt3(string label, ref Int3 value, int min, int max, string displayText)
            => ImGuiNative.igSliderInt3(label, ref value, min, max, displayText);

        public static bool SliderInt4(string label, ref Int4 value, int min, int max, string displayText)
            => ImGuiNative.igSliderInt4(label, ref value, min, max, displayText);

        public static bool DragFloat(string label, ref float value, float min, float max, float dragSpeed = 1f, string displayFormat = "%f", float dragPower = 1f)
            => ImGuiNative.igDragFloat(label, ref value, dragSpeed, min, max, displayFormat, dragPower);

        public static bool DragImVec2(string label, ref ImVec2 value, float min, float max, float dragSpeed = 1f, string displayFormat = "%f", float dragPower = 1f)
            => ImGuiNative.igDragFloat2(label, ref value, dragSpeed, min, max, displayFormat, dragPower);

        public static bool DragImVec3(string label, ref ImVec3 value, float min, float max, float dragSpeed = 1f, string displayFormat = "%f", float dragPower = 1f)
            => ImGuiNative.igDragFloat3(label, ref value, dragSpeed, min, max, displayFormat, dragPower);

        public static bool DragImVec4(string label, ref ImVec4 value, float min, float max, float dragSpeed = 1f, string displayFormat = "%f", float dragPower = 1f)
            => ImGuiNative.igDragFloat4(label, ref value, dragSpeed, min, max, displayFormat, dragPower);

        public static bool DragFloatRange2(
            string label,
            ref float currentMinValue,
            ref float currentMaxValue,
            float speed = 1.0f,
            float minValueLimit = 0.0f,
            float maxValueLimit = 0.0f,
            string displayFormat = "%.3f",
            string displayFormatMax = null,
            float power = 1.0f)
            => ImGuiNative.igDragFloatRange2(label, ref currentMinValue, ref currentMaxValue, speed, minValueLimit, maxValueLimit, displayFormat, displayFormatMax, power);

        public static bool DragInt(string label, ref int value, float speed, int minValue, int maxValue, string displayText)
            => ImGuiNative.igDragInt(label, ref value, speed, minValue, maxValue, displayText);

        public static bool DragInt2(string label, ref Int2 value, float speed, int minValue, int maxValue, string displayText)
            => ImGuiNative.igDragInt2(label, ref value, speed, minValue, maxValue, displayText);

        public static bool DragInt3(string label, ref Int3 value, float speed, int minValue, int maxValue, string displayText)
            => ImGuiNative.igDragInt3(label, ref value, speed, minValue, maxValue, displayText);

        public static bool DragInt4(string label, ref Int4 value, float speed, int minValue, int maxValue, string displayText)
            => ImGuiNative.igDragInt4(label, ref value, speed, minValue, maxValue, displayText);

        public static bool DragIntRange2(
            string label,
            ref int currentMinValue,
            ref int currentMaxValue,
            float speed = 1.0f,
            int minLimit = 0,
            int maxLimit = 0,
            string displayFormat = "%.0f",
            string displayFormatMax = null) {
            return ImGuiNative.igDragIntRange2(
                label,
                ref currentMinValue,
                ref currentMaxValue,
                speed,
                minLimit,
                maxLimit,
                displayFormat,
                displayFormatMax);
        }

        public static bool Button(string message)
            => ImGuiNative.igButton(message, ImVec2.Zero);

        public static bool Button(string message, ImVec2 size)
            => ImGuiNative.igButton(message, size);

        public static void SetNextWindowSize(ImVec2 size, ImGuiCond condition)
            => ImGuiNative.igSetNextWindowSize(size, condition);

        public static void SetNextWindowFocus()
            => ImGuiNative.igSetNextWindowFocus();

        public static void SetNextWindowPos(ImVec2 position, ImGuiCond condition)
            => ImGuiNative.igSetNextWindowPos(position, condition);

        public static void SetNextWindowPosCenter(ImGuiCond condition)
            => ImGuiNative.igSetNextWindowPosCenter(condition);

        public static void AddInputCharacter(char keyChar)
            => ImGuiNative.ImGuiIO_AddInputCharacter(keyChar);

        public static void AddInputCharactersUTF8(string utf8_chars)
            => ImGuiNative.ImGuiIO_AddInputCharactersUTF8(utf8_chars);

        public static unsafe void AddInputCharactersUTF8(byte* utf8_chars)
            => ImGuiNative.ImGuiIO_AddInputCharactersUTF8(utf8_chars);

        /// <summary>
        /// Helper to scale the ClipRect field of each ImDrawCmd.
        /// Use if your final output buffer is at a different scale than ImGui expects,
        /// or if there is a difference between your window resolution and framebuffer resolution.
        /// </summary>
        /// <param name="drawData">Pointer to the DrawData to scale.</param>
        /// <param name="scale">The scale to apply.</param>
        public static unsafe void ScaleClipRects(ImDrawData drawData, ImVec2 scale) {
            for (int i = 0; i < drawData.CmdListsCount; i++) {
                NativeImDrawList* cmd_list = drawData.CmdLists[i];
                for (int cmd_i = 0; cmd_i < cmd_list->CmdBuffer.Size; cmd_i++) {
                    ImDrawCmd* drawCmdList = (ImDrawCmd*) cmd_list->CmdBuffer.Data;
                    ImDrawCmd* cmd = &drawCmdList[cmd_i];
                    cmd->ClipRect = new ImVec4(cmd->ClipRect.X * scale.X, cmd->ClipRect.Y * scale.Y, cmd->ClipRect.Z * scale.X, cmd->ClipRect.W * scale.Y);
                }
            }
        }

        public static float GetWindowHeight()
            => ImGuiNative.igGetWindowHeight();


        public static float GetWindowWidth()
            => ImGuiNative.igGetWindowWidth();

        public static ImVec2 GetWindowSize() {
            ImVec2 size;
            ImGuiNative.igGetWindowSize(out size);
            return size;
        }

        public static ImVec2 GetWindowPosition() {
            ImVec2 pos;
            ImGuiNative.igGetWindowPos(out pos);
            return pos;
        }

        public static void SetWindowPos(ImVec2 size, ImGuiCond cond = 0)
            => ImGuiNative.igSetWindowPos(size, cond);

        public static void SetWindowSize(ImVec2 size, ImGuiCond cond = 0)
            => ImGuiNative.igSetWindowSize(size, cond);

        public static unsafe ImDrawList GetWindowDrawList()
            => new ImDrawList(ImGuiNative.igGetWindowDrawList());

        public static bool Begin(string windowTitle)
            => Begin(windowTitle, ImGuiWindowFlags.Default);

        public static bool Begin(string windowTitle, ImGuiWindowFlags flags) {
            bool opened = true;
            return ImGuiNative.igBegin(windowTitle, ref opened, flags);
        }

        public static bool Begin(string windowTitle, ref bool opened, ImGuiWindowFlags flags = ImGuiWindowFlags.Default)
            => ImGuiNative.igBegin(windowTitle, ref opened, flags);

        public static bool Begin(string windowTitle, ref bool opened, float backgroundAlpha, ImGuiWindowFlags flags = ImGuiWindowFlags.Default)
            => ImGuiNative.igBegin2(windowTitle, ref opened, new ImVec2(), backgroundAlpha, flags);

        public static bool Begin(string windowTitle, ref bool opened, ImVec2 startingSize, ImGuiWindowFlags flags = ImGuiWindowFlags.Default)
            => ImGuiNative.igBegin2(windowTitle, ref opened, startingSize, 1f, flags);

        public static bool Begin(string windowTitle, ref bool opened, ImVec2 startingSize, float backgroundAlpha, ImGuiWindowFlags flags = ImGuiWindowFlags.Default)
            => ImGuiNative.igBegin2(windowTitle, ref opened, startingSize, backgroundAlpha, flags);

        public static bool BeginMenu(string label)
            => ImGuiNative.igBeginMenu(label, true);

        public static bool BeginMenu(string label, bool enabled)
            => ImGuiNative.igBeginMenu(label, enabled);

        public static bool BeginMenuBar()
            => ImGuiNative.igBeginMenuBar();

        public static void CloseCurrentPopup()
            => ImGuiNative.igCloseCurrentPopup();

        public static void EndMenuBar()
            => ImGuiNative.igEndMenuBar();

        public static void EndMenu()
            => ImGuiNative.igEndMenu();

        public static void Separator()
            => ImGuiNative.igSeparator();

        public static bool MenuItem(string label)
            => MenuItem(label, string.Empty, false, true);

        public static bool MenuItem(string label, string shortcut)
            => MenuItem(label, shortcut, false, true);

        public static bool MenuItem(string label, bool enabled)
            => MenuItem(label, string.Empty, false, enabled);

        public static bool MenuItem(string label, string shortcut, bool selected, bool enabled)
            => ImGuiNative.igMenuItem(label, shortcut, selected, enabled);

        public static unsafe bool InputText(string label, byte[] textBuffer, uint bufferSize, ImGuiInputTextFlags flags = ImGuiInputTextFlags.Default, ImGuiTextEditCallback textEditCallback = null)
            => InputText(label, textBuffer, bufferSize, flags, textEditCallback, IntPtr.Zero);

        public static unsafe bool InputText(string label, byte[] textBuffer, uint bufferSize, ImGuiInputTextFlags flags, ImGuiTextEditCallback textEditCallback, IntPtr userData) {
            Debug.Assert(bufferSize <= textBuffer.Length);
            fixed (byte* ptrBuf = textBuffer)
                return InputText(label, new IntPtr(ptrBuf), bufferSize, flags, textEditCallback, userData);
        }

        public static unsafe bool InputText(string label, IntPtr textBuffer, uint bufferSize, ImGuiInputTextFlags flags = ImGuiInputTextFlags.Default, ImGuiTextEditCallback textEditCallback = null)
            => InputText(label, textBuffer, bufferSize, flags, textEditCallback, IntPtr.Zero);

        public static unsafe bool InputText(string label, IntPtr textBuffer, uint bufferSize, ImGuiInputTextFlags flags, ImGuiTextEditCallback textEditCallback, IntPtr userData)
            => ImGuiNative.igInputText(label, textBuffer, bufferSize, flags, textEditCallback, userData.ToPointer());

        public static void End()
            => ImGuiNative.igEnd();

        public static void PushStyleColor(ImGuiCol target, ImVec4 color)
            => ImGuiNative.igPushStyleColor(target, color);

        public static void PopStyleColor()
            => PopStyleColor(1);

        public static void PopStyleColor(int numStyles)
            => ImGuiNative.igPopStyleColor(numStyles);

        public static void PushStyleVar(ImGuiStyleVar var, float value)
            => ImGuiNative.igPushStyleVar(var, value);
        public static void PushStyleVar(ImGuiStyleVar var, ImVec2 value)
            => ImGuiNative.igPushStyleVarVec(var, value);

        public static void PopStyleVar()
            => ImGuiNative.igPopStyleVar(1);
        public static void PopStyleVar(int count)
            => ImGuiNative.igPopStyleVar(count);

        public static unsafe void InputTextMultiline(string label, IntPtr textBuffer, uint bufferSize, ImVec2 size, ImGuiInputTextFlags flags, ImGuiTextEditCallback callback)
            => ImGuiNative.igInputTextMultiline(label, textBuffer, bufferSize, size, flags, callback, null);

        public static unsafe ImDrawData GetDrawData()
            => new ImDrawData(ImGuiNative.igGetDrawData());

        public static unsafe void InputTextMultiline(string label, IntPtr textBuffer, uint bufferSize, ImVec2 size, ImGuiInputTextFlags flags, ImGuiTextEditCallback callback, IntPtr userData)
            => ImGuiNative.igInputTextMultiline(label, textBuffer, bufferSize, size, flags, callback, userData.ToPointer());

        public static bool BeginChildFrame(uint id, ImVec2 size, ImGuiWindowFlags flags)
            => ImGuiNative.igBeginChildFrame(id, size, flags);

        public static void EndChildFrame()
            => ImGuiNative.igEndChildFrame();

        public static unsafe void ColorConvertU32ToFloat4(ref ImVec4 pOut, uint @in) {
            fixed (ImVec4* ptr = &pOut)
                ImGuiNative.igColorConvertU32ToFloat4(ptr, @in);
        }

        public static unsafe uint ColorConvertFloat4ToU32(ImVec4 @in)
            => ImGuiNative.igColorConvertFloat4ToU32(@in);

        public static unsafe void ColorConvertRGBToHSV(float r, float g, float b, out float h, out float s, out float v) {
            float h2, s2, v2;
            ImGuiNative.igColorConvertRGBtoHSV(r, g, b, &h2, &s2, &v2);
            h = h2;
            s = s2;
            v = v2;
        }

        public static unsafe void ColorConvertHSVToRGB(float h, float s, float v, out float r, out float g, out float b) {
            float r2, g2, b2;
            ImGuiNative.igColorConvertHSVtoRGB(h, s, v, &r2, &g2, &b2);
            r = r2;
            g = g2;
            b = b2;
        }


        public static int GetKeyIndex(ImGuiKey key)
            => ImGuiNative.igGetKeyIndex(key);

        public static bool IsKeyDown(int keyIndex)
            => ImGuiNative.igIsKeyDown(keyIndex);

        public static bool IsKeyPressed(int keyIndex, bool repeat = true)
            => ImGuiNative.igIsKeyPressed(keyIndex, repeat);

        public static bool IsKeyReleased(int keyIndex)
            => ImGuiNative.igIsKeyReleased(keyIndex);

        public static bool IsMouseDown(int button)
            => ImGuiNative.igIsMouseDown(button);

        public static bool IsMouseClicked(int button, bool repeat = false)
            => ImGuiNative.igIsMouseClicked(button, repeat);

        public static bool IsMouseDoubleClicked(int button)
            => ImGuiNative.igIsMouseDoubleClicked(button);

        public static bool IsMouseReleased(int button)
            => ImGuiNative.igIsMouseReleased(button);

        public static bool IsMouseHoveringWindow()
            => ImGuiNative.igIsMouseHoveringWindow();

        public static bool IsMouseHoveringAnyWindow()
            => ImGuiNative.igIsMouseHoveringAnyWindow();

        public static bool IsWindowFocused()
            => ImGuiNative.igIsWindowFocused();

        public static bool IsMouseHoveringRect(ImVec2 minPosition, ImVec2 maxPosition, bool clip)
            => ImGuiNative.igIsMouseHoveringRect(minPosition, maxPosition, clip);

        public static bool IsMouseDragging(int button, float lockThreshold)
            => ImGuiNative.igIsMouseDragging(button, lockThreshold);

        public static ImVec2 GetMousePos() {
            ImVec2 retVal;
            ImGuiNative.igGetMousePos(out retVal);
            return retVal;
        }

        public static ImVec2 GetMousePosOnOpeningCurrentPopup() {
            ImVec2 retVal;
            ImGuiNative.igGetMousePosOnOpeningCurrentPopup(out retVal);
            return retVal;
        }

        public static ImVec2 GetMouseDragDelta(int button, float lockThreshold) {
            ImVec2 retVal;
            ImGuiNative.igGetMouseDragDelta(out retVal, button, lockThreshold);
            return retVal;
        }

        public static void ResetMouseDragDelta(int button)
            => ImGuiNative.igResetMouseDragDelta(button);

        public static ImGuiMouseCursor MouseCursor {
            get {
                return ImGuiNative.igGetMouseCursor();
            }
            set {
                ImGuiNative.igSetMouseCursor(value);
            }
        }

        public static ImVec2 GetCursorStartPos() {
            ImVec2 retVal;
            ImGuiNative.igGetCursorStartPos(out retVal);
            return retVal;
        }

        public static unsafe ImVec2 GetCursorScreenPos() {
            ImVec2 retVal;
            ImGuiNative.igGetCursorScreenPos(&retVal);
            return retVal;
        }

        public static void SetCursorScreenPos(ImVec2 pos)
            => ImGuiNative.igSetCursorScreenPos(pos);

        public static bool BeginChild(string id, bool border = false, ImGuiWindowFlags flags = 0)
            => BeginChild(id, new ImVec2(0, 0), border, flags);

        public static bool BeginChild(string id, ImVec2 size, bool border, ImGuiWindowFlags flags)
            => ImGuiNative.igBeginChild(id, size, border, flags);

        public static bool BeginChild(uint id, ImVec2 size, bool border, ImGuiWindowFlags flags)
            => ImGuiNative.igBeginChildEx(id, size, border, flags);

        public static void EndChild()
            => ImGuiNative.igEndChild();

        public static ImVec2 GetContentRegionMax() {
            ImVec2 value;
            ImGuiNative.igGetContentRegionMax(out value);
            return value;
        }

        public static ImVec2 GetContentRegionAvailable() {
            ImVec2 value;
            ImGuiNative.igGetContentRegionAvail(out value);
            return value;
        }

        public static float GetContentRegionAvailableWidth()
            => ImGuiNative.igGetContentRegionAvailWidth();

        public static ImVec2 GetWindowContentRegionMin() {
            ImVec2 value;
            ImGuiNative.igGetWindowContentRegionMin(out value);
            return value;
        }

        public static ImVec2 GetWindowContentRegionMax() {
            ImVec2 value;
            ImGuiNative.igGetWindowContentRegionMax(out value);
            return value;
        }

        public static float GetWindowContentRegionWidth()
            => ImGuiNative.igGetWindowContentRegionWidth();

        public static bool Selectable(string label)
            => Selectable(label, false);

        public static bool Selectable(string label, bool isSelected)
            => Selectable(label, isSelected, ImGuiSelectableFlags.Default);

        public static bool BeginMainMenuBar()
            => ImGuiNative.igBeginMainMenuBar();

        public static bool BeginPopup(string id)
            => ImGuiNative.igBeginPopup(id);

        public static void EndMainMenuBar()
            => ImGuiNative.igEndMainMenuBar();

        public static bool SmallButton(string label)
            => ImGuiNative.igSmallButton(label);

        public static bool BeginPopupModal(string name)
            => BeginPopupModal(name, ImGuiWindowFlags.Default);

        public static bool BeginPopupModal(string name, ref bool opened)
            => BeginPopupModal(name, ref opened, ImGuiWindowFlags.Default);

        public static unsafe bool BeginPopupModal(string name, ImGuiWindowFlags extra_flags)
            => ImGuiNative.igBeginPopupModal(name, null, extra_flags);

        public static unsafe bool BeginPopupModal(string name, ref bool p_opened, ImGuiWindowFlags extra_flags) {
            byte value = p_opened ? (byte) 1 : (byte) 0;
            bool result = ImGuiNative.igBeginPopupModal(name, &value, extra_flags);

            p_opened = value == 1 ? true : false;
            return result;
        }

        public static bool Selectable(string label, bool isSelected, ImGuiSelectableFlags flags)
            => Selectable(label, isSelected, flags, new ImVec2());

        public static bool Selectable(string label, bool isSelected, ImGuiSelectableFlags flags, ImVec2 size)
            => ImGuiNative.igSelectable(label, isSelected, flags, size);

        public static bool SelectableEx(string label, ref bool isSelected)
            => ImGuiNative.igSelectableEx(label, ref isSelected, ImGuiSelectableFlags.Default, new ImVec2());

        public static bool SelectableEx(string label, ref bool isSelected, ImGuiSelectableFlags flags, ImVec2 size)
            => ImGuiNative.igSelectableEx(label, ref isSelected, flags, size);

        public static unsafe ImVec2 GetTextSize(string text, float wrapWidth = int.MaxValue) {
            ImVec2 result;
            IntPtr buffer = Marshal.StringToHGlobalAnsi(text);
            byte* textStart = (byte*) buffer.ToPointer();
            byte* textEnd = textStart + text.Length;
            ImGuiNative.igCalcTextSize(out result, (char*) textStart, (char*) textEnd, false, wrapWidth);
            Marshal.FreeHGlobal(buffer);
            return result;
        }

        public static bool BeginPopupContextItem(string id)
            => BeginPopupContextItem(id, 1);

        public static bool BeginPopupContextItem(string id, int mouseButton)
            => ImGuiNative.igBeginPopupContextItem(id, mouseButton);

        public static unsafe void Dummy(float width, float height)
            => Dummy(new ImVec2(width, height));

        public static void EndPopup()
            => ImGuiNative.igEndPopup();

        public static unsafe void Dummy(ImVec2 size)
            => ImGuiNative.igDummy(&size);

        public static void Spacing()
            => ImGuiNative.igSpacing();

        public static void Columns(int count = 1, string id = null, bool border = true)
            => ImGuiNative.igColumns(count, id, border);

        public static void NextColumn()
            => ImGuiNative.igNextColumn();

        public static int GetColumnIndex()
            => ImGuiNative.igGetColumnIndex();

        public static float GetColumnOffset(int columnIndex)
            => ImGuiNative.igGetColumnOffset(columnIndex);

        public static void SetColumnOffset(int columnIndex, float offsetX)
            => ImGuiNative.igSetColumnOffset(columnIndex, offsetX);

        public static float GetColumnWidth(int columnIndex)
            => ImGuiNative.igGetColumnWidth(columnIndex);

        public static int GetColumnsCount()
            => ImGuiNative.igGetColumnsCount();

        public static void OpenPopup(string id)
            => ImGuiNative.igOpenPopup(id);

        public static void SameLine(float localPositionX = 0, float spacingW = -1.0f)
            => ImGuiNative.igSameLine(localPositionX, spacingW);

        public static void PushClipRect(ImVec2 min, ImVec2 max, bool intersectWithCurrentCliRect)
            => ImGuiNative.igPushClipRect(min, max, intersectWithCurrentCliRect ? (byte) 1 : (byte) 0);

        public static void PopClipRect()
            => ImGuiNative.igPopClipRect();

        public static bool IsLastItemHovered()
            => ImGuiNative.igIsItemHovered();

        public static bool IsLastItemHoveredRect()
            => ImGuiNative.igIsItemHoveredRect();

        public static bool IsLastItemActive()
            => ImGuiNative.igIsItemActive();

        public static bool IsLastItemVisible()
            => ImGuiNative.igIsItemVisible();

        public static bool IsAnyItemHovered()
            => ImGuiNative.igIsAnyItemHovered();

        public static bool IsAnyItemActive()
            => ImGuiNative.igIsAnyItemActive();

        public static void SetTooltip(string text)
            => ImGuiNative.igSetTooltip(text);

        public static void SetNextTreeNodeOpen(bool opened)
            => ImGuiNative.igSetNextTreeNodeOpen(opened, ImGuiCond.Always);

        public static void SetNextTreeNodeOpen(bool opened, ImGuiCond setCondition)
            => ImGuiNative.igSetNextTreeNodeOpen(opened, setCondition);

        public static bool TreeNode(string label)
            => ImGuiNative.igTreeNode(label);

        public static bool TreeNodeEx(string label, ImGuiTreeNodeFlags flags = 0)
            => ImGuiNative.igTreeNodeEx(label, flags);

        public static void TreePop()
            => ImGuiNative.igTreePop();

        public static ImVec2 GetLastItemRectSize() {
            ImVec2 result;
            ImGuiNative.igGetItemRectSize(out result);
            return result;
        }

        public static ImVec2 GetLastItemRectMin() {
            ImVec2 result;
            ImGuiNative.igGetItemRectMin(out result);
            return result;
        }

        public static ImVec2 GetLastItemRectMax() {
            ImVec2 result;
            ImGuiNative.igGetItemRectMax(out result);
            return result;
        }

        public static void SetWindowFontScale(float scale)
            => ImGuiNative.igSetWindowFontScale(scale);

        public static void SetScrollHere()
            => ImGuiNative.igSetScrollHere();

        public static void SetScrollHere(float centerYRatio)
            => ImGuiNative.igSetScrollHere(centerYRatio);

        public static unsafe void PushFont(ImFont font)
            => ImGuiNative.igPushFont(font.Native);

        public static void PopFont()
            => ImGuiNative.igPopFont();

        public static void SetKeyboardFocusHere()
            => ImGuiNative.igSetKeyboardFocusHere(0);

        public static void SetKeyboardFocusHere(int offset)
            => ImGuiNative.igSetKeyboardFocusHere(offset);

        public static void CalcListClipping(int itemsCount, float itemsHeight, ref int outItemsDisplayStart, ref int outItemsDisplayEnd)
            => ImGuiNative.igCalcListClipping(itemsCount, itemsHeight, ref outItemsDisplayStart, ref outItemsDisplayEnd);

        public static void AlignFirstTextHeightToWidgets()
            => ImGuiNative.igAlignFirstTextHeightToWidgets();

    }
}
