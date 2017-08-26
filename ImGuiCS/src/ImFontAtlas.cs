using System.Runtime.InteropServices;
using System;

namespace ImGuiNET {
    public unsafe class ImFontAtlas {
        public readonly NativeImFontAtlas* Native;

        public ImFontAtlas(NativeImFontAtlas* native) {
            Native = native;
        }

        public ImFontTextureData GetTexDataAsAlpha8() {
            byte* pixels;
            int width, height;
            int bytesPerPixel;
            ImGuiNative.ImFontAtlas_GetTexDataAsAlpha8(Native, &pixels, &width, &height, &bytesPerPixel);

            return new ImFontTextureData(new IntPtr(pixels), width, height, bytesPerPixel);
        }

        public ImFontTextureData GetTexDataAsRGBA32() {
            byte* pixels;
            int width, height;
            int bytesPerPixel;
            ImGuiNative.ImFontAtlas_GetTexDataAsRGBA32(Native, &pixels, &width, &height, &bytesPerPixel);

            return new ImFontTextureData(new IntPtr(pixels), width, height, bytesPerPixel);
        }

        public void SetTexID(int textureID) {
            SetTexID(new IntPtr(textureID));
        }

        public void SetTexID(IntPtr textureID) {
            ImGuiNative.ImFontAtlas_SetTexID(Native, textureID.ToPointer());
        }

        public void Clear() {
            ImGuiNative.ImFontAtlas_Clear(Native);
        }

        public void ClearTexData() {
            ImGuiNative.ImFontAtlas_ClearTexData(Native);
        }

        public ImFont AddDefaultFont() {
            NativeImFont* nativeFontPtr = ImGuiNative.ImFontAtlas_AddFontDefault(Native);
            return new ImFont(nativeFontPtr);
        }

        public ImFont AddFontFromFileTTF(string fileName, float pixelSize) {
            NativeImFont* nativeFontPtr = ImGuiNative.ImFontAtlas_AddFontFromFileTTF(Native, fileName, pixelSize, IntPtr.Zero, null);
            return new ImFont(nativeFontPtr);
        }

        public ImFont AddFontFromMemoryTTF(IntPtr ttfData, int ttfDataSize, float pixelSize) {
            NativeImFont* nativeFontPtr = ImGuiNative.ImFontAtlas_AddFontFromMemoryTTF(Native, ttfData.ToPointer(), ttfDataSize, pixelSize, IntPtr.Zero, null);
            return new ImFont(nativeFontPtr);
        }

        public ImFont AddFontFromMemoryTTF(IntPtr ttfData, int ttfDataSize, float pixelSize, IntPtr fontConfig) {
            NativeImFont* nativeFontPtr = ImGuiNative.ImFontAtlas_AddFontFromMemoryTTF(Native, ttfData.ToPointer(), ttfDataSize, pixelSize, fontConfig, null);
            return new ImFont(nativeFontPtr);
        }

        /// <summary>
        /// User data to refer to the texture once it has been uploaded to user's graphic systems.
        /// It ia passed back to you during rendering.
        /// </summary>
        public IntPtr TexID {
            get {
                return Native->TexID;
            }
            set {
                Native->TexID = value;
            }
        }

        /// <summary>
        /// 1 component per pixel, each component is unsigned 8-bit. Total size = TexWidth * TexHeight
        /// </summary>
        public byte* TexPixelsAlpha8 {
            get {
                return Native->TexPixelsAlpha8;
            }
            set {
                Native->TexPixelsAlpha8 = value;
            }
        }

        /// <summary>
        /// 4 component per pixel, each component is unsigned 8-bit. Total size = TexWidth * TexHeight * 4
        /// </summary>
        public UIntPtr TexPixelsRGBA32 {
            get {
                return Native->TexPixelsRGBA32;
            }
            set {
                Native->TexPixelsRGBA32 = value;
            }
        }

        /// <summary>
        /// Texture width calculated during Build().
        /// </summary>
        public IntPtr TexWidth {
            get {
                return Native->TexWidth;
            }
            set {
                Native->TexWidth = value;
            }
        }

