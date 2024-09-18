using System;

namespace MegaChaseGame.Behaviours
{
    [Flags]
    public enum MovementDirection
    {
        None = 0,
        Forward = 1 << 0,
        Backward = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3
    }
}
