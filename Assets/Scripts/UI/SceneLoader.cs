using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public static class SceneLoader
    {
        public static Action OnSceneStartLoad;

        private static AsyncOperation _loader;

        public static void LoadScene(int buildIndex, bool allowActivate)
        {
            if (_loader != null) 
                return;
            _loader = SceneManager.LoadSceneAsync(buildIndex);
            _loader.allowSceneActivation = allowActivate;
            OnSceneStartLoad?.Invoke();
        }
        
        public static float GetProgress() => _loader?.progress ?? -1;

        public static void ActivateScene()
        {
            if (_loader == null) 
                return;
            _loader.allowSceneActivation = true;
        }
    }
}