using System;

[Flags]
public enum DeformStyle
{
    None = 0,
    Right = 1 << 0,
    Left = 1 << 1,
    Up = 1 << 2,
    Down = 1 << 3,
    Forward = 1 << 4,
    Back = 1 << 5,
    XAxis = Right | Left,
    YAxis = Up | Down,
    ZAxis = Forward | Back,
    XYFace = XAxis | YAxis,
    XZFace = XAxis | ZAxis,
    YZFace = YAxis | ZAxis,
    All = XAxis | YAxis | ZAxis,
}
