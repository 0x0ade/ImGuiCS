using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ImGuiNET {
    /// <summary>
    /// ImGui.NET's TextInputBuffer, modified to use ImGui's MemAllocFn / MemFreeFn, avoiding Marshal.
    /// </summary>
    public class TextInputBuffer : IDisposable {

        public IntPtr Buffer { get; private set; }

        private uint _Length;
        public uint Length {
            get {
                return _Length;
            }
            set {
                if (value > int.MaxValue) {
                    throw new ArgumentOutOfRangeException("Length cannot be greater that Int32.MaxValue.");
                }

                Resize((int) value);
            }
        }

        public TextInputBuffer(int length = 1024) {
            if (length < 0) {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            CreateBuffer(length);
        }

        private unsafe void Resize(int newSize) {
            IntPtr newBuffer = ImGui.IO.MemAlloc(newSize);
            ImGuiNativeHelper.CopyData((void*) Buffer, (void*) newBuffer, Length);
            ImGui.IO.MemFree(Buffer);
            if (newSize > Length)
                ImGuiNativeHelper.ClearData((void*) ((byte*) Buffer + Length), (uint) newSize - Length);
            Buffer = newBuffer;
            _Length = (uint) newSize;
        }

        private unsafe void CreateBuffer(int size) {
            if (Buffer != IntPtr.Zero)
                ImGui.IO.MemFree(Buffer);
            Buffer = ImGui.IO.MemAlloc(size);
            _Length = (uint) size;
            ClearData();
        }

        public unsafe void ClearData() {
            ImGuiNativeHelper.ClearData((void*) Buffer, Length);
        }

        public void Dispose() {
            if (Buffer != IntPtr.Zero) {
                FreeNativeBuffer();
            }
        }

        private void FreeNativeBuffer() {
            ImGui.IO.MemFree(Buffer);
            Buffer = IntPtr.Zero;
            _Length = 0;
        }

        public string StringValue {
            get {
                return Marshal.PtrToStringAnsi(Buffer);
            }
            set {
                if (value.Length > Length)
                    Length = (uint) value.Length;

                IntPtr ptr = Marshal.StringToHGlobalAnsi(value);
                uint copyCount = (uint) Math.Min(Length, value.Length);
                unsafe
                {
                    ImGuiNativeHelper.CopyData((void*) ptr, (void*) Buffer, copyCount);
                    ImGuiNativeHelper.ClearData((byte*) Buffer + copyCount, _Length - copyCount);
                }
                Marshal.FreeHGlobal(ptr);
            }
        }

        public override string ToString() => StringValue;

        public static TextInputBuffer[] CreateBuffers(int size, int bufferLength = 1024) {
            TextInputBuffer[] buffers = new TextInputBuffer[size];
            for (int i = 0; i < size; i++)
                buffers[i] = new TextInputBuffer(bufferLength);
            return buffers;
        }

        public static void DisposeBuffers(TextInputBuffer[] buffers) {
            for (int i = 0; i < buffers.Length; i++) {
                buffers[i]?.Dispose();
                buffers[i] = null;
            }
        }

    }
}
