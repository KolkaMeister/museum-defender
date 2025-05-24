using System.Runtime.CompilerServices;

namespace Utility
{
    public static class UnityUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToLayerMask(int obj) => 1 << obj;
    }
}