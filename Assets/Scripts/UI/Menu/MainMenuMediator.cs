using UnityEngine;

namespace UI
{
    public class MainMenuMediator : MonoBehaviour
    {
        public void Play() => SceneLoader.LoadScene(5, false);
        public void ShowSettings() {}
        public void ShowAuthors() {}
        public void Quit() => Application.Quit();
    }
}