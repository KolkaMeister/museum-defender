using UnityEngine;

namespace Di
{
    public interface IDiInstaller
    {
        public DiContainer Container { get; }
        public void Install();
        public GameObject InstantiatePrefab(GameObject obj, Transform parent);
        public T InstantiatePrefab<T>(T comp, Transform parent) where T : Component;
    }
}