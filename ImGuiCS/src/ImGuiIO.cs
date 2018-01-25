using System;
using System.Runtime.InteropServices;

namespace ImGuiNET {
    public delegate string RenderDrawListsFn_(IntPtr data);
    public delegate string RenderDrawListsFn(ImDrawData data);
    public delegate string GetClipboardTextFn(IntPtr user_data);
    public delegate void SetClipboardTextFn(IntPtr user_data, string text);
    public unsafe delegate void* MemAllocFn(void* sz);
    public unsafe delegate void MemFreeFn(void* ptr);
    public unsafe struct ImGuiIO {

        public readonly NativeImGuiIO* Native;

        internal ImGuiIO(NativeImGuiIO* native) {
            Native = native;
            MouseDown = new MouseDownStates(native);
            KeyMap = new KeyMap(Native);
            KeysDown = new KeyDownStates(Native);
            FontAtlas = new ImFontAtlas(Native->FontAtlas);
        }

        /// <summary>
        /// Display size, in pixels. For clamping windows positions.
        /// Default value: [unset]
        /// </summary>
        public ImVec2 DisplaySize {
            get { return Native->DisplaySize; }
            set { Native->DisplaySize = value; }
        }

        /// <summary>
        /// Time elapsed since last frame, in seconds.
        /// Default value: 1.0f / 10.0f.
        /// </summary>
        public float DeltaTime {
            get { return Native->DeltaTime; }
            set { Native->DeltaTime = value; }
        }

        /// <summary>
        /// Mouse is hovering a window or widget is active (= ImGui will use your mouse input).
        /// </summary>
        public bool WantCaptureMouse {
            get { return Native->WantCaptureMouse == 1; }
        }

        /// <summary>
        /// Widget is active (= ImGui will use your keyboard input).
        /// </summary>
        public bool WantCaptureKeyboard {
            get { return Native->WantCaptureKeyboard == 1; }
        }

        /// <summary>
        /// Some text input widget is active, which will read input characters from the InputCharacters array.
        /// </summary>
        public bool WantTextInput {
            get { return Native->WantTextInput == 1; }
        }

        /// <summary>
        /// Application framerate estimation, in frame per second. Solely for convenience.
        /// Rolling average estimation based on IO.DeltaTime over 120 frames
        /// </summary>
        public float Framerate {
            get { return Native->Framerate; }
        }

        /// <summary>
        /// For retina display or other situations where window coordinates are different from framebuffer coordinates.
        /// User storage only, presently not used by ImGui.
        /// Default value: (1.0f, 1.0f).
        /// </summary>
        public ImVec2 DisplayFramebufferScale {
            get { return Native->DisplayFramebufferScale; }
            set { Native->DisplayFramebufferScale = value; }
        }

        /// <summary>
        /// Mouse position, in pixels (set to -1,-1 if no mouse / on another screen, etc.).
        /// </summary>
        public ImVec2 MousePosition {
            get { return Native->MousePos; }
            set { Native->MousePos = value; }
        }

        /// <summary>
        /// Mouse wheel: 1 unit scrolls about 5 lines text.
        /// </summary>
        public float MouseWheel {
            get { return Native->MouseWheel; }
            set { Native->MouseWheel = value; }
        }

        /// <summary>
        /// Request ImGui to draw a mouse cursor for you (if you are on a platform without a mouse cursor).
        /// </summary>
        public bool MouseDrawCursor {
            get { return Native->MouseDrawCursor == 1; }
            set { Native->MouseDrawCursor = value ? (byte) 1 : (byte) 0; }
        }

        /// <summary>
        /// Mouse buttons: left, right, middle + extras.
        /// ImGui itself mostly only uses left button (BeginPopupContext** are using right button).
        /// Others buttons allows us to track if the mouse is being used by your application + available to user as a convenience via IsMouse** API.
        /// </summary>
        public MouseDownStates MouseDown { get; }

        /// <summary>
        /// Map of indices into the KeysDown[512] entries array.
        /// Default values: [unset]
        /// </summary>
        public KeyMap KeyMap { get; }