        /// <summary>
        /// Texture height calculated during Build().
        /// </summary>
        public IntPtr TexHeight {
            get {
                return Native->TexHeight;
            }
            set {
                Native->TexHeight = value;
            }
        }

        /// <summary>
        /// Texture width desired by user before Build(). Must be a power-of-two.
        /// If have many glyphs your graphics API have texture size restrictions you may want to increase texture width to decrease height.
        /// </summary>
        public IntPtr TexDesiredWidth {
            get {
                return Native->TexDesiredWidth;
            }
            set {
                Native->TexDesiredWidth = value;
            }
        }

        /// <summary>
        /// Texture coordinates to a white pixel (part of the TexExtraData block)
        /// </summary>
        public ImVec2 TexUvWhitePixel {
            get {
                return Native->TexUvWhitePixel;
            }
            set {
                Native->TexUvWhitePixel = value;
            }
        }

        /// <summary>
        /// (ImVector(ImFont*)
        /// </summary>
        public ImWrappedPtrVector<ImFont> Fonts {
            get {
                return &Native->Fonts;
            }
            set {
                Native->Fonts = value;
            }
        }

        // Private
        /// <summary>
        /// ImVector(ImFontConfig). Internal data
        /// </summary>
        public ImVector ConfigData {
            get {
                return Native->ConfigData;
            }
            set {
                Native->ConfigData = value;
            }
        }

    }

    public unsafe struct ImFontTextureData {
        public readonly IntPtr Pixels;
        public readonly int Width;
        public readonly int Height;
        public readonly int BytesPerPixel;

        public ImFontTextureData(IntPtr pixels, int width, int height, int bytesPerPixel) {
            Pixels = pixels;
            Width = width;
            Height = height;
            BytesPerPixel = bytesPerPixel;
        }
    }

    // Load and rasterize multiple TTF fonts into a same texture.
    // Sharing a texture for multiple fonts allows us to reduce the number of draw calls during rendering.
    // We also add custom graphic data into the texture that serves for ImGui.
    //  1. (Optional) Call AddFont*** functions. If you don't call any, the default font will be loaded for you.
    //  2. Call GetTexDataAsAlpha8() or GetTexDataAsRGBA32() to build and retrieve pixels data.
    //  3. Upload the pixels data into a texture within your graphics system.
    //  4. Call SetTexID(my_tex_id); and pass the pointer/identifier to your texture. This value will be passed back to you during rendering to identify the texture.
    //  5. Call ClearTexData() to free textures memory on the heap.
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct NativeImFontAtlas {
        // Members
        // (Access texture data via GetTexData*() calls which will setup a default font for you.)

        /// <summary>
        /// User data to refer to the texture once it has been uploaded to user's graphic systems.
        /// It ia passed back to you during rendering.
        /// </summary>
        public IntPtr TexID;
        /// <summary>
        /// 1 component per pixel, each component is unsigned 8-bit. Total size = TexWidth * TexHeight
        /// </summary>
        public byte* TexPixelsAlpha8;
        /// <summary>
        /// 4 component per pixel, each component is unsigned 8-bit. Total size = TexWidth * TexHeight * 4
        /// </summary>
        public UIntPtr TexPixelsRGBA32;
        /// <summary>
        /// Texture width calculated during Build().
        /// </summary>
        public IntPtr TexWidth;
        /// <summary>
        /// Texture height calculated during Build().
        /// </summary>
        public IntPtr TexHeight;
        /// <summary>
        /// Texture width desired by user before Build(). Must be a power-of-two.
        /// If have many glyphs your graphics API have texture size restrictions you may want to increase texture width to decrease height.
        /// </summary>
        public IntPtr TexDesiredWidth;
        /// <summary>
        /// Texture coordinates to a white pixel (part of the TexExtraData block)
        /// </summary>
        public ImVec2 TexUvWhitePixel;

        /// <summary>
        /// (ImVector(ImFont*)
        /// </summary>
        public ImVector Fonts;

        // Private
        /// <summary>
        /// ImVector(ImFontConfig). Internal data
        /// </summary>
        public ImVector ConfigData;
    }
}
