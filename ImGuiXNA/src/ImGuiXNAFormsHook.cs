using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ImGuiXNA {
    internal sealed class ImGuiXNAFormsHook {
        public readonly IntPtr HandleForm;
        public IntPtr HandleHook { get; private set; }
        private Win32.WndProcDelegate _WndProcHook;

        public ImGuiXNAFormsHook(IntPtr handleForm, HookDelegate hook) {
            HandleForm = handleForm;
            Hook = hook; 
            _WndProcHook = WndProcHook;
            HandleHook = Win32.SetWindowsHookEx(
                4, _WndProcHook, IntPtr.Zero,
                Win32.GetWindowThreadProcessId(HandleForm, IntPtr.Zero)
            );
        }

        ~ImGuiXNAFormsHook() {
            Dispose(false);
        }

        private int WndProcHook(int nCode, IntPtr wParam, ref Win32.Message lParam) {
            if (nCode >= 0) {
                Win32.TranslateMessage(ref lParam);
                Hook?.Invoke(ref lParam);
            }

            return Win32.CallNextHookEx(HandleHook, nCode, wParam, ref lParam);
        }

        internal delegate void HookDelegate(ref Win32.Message msg);
        public HookDelegate Hook;

        public void Dispose() => Dispose(true);
        private void Dispose(bool disposing) {
            if (HandleHook == IntPtr.Zero)
                return;

            Win32.UnhookWindowsHookEx(HandleHook);
            HandleHook = IntPtr.Zero;
        }
    }

    internal static class Win32 {
        internal struct Message {
            public IntPtr LParam;
            public IntPtr WParam;
            public uint Msg;
            public IntPtr HWnd;
        }

        internal delegate int WndProcDelegate(int nCode, IntPtr wParam, ref Message m);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr SetWindowsHookEx(int hook, WndProcDelegate callback, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, ref Message m);

        [DllImport("user32.dll", EntryPoint = "TranslateMessage")]
        internal extern static bool TranslateMessage(ref Message m);

        [DllImport("user32.dll")]
        internal extern static uint GetWindowThreadProcessId(IntPtr window, IntPtr module);
    }
}
