using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Di
{
    public abstract class DiMonoInstaller : MonoBehaviour, IDiInstaller
    {
        public DiContainer Container { get; private set; }

        public abstract void Install();

        public DiState DiState;

        public T InstantiatePrefab<T>(T comp, Transform parent)
            where T : Component
        {
            if (!IsState(DiState.Initialize)) return null;
            T instance = Instantiate(comp, parent);
            InjectToGameObject(instance.gameObject);
            return instance;
        }

        public GameObject InstantiatePrefab(GameObject obj, Transform parent)
        {
            if (!IsState(DiState.Initialize)) return null;
            GameObject instance = Instantiate(obj, parent);
            InjectToGameObject(instance);
            return instance;
        }

        private bool IsState(DiState state)
        {
            if (DiState == state) return true;
            Debug.LogWarning($"Installer is not in {state} state");
            return false;

        }

        private void Awake()
        {
            Container = new DiContainer();
            Install();
            DiState = DiState.Initialize;
            Initialize();
            InitializeOnScene();
            DiState = DiState.Update;
        }

        public virtual void Initialize()
        {
        }

        private void InitializeOnScene()
        {
            foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
                InjectToGameObject(obj);
        }

        private void InjectToGameObject(GameObject instance)
        {
            if (!IsState(DiState.Initialize)) return;
            foreach (MonoBehaviour mono in instance.GetComponentsInChildren<MonoBehaviour>())
                Container.InjectTo(mono);
        }
    }

    public enum DiState
    {
        Install = 0,
        Initialize = 1,
        Update = 2
    }
}