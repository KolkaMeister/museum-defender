using UnityEngine;

namespace UI
{
    public class PlayMenu : MonoBehaviour
    {
        private static readonly int _show = Animator.StringToHash("Show");
        private static readonly int _hide = Animator.StringToHash("Hide");
        
        public bool IsActive;
        [SerializeField] private Animator _anim;

        public void SwitchMenu()
        {
            SwitchMenu(!IsActive);
        }

        public void SwitchMenu(bool active)
        {
            IsActive = active;
            _anim.SetBool(_show, IsActive);
            _anim.SetBool(_hide, !IsActive);
        }
    }
}