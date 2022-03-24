using System;
using System.Runtime.InteropServices;

namespace F1T.Structs
{
    [ObsoleteAttribute()]
    [StructLayout(LayoutKind.Explicit, Pack = 0, Size = 4)]
    public struct UInt8Quad
    {
        [FieldOffset(0)]
        public byte A;
        [FieldOffset(1)]
        public byte B;
        [FieldOffset(2)]
        public byte C;
        [FieldOffset(3)]
        public byte D;
    }

    [ObsoleteAttribute()]
    [StructLayout(LayoutKind.Explicit, Pack = 0, Size = 8)]
    public struct UInt16Quad
    {
        [FieldOffset(0)]
        public ushort A;
        [FieldOffset(2)]
        public ushort B;
        [FieldOffset(6)]
        public ushort C;
        [FieldOffset(6)]
        public ushort D;
    }
    [ObsoleteAttribute()]
    [StructLayout(LayoutKind.Explicit, Pack = 0, Size = 16)]
    public struct FloatQuad
    {
        [FieldOffset(0)]
        public float A;
        [FieldOffset(4)]
        public float B;
        [FieldOffset(8)]
        public float C;
        [FieldOffset(12)]
        public float D;
    }
    [ObsoleteAttribute()]
    [StructLayout(LayoutKind.Explicit, Pack = 0, Size = 4)]
    public struct ByteQuad
    {
        [FieldOffset(0)]
        public byte A;
        [FieldOffset(1)]
        public byte B;
        [FieldOffset(2)]
        public byte C;
        [FieldOffset(3)]
        public byte D;
    }
}
