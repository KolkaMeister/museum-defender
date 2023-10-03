using UnityEngine;

namespace UI
{
    public class PlayMenuMediator : MonoBehaviour
    {
        [SerializeField] private PlayMenu _menu;

        public void SwitchMenu() => _menu.SwitchMenu();
        public void SwitchMenu(bool active) => _menu.SwitchMenu(active);
        public void ShowSettings() {}
        public void Exit() => SceneLoader.LoadScene(0, false);
    }
}