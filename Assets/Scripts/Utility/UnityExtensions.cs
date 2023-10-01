using System.Runtime.CompilerServices;

namespace Utility
{
    public static class UnityExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToLayerMask(this int obj) => 1 << obj;
    }
}