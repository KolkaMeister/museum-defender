using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ForegroundFade : MonoBehaviour
    {
        public float SceneLoadTime = 1f;
        [SerializeField] private Image _foreground;

        private void OnEnable()
        {
            SceneLoader.OnSceneStartLoad += Fade;
        }

        private void OnDisable()
        {
            SceneLoader.OnSceneStartLoad -= Fade;
        }

        private void Start()
        {
            StartCoroutine(LoadRoutine(true));
        }

        private void Fade()
        {
            StartCoroutine(LoadRoutine(false));
        }

        private IEnumerator LoadRoutine(bool forward)
        {
            float timer = SceneLoadTime;
            float progress = forward ? 1 : 0;
            while (timer > 0)
            {
                _foreground.color = new Color(0, 0, 0, progress);
                timer -= Time.deltaTime;
                progress = Mathf.Clamp(forward ? timer / SceneLoadTime : 1 - timer / SceneLoadTime, 0, 1);
                yield return null;
            }

            _foreground.color = new Color(0, 0, 0, progress);
            
            if(!forward)
                SceneLoader.ActivateScene();
        }
    }
}