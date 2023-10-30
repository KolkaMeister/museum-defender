using UnityEngine;

namespace Dialogs.Sideline
{
    [CreateAssetMenu(fileName = "DialogConfig", menuName = "History Trip/Dialog Config")]
    public class DialogConfigSo : ScriptableObject
    {
        public float StoryTypeInterval;
        public float BubbleTypeInterval;
        public float BubbleWaitTime;
    }
}