using System;

namespace Appalachia.KOC.Character.States
{
    [Flags]
    public enum HumanHands : byte
    {
        Neither = 0,
        Right = 1,
        Left = 2,
        Both = 3
    }
}
