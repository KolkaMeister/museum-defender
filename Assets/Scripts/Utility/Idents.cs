using UnityEngine;

namespace Utility
{
    public static class Idents
    {
        public static class Layers
        {
            public static readonly int Allies = LayerMask.NameToLayer("Allies");
            public static readonly int Enemy = LayerMask.NameToLayer("Enemies");
        }
    }
}