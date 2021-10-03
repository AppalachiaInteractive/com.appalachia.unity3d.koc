using System;

namespace Appalachia.Core.Character.States
{
    [Flags]
    public enum HumanFeet : byte
    {
        Neither = 0,
        Right = 1,
        Left = 2,
        Both = 3
    }
}