using System;

namespace Pools
{
    [Flags]
    public enum PoolType
    {
        Nothing = 1 << 0,
        Arrow = 1 << 1,
        Bullet = 1 << 2
    }
}