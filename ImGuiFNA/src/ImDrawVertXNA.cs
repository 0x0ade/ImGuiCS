using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImGuiXNA {
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImDrawVertXNA : IVertexType {
        public ImVec2 pos;
        public ImVec2 uv;
        public uint col;

        public const int PosOffset = 0;
        public const int UVOffset = 8;
        public const int ColOffset = 16;
        public readonly static int Size = sizeof(ImDrawVert);

        public readonly static VertexDeclaration _VertexDeclaration = new VertexDeclaration(
            Size,
            new VertexElement(PosOffset, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
            new VertexElement(UVOffset, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
            new VertexElement(ColOffset, VertexElementFormat.Color, VertexElementUsage.Color, 0)
        );
        public VertexDeclaration VertexDeclaration => _VertexDeclaration;
    }
}