        /// <summary>
        /// Keyboard keys that are pressed (in whatever storage order you naturally have access to keyboard data)
        /// </summary>
        public KeyDownStates KeysDown { get; }

        public ImFontAtlas FontAtlas { get; }

        public bool FontAllowUserScaling {
            get { return Native->FontAllowUserScaling != 0; }
            set { Native->FontAllowUserScaling = value ? (byte) 1 : (byte) 0; }
        }

        /// <summary>
        /// Keyboard modifier pressed: Control.
        /// </summary>
        public bool CtrlPressed {
            get { return Native->KeyCtrl == 1; }
            set { Native->KeyCtrl = value ? (byte) 1 : (byte) 0; }
        }

        /// <summary>
        /// Keyboard modifier pressed: Shift
        /// </summary>
        public bool ShiftPressed {
            get { return Native->KeyShift == 1; }
            set { Native->KeyShift = value ? (byte) 1 : (byte) 0; }
        }

        /// <summary>
        /// Keyboard modifier pressed: Alt
        /// </summary>
        public bool AltPressed {
            get { return Native->KeyAlt == 1; }
            set { Native->KeyAlt = value ? (byte) 1 : (byte) 0; }
        }

        /// <summary>
        /// Keyboard modifier pressed: Cmd/Super/Windows
        /// </summary>
        public bool SuperPressed {
            get { return Native->KeySuper == 1; }
            set { Native->KeySuper = value ? (byte) 1 : (byte) 0; }
        }

        /// <summary>
        /// Rendering function, will be called in Render().
        /// Alternatively you can keep this to NULL and call GetDrawData() after Render() to get the same pointer.
        /// See example applications if you are unsure of how to implement this.
        /// </summary>
        public IntPtr RenderDrawListsFn {
            get { return Native->RenderDrawListsFn; }
            set { Native->RenderDrawListsFn = value; }
        }
        public RenderDrawListsFn GetRenderDrawListsFn() {
            RenderDrawListsFn_ native = (RenderDrawListsFn_) Marshal.GetDelegateForFunctionPointer(GetClipboardTextFn, typeof(RenderDrawListsFn_));
            return data => native((IntPtr) data.Native);
        }
        public IntPtr SetRenderDrawListsFn(RenderDrawListsFn fn)
            => RenderDrawListsFn = Marshal.GetFunctionPointerForDelegate((RenderDrawListsFn_) (ptr => fn(new ImDrawData((NativeImDrawData*) ptr))));

        /// <summary>
        /// Optional: access OS clipboard
        /// (default to use native Win32 clipboard on Windows, otherwise uses a private clipboard. Override to access OS clipboard on other architectures)
        /// </summary>
        public IntPtr GetClipboardTextFn {
            get { return Native->GetClipboardTextFn; }
            set { Native->GetClipboardTextFn = value; }
        }
        public GetClipboardTextFn GetGetClipboardTextFn()
            => (GetClipboardTextFn) Marshal.GetDelegateForFunctionPointer(GetClipboardTextFn, typeof(GetClipboardTextFn));
        public IntPtr SetGetClipboardTextFn(GetClipboardTextFn fn)
            => GetClipboardTextFn = Marshal.GetFunctionPointerForDelegate(fn);

        /// <summary>
        /// Optional: access OS clipboard
        /// (default to use native Win32 clipboard on Windows, otherwise uses a private clipboard. Override to access OS clipboard on other architectures)
        /// </summary>
        public IntPtr SetClipboardTextFn {
            get { return Native->SetClipboardTextFn; }
            set { Native->SetClipboardTextFn = value; }
        }
        public SetClipboardTextFn GetSetClipboardTextFn()
            => (SetClipboardTextFn) Marshal.GetDelegateForFunctionPointer(SetClipboardTextFn, typeof(SetClipboardTextFn));
        public IntPtr SetSetClipboardTextFn(SetClipboardTextFn fn)
            => SetClipboardTextFn = Marshal.GetFunctionPointerForDelegate(fn);

