using UnityEngine;

namespace Utility
{
    public static class Idents
    {
        public static class Layers
        {
            public static readonly int Player = LayerMask.NameToLayer("Player");
            public static readonly int Enemy = LayerMask.NameToLayer("Enemies");
        }
    }
}