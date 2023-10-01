using UnityEngine;

namespace Utility
{
    public static class Idents
    {
        public static readonly int PlayerLayer = LayerMask.NameToLayer("Player");
        public static readonly int EnemyLayer = LayerMask.NameToLayer("Enemies");
    }
}