        /// <summary>
        /// Optional: access OS clipboard
        /// (default to use native Win32 clipboard on Windows, otherwise uses a private clipboard. Override to access OS clipboard on other architectures)
        /// </summary>
        public IntPtr ClipboardUserData {
            get { return Native->ClipboardUserData; }
            set { Native->ClipboardUserData = value; }
        }

        /// <summary>
        /// Optional: override memory allocations. MemFreeFn() may be called with a NULL pointer.
        /// (default to posix malloc/free)
        /// </summary>
        public IntPtr MemAllocFn {
            get { return Native->MemAllocFn; }
            set { Native->MemAllocFn = value; }
        }
        public MemAllocFn GetMemAllocFn()
            => (MemAllocFn) Marshal.GetDelegateForFunctionPointer(MemAllocFn, typeof(MemAllocFn));
        public IntPtr SetMemAllocFn(MemAllocFn fn)
            => MemAllocFn = Marshal.GetFunctionPointerForDelegate(fn);

        /// <summary>
        /// Optional: override memory allocations. MemFreeFn() may be called with a NULL pointer.
        /// (default to posix malloc/free)
        /// </summary>
        public IntPtr MemFreeFn {
            get { return Native->MemFreeFn; }
            set { Native->MemFreeFn = value; }
        }
        public MemFreeFn GetMemFreeFn()
            => (MemFreeFn) Marshal.GetDelegateForFunctionPointer(MemFreeFn, typeof(MemFreeFn));
        public IntPtr SetMemFreeFn(MemFreeFn fn)
            => MemFreeFn = Marshal.GetFunctionPointerForDelegate(fn);

        private static IntPtr _MemAllocFnPtr;
        private static MemAllocFn _MemAllocFnDelegate;
        public IntPtr MemAlloc(void* sz) {
            if (_MemAllocFnPtr == MemAllocFn)
                goto Invoke;
            _MemAllocFnPtr = MemAllocFn;
            _MemAllocFnDelegate = GetMemAllocFn();
            Invoke:
            return (IntPtr) _MemAllocFnDelegate(sz);
        }
        public IntPtr MemAlloc(UIntPtr sz) => MemAlloc((void*) sz);
        public IntPtr MemAlloc(IntPtr sz) => MemAlloc((void*) sz);
        public IntPtr MemAlloc(int sz) => MemAlloc((void*) sz);

        private static IntPtr _MemFreeFnPtr;
        private static MemFreeFn _MemFreeFnDelegate;
        public void MemFree(void* ptr) {
            if (_MemFreeFnPtr == MemFreeFn)
                goto Invoke;
            _MemFreeFnPtr = MemFreeFn;
            _MemFreeFnDelegate = GetMemFreeFn();
            Invoke:
            _MemFreeFnDelegate(ptr);
        }
        public void MemFree(IntPtr ptr) => MemFree((void*) ptr);

    }

    public unsafe class KeyMap {
        private readonly NativeImGuiIO* _nativePtr;

        public KeyMap(NativeImGuiIO* nativePtr) {
            _nativePtr = nativePtr;
        }

        public int this[ImGuiKey key] {
            get {
                return _nativePtr->KeyMap[(int) key];
            }
            set {
                _nativePtr->KeyMap[(int) key] = value;
            }
        }
    }

    public unsafe class MouseDownStates {
        private readonly NativeImGuiIO* _nativePtr;

        public MouseDownStates(NativeImGuiIO* nativePtr) {
            _nativePtr = nativePtr;
        }

        public bool this[int button] {
            get {
                if (button < 0 || button > 5) {
                    throw new ArgumentOutOfRangeException(nameof(button));
                }

                return _nativePtr->MouseDown[button] == 1;
            }
            set {
                if (button < 0 || button > 5) {
                    throw new ArgumentOutOfRangeException(nameof(button));
                }

                byte pressed = value ? (byte) 1 : (byte) 0;
                _nativePtr->MouseDown[button] = pressed;
            }
        }
    }

    public unsafe class KeyDownStates {
        private readonly NativeImGuiIO* _nativePtr;

        public KeyDownStates(NativeImGuiIO* nativePtr) {
            _nativePtr = nativePtr;
        }

        public bool this[int key] {
            get {
                if (key < 0 || key > 512) {
                    throw new ArgumentOutOfRangeException(nameof(key));
                }

                return _nativePtr->KeysDown[key] == 1;
            }
            set {
                if (key < 0 || key > 512) {
                    throw new ArgumentOutOfRangeException(nameof(key));
                }

                byte pressed = value ? (byte) 1 : (byte) 0;
                _nativePtr->KeysDown[key] = pressed;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct NativeImGuiIO {
        //------------------------------------------------------------------
        // Settings (fill once)
        //------------------------------------------------------------------

        /// <summary>
        /// Display size, in pixels. For clamping windows positions.
        /// Default value: [unset]
        /// </summary>
        public ImVec2 DisplaySize;
        /// <summary>
        /// Time elapsed since last frame, in seconds.
        /// Default value: 1.0f / 10.0f.
        /// </summary>
        public float DeltaTime;
        /// <summary>
        /// Maximum time between saving positions/sizes to .ini file, in seconds.
        /// Default value: 5.0f.
        /// </summary>
        public float IniSavingRate;
        /// <summary>
        /// Path to .ini file. NULL to disable .ini saving.
        /// Default value: "imgui.ini"
        /// </summary>
        public IntPtr IniFilename;
        /// <summary>
        /// Path to .log file (default parameter to ImGui::LogToFile when no file is specified).
        /// Default value: "imgui_log.txt"
        /// </summary>
        public IntPtr LogFilename;
        /// <summary>
        /// Time for a double-click, in seconds.
        /// Default value: 0.30f.
        /// </summary>
        public float MouseDoubleClickTime;
        /// <summary>
        /// Distance threshold to stay in to validate a double-click, in pixels.
        /// Default Value: 6.0f.
        /// </summary>
        public float MouseDoubleClickMaxDist;
        /// <summary>
        /// Distance threshold before considering we are dragging.
        /// Default Value: 6.0f.
        /// </summary>
        public float MouseDragThreshold;

        /// <summary>
        /// Map of indices into the KeysDown[512] entries array.
        /// Default values: [unset]
        /// </summary>
        public fixed int KeyMap[(int) ImGuiKey.Count];
        /// <summary>
        /// When holding a key/button, time before it starts repeating, in seconds. (for actions where 'repeat' is active).
        /// Default value: 0.250f.
        /// </summary>
        public float KeyRepeatDelay;
        /// <summary>
        /// When holding a key/button, rate at which it repeats, in seconds.
        /// Default value: 0.020f.
        /// </summary>
        public float KeyRepeatRate;
        /// <summary>
        /// Store your own data for retrieval by callbacks.
        /// Default value: IntPtr.Zero.
        /// </summary>
        public IntPtr UserData;

        /// <summary>
        /// Load and assemble one or more fonts into a single tightly packed texture. Output to Fonts array.
        /// Default value: [auto]
        /// </summary>
        public NativeImFontAtlas* FontAtlas;
        /// <summary>
        /// Global scale all fonts.
        /// Default value: 1.0f.
        /// </summary>
        public float FontGlobalScale;
        /// <summary>
        /// Allow user scaling text of individual window with CTRL+Wheel.
        /// Default value: false.
        /// </summary>
        public byte FontAllowUserScaling;
        /// <summary>
        /// Font to use on NewFrame(). Use NULL to uses Fonts->Fonts[0].
        /// Default value: null.
        /// </summary>
        public NativeImFont* FontDefault;
        /// <summary>
        /// For retina display or other situations where window coordinates are different from framebuffer coordinates.
        /// User storage only, presently not used by ImGui.
        /// Default value: (1.0f, 1.0f).
        /// </summary>
        public ImVec2 DisplayFramebufferScale;
        /// <summary>
        /// If you use DisplaySize as a virtual space larger than your screen, set DisplayVisibleMin/Max to the visible area.
        /// Default value: (0.0f, 0.0f)
        /// </summary>
        public ImVec2 DisplayVisibleMin;
        /// <summary>
        /// If the values are the same, we defaults to Min=0.0f) and Max=DisplaySize.
        /// Default value: (0.0f, 0.0f).
        /// </summary>
        public ImVec2 DisplayVisibleMax;
        /// <summary>
        /// OS X style: Text editing cursor movement using Alt instead of Ctrl,
        /// Shortcuts using Cmd/Super instead of Ctrl,
        /// Line/Text Start and End using Cmd+Arrows instead of Home/End,
        /// Double click selects by word instead of selecting whole text,
        /// Multi-selection in lists uses Cmd/Super instead of Ctrl
        /// Default value: True on OSX; false otherwise.
        /// </summary>
        public byte OSXBehaviors;

        //------------------------------------------------------------------
        // User Functions
        //------------------------------------------------------------------

        /// <summary>
        /// Rendering function, will be called in Render(). 
        /// Alternatively you can keep this to NULL and call GetDrawData() after Render() to get the same pointer.
        /// </summary>
        public IntPtr RenderDrawListsFn;

        /// <summary>
        /// Optional: access OS clipboard
        /// (default to use native Win32 clipboard on Windows, otherwise uses a private clipboard. Override to access OS clipboard on other architectures)
        /// </summary>
        public IntPtr GetClipboardTextFn;
        /// <summary>
        /// Optional: access OS clipboard
        /// (default to use native Win32 clipboard on Windows, otherwise uses a private clipboard. Override to access OS clipboard on other architectures)
        /// </summary>
        public IntPtr SetClipboardTextFn;
        public IntPtr ClipboardUserData;

        /// <summary>
        /// Optional: override memory allocations. MemFreeFn() may be called with a NULL pointer.
        /// (default to posix malloc/free)
        /// </summary>
        public IntPtr MemAllocFn;
        /// <summary>
        /// Optional: override memory allocations. MemFreeFn() may be called with a NULL pointer.
        /// (default to posix malloc/free)
        /// </summary>
        public IntPtr MemFreeFn;

        /// <summary>
        /// Optional: notify OS Input Method Editor of the screen position of your cursor for text input position (e.g. when using Japanese/Chinese IME in Windows)
        /// (default to use native imm32 api on Windows)
        /// </summary>
        public IntPtr ImeSetInputScreenPosFn;
        /// <summary>
        /// (Windows) Set this to your HWND to get automatic IME cursor positioning.
        /// </summary>
        public IntPtr ImeWindowHandle;

        //------------------------------------------------------------------
        // Input - Fill before calling NewFrame()
        //------------------------------------------------------------------

        /// <summary>
        /// Mouse position, in pixels (set to -1,-1 if no mouse / on another screen, etc.).
        /// </summary>
        public ImVec2 MousePos;
        /// <summary>
        /// Mouse buttons: left, right, middle + extras.
        /// ImGui itself mostly only uses left button (BeginPopupContext** are using right button).
        /// Others buttons allows us to track if the mouse is being used by your application + available to user as a convenience via IsMouse** API.
        /// </summary>
        public fixed byte MouseDown[5];
        /// <summary>
        /// Mouse wheel: 1 unit scrolls about 5 lines text.
        /// </summary>
        public float MouseWheel;
        /// <summary>
        /// Request ImGui to draw a mouse cursor for you (if you are on a platform without a mouse cursor).
        /// </summary>
        public byte MouseDrawCursor;
        /// <summary>
        /// Keyboard modifier pressed: Control.
        /// </summary>
        public byte KeyCtrl;
        /// <summary>
        /// Keyboard modifier pressed: Shift
        /// </summary>
        public byte KeyShift;
        /// <summary>
        /// Keyboard modifier pressed: Alt
        /// </summary>
        public byte KeyAlt;
        public byte KeySuper;
        /// <summary>
        /// Keyboard keys that are pressed (in whatever storage order you naturally have access to keyboard data)
        /// </summary>
        public fixed byte KeysDown[512];
        /// <summary>
        /// List of characters input (translated by user from keypress+keyboard state).
        /// Fill using AddInputCharacter() helper.
        /// </summary>
        public fixed ushort InputCharacters[16 + 1];

        //------------------------------------------------------------------
        // Output - Retrieve after calling NewFrame(), you can use them to discard inputs or hide them from the rest of your application
        //------------------------------------------------------------------

        /// <summary>
        /// Mouse is hovering a window or widget is active (= ImGui will use your mouse input).
        /// </summary>
        public byte WantCaptureMouse;
        /// <summary>
        /// Widget is active (= ImGui will use your keyboard input).
        /// </summary>
        public byte WantCaptureKeyboard;
        /// <summary>
        /// Some text input widget is active, which will read input characters from the InputCharacters array.
        /// </summary>
        public byte WantTextInput;
        /// <summary>
        /// Framerate estimation, in frame per second. Rolling average estimation based on IO.DeltaTime over 120 frames.
        /// </summary>
        public float Framerate;
        /// <summary>
        /// Number of active memory allocations.
        /// </summary>
        public int MetricsAllocs;
        /// <summary>
        /// Vertices output during last call to Render().
        /// </summary>
        public int MetricsRenderVertices;
        /// <summary>
        /// Indices output during last call to Render() = number of triangles * 3
        /// </summary>
        public int MetricsRenderIndices;
        /// <summary>
        /// Number of visible windows (exclude child windows)
        /// </summary>
        public int MetricsActiveWindows;
        /// <summary>
        /// Mouse delta. Note that this is zero if either current or previous position are negative,
        /// so a disappearing/reappearing mouse won't have a huge delta for one frame.
        /// </summary>
        public ImVec2 MouseDelta;

        //------------------------------------------------------------------
        // [Internal] ImGui will maintain those fields for you
        //------------------------------------------------------------------

        /// <summary>
        /// Previous mouse position
        /// </summary>
        public ImVec2 MousePosPrev;
        /// <summary>
        /// Mouse button went from !Down to Down
        /// </summary>
        public fixed byte MouseClicked[5];
        /// <summary>
        /// Position at time of clicking
        /// </summary>
        public ImVec2 MouseClickedPos0;
        /// <summary>
        /// Position at time of clicking
        /// </summary>
        public ImVec2 MouseClickedPos1;
        /// <summary>
        /// Position at time of clicking
        /// </summary>
        public ImVec2 MouseClickedPos2;
        /// <summary>
        /// Position at time of clicking
        /// </summary>
        public ImVec2 MouseClickedPos3;
        /// <summary>
        /// Position at time of clicking
        /// </summary>
        public ImVec2 MouseClickedPos4;
        /// <summary>
        /// Time of last click (used to figure out double-click)
        /// </summary>
        public fixed float MouseClickedTime[5];
        /// <summary>
        /// Has mouse button been double-clicked?
        /// </summary>
        public fixed byte MouseDoubleClicked[5];
        /// <summary>
        /// Mouse button went from Down to !Down
        /// </summary>
        public fixed byte MouseReleased[5];
        /// <summary>
        /// Track if button was clicked inside a window.
        /// We don't request mouse capture from the application if click started outside ImGui bounds.
        /// </summary>
        public fixed byte MouseDownOwned[5];
        /// <summary>
        /// Duration the mouse button has been down (0.0f == just clicked).
        /// </summary>
        public fixed float MouseDownDuration[5];
        /// <summary>
        /// Previous time the mouse button has been down
        /// </summary>
        public fixed float MouseDownDurationPrev[5];
        /// <summary>
        /// Squared maximum distance of how much mouse has traveled from the click point
        /// </summary>
        public fixed float MouseDragMaxDistanceSqr[5];
        /// <summary>
        /// Duration the keyboard key has been down (0.0f == just pressed)
        /// </summary>
        public fixed float KeysDownDuration[512];
        /// <summary>
        /// Previous duration the key has been down
        /// </summary>
        public fixed float KeysDownDurationPrev[512];
    }

}
