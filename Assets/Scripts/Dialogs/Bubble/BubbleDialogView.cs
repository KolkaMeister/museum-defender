using UnityEngine;
using Zenject;

namespace Dialogs.Sideline
{
    public class BubbleDialogView : MonoBehaviour
    {
        public DialogTextView Text;
        [HideInInspector] public float TypeInterval;
        [HideInInspector] public float WaitTime;
        
        [Inject]
        private void Construct(DialogConfigSo config)
        {
            TypeInterval = config.BubbleTypeInterval;
            WaitTime = config.BubbleWaitTime;
            Text.Init(TypeInterval);
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        private void OnEnable()
        {
            Text.SetText("");
        }
    }
}