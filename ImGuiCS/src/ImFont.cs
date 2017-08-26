using System;
using System.Runtime.InteropServices;

namespace ImGuiNET {
    public unsafe struct ImFont {

        static ImFont() {
            RegisterImVectorWrap();
        }

        public static void RegisterImVectorWrap() {
            ImWrappedPtrVector<ImFont>.Wrap = ptr => new ImFont((NativeImFont*) ptr);
            ImWrappedPtrVector<ImFont>.Unwrap = wrap => (IntPtr) wrap.Native;
        }

        public readonly NativeImFont* Native;

        public ImFont(NativeImFont* native) {
            Native = native;
        }

        /// <summary>
        /// Height of characters, set during loading (don't change after loading).
        /// Default value: [user-set]
        /// </summary>
        public float FontSize {
            get {
                return Native->FontSize;
            }
            set {
                Native->FontSize = value;
            }
        }

        /// <summary>
        /// Base font scale, multiplied by the per-window font scale which you can adjust with SetFontScale()
        /// Default value: 1.0f.
        /// </summary>
        public float Scale {
            get {
                return Native->Scale;
            }
            set {
                Native->Scale = value;
            }
        }

        /// <summary>
        /// Offset font rendering by xx pixels.
        /// Default value: (0.0f, 1.0f)
        /// </summary>
        public ImVec2 DisplayOffset {
            get {
                return Native->DisplayOffset;
            }
            set {
                Native->DisplayOffset = value;
            }
        }

        /// <summary>
        /// ImVector(Glyph)
        /// </summary>
        public ImVector<NativeImFont.Glyph> Glyphs {
            get {
                return &Native->Glyphs;
            }
            set {
                Native->Glyphs = value;
            }
        }

        /// <summary>
        /// Sparse. Glyphs->XAdvance directly indexable (more cache-friendly that reading from Glyphs,
        /// for CalcTextSize functions which are often bottleneck in large UI).
        /// </summary>
        public ImVector<float> IndexXAdvance {
            get {
                return &Native->IndexXAdvance;
            }
            set {
                Native->IndexXAdvance = value;
            }
        }

        /// <summary>
        /// Sparse. Index glyphs by Unicode code-point.
        /// </summary>
        public ImVector<ushort> IndexLookup {
            get {
                return &Native->IndexLookup;
            }
            set {
                Native->IndexLookup = value;
            }
        }

        /// <summary>
        /// Equivalent to FindGlyph(FontFallbackChar)
        /// </summary>
        public NativeImFont.Glyph* FallbackGlyph {
            get {
                return Native->FallbackGlyph;
            }
            set {
                Native->FallbackGlyph = value;
            }
        }

        public float FallbackXAdvance {
            get {
                return Native->FallbackXAdvance;
            }
            set {
                Native->FallbackXAdvance = value;
            }
        }

        /// <summary>
        /// Replacement glyph if one isn't found. Only set via SetFallbackChar()
        /// Default value: '?'
        /// </summary>
        public ushort FallbackChar {
            get {
                return Native->FallbackChar;
            }
            set {
                Native->FallbackChar = value;
            }
        }

        // Members: Cold ~18/26 bytes
        public int ConfigDataCount {
            get {
                return Native->ConfigDataCount;
            }
            set {
                Native->ConfigDataCount = value;
            }
        }

        /// <summary>
        /// ImFontConfig*. Pointer within ImFontAtlas->ConfigData
        /// </summary>
        public IntPtr ConfigData {
            get {
                return Native->ConfigData;
            }
            set {
                Native->ConfigData = value;
            }
        }

        /// <summary>
        /// ImFontAtlas*
        /// </summary>
        public IntPtr ContainerAtlas {
            get {
                return Native->ContainerAtlas;
            }
            set {
                Native->ContainerAtlas = value;
            }
        }

        /// <summary>
        /// Ascent: distance from top to bottom of e.g. 'A' [0..FontSize]
        /// </summary>
        public float Ascent {
            get {
                return Native->Ascent;
            }
            set {
                Native->Ascent = value;
            }
        }

        public float Descent {
            get {
                return Native->Descent;
            }
            set {
                Native->Descent = value;
            }
        }

    }

    /// <summary>
    /// Font runtime data and rendering.
    /// ImFontAtlas automatically loads a default embedded font for you when you call GetTexDataAsAlpha8() or GetTexDataAsRGBA32().
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct NativeImFont {
        static NativeImFont() {
            ImFont.RegisterImVectorWrap();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Glyph {
            public ushort Codepoint;
            public float XAdvance;
            public float X0, Y0, X1, Y1;
            public float U0, V0, U1, V1;     // Texture coordinates
        };

        // Members: Hot ~62/78 bytes
        /// <summary>
        /// Height of characters, set during loading (don't change after loading).
        /// Default value: [user-set]
        /// </summary>
        public float FontSize;
        /// <summary>
        /// Base font scale, multiplied by the per-window font scale which you can adjust with SetFontScale()
        /// Default value: 1.0f.
        /// </summary>
        public float Scale;
        /// <summary>
        /// Offset font rendering by xx pixels.
        /// Default value: (0.0f, 1.0f)
        /// </summary>
        public ImVec2 DisplayOffset;
        /// <summary>
        /// ImVector(Glyph)
        /// </summary>
        public ImVector Glyphs;

        /// <summary>
        /// Sparse. Glyphs->XAdvance directly indexable (more cache-friendly that reading from Glyphs,
        /// for CalcTextSize functions which are often bottleneck in large UI).
        /// </summary>
        public ImVector IndexXAdvance;

        /// <summary>
        /// Sparse. Index glyphs by Unicode code-point.
        /// </summary>
        public ImVector IndexLookup;

        /// <summary>
        /// Equivalent to FindGlyph(FontFallbackChar)
        /// </summary>
        public Glyph* FallbackGlyph;
        public float FallbackXAdvance;

        /// <summary>
        /// Replacement glyph if one isn't found. Only set via SetFallbackChar()
        /// Default value: '?'
        /// </summary>
        public ushort FallbackChar;

        // Members: Cold ~18/26 bytes
        public int ConfigDataCount;

        /// <summary>
        /// ImFontConfig*. Pointer within ImFontAtlas->ConfigData
        /// </summary>
        public IntPtr ConfigData;

        /// <summary>
        /// ImFontAtlas*
        /// </summary>
        public IntPtr ContainerAtlas;     // What we has been loaded into

        /// <summary>
        /// Ascent: distance from top to bottom of e.g. 'A' [0..FontSize]
        /// </summary>
        public float Ascent, Descent;
    };
}
