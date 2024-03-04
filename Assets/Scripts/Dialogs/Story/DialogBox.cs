using System;
using Dialogs.Answers;
using Dialogs.Sideline;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Dialogs
{
    public class DialogBox : MonoBehaviour
    {
        public DialogTextView Speech;
        public DialogTextView SpeakerName;
        public Button Button;
        [HideInInspector] public float TypeInterval = 0.3f;
        public float OpenTime;
        public float CloseTime;
        public Animator Animator;
        public AnswerGroup AnswerGroup;
        
        [SerializeField] private Transform _answerContainer;

        [Inject]
        private void Construct(DialogConfigSo config)
        {
            TypeInterval = config.StoryTypeInterval;
            Speech.Init(TypeInterval);
            SpeakerName.Init(TypeInterval);
            AnswerGroup = new AnswerGroup(_answerContainer);
        }

        private void Start()
        {
            Clear();
        }

        public void Clear()
        {
            Speech.SetText("");
            SpeakerName.SetText("");
            AnswerGroup.SetData(Array.Empty<string>());
        }
    }
}