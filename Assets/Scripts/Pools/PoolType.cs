using System;

namespace Pools
{
    [Flags]
    public enum PoolType
    {
        Nothing = 1 << 0,
        Bullet = 1 << 2
    }
}