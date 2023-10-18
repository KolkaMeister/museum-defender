using System.Collections;
using TMPro;
using UnityEngine;

namespace Dialogs
{
    public class DialogTextView : MonoBehaviour
    {
        private TMP_Text _text;
        private float _typeInterval;
        private bool _isTyping;

        public TMP_Text Text
        {
            get => _text;
        }

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void Init(float typeInterval)
        {
            _typeInterval = typeInterval;
        }

        public bool IsTyping()
        {
            return _isTyping;
        }

        public void SetText(string text, bool withAnimation = false)
        {
            StopAllCoroutines();
            if (withAnimation)
                StartCoroutine(TypeText(text));
            else
                _text.text = text;
        }

        private IEnumerator TypeText(string text)
        {
            _isTyping = true;
            foreach (char t in text)
            {
                _text.text += t;
                yield return new WaitForSeconds(_typeInterval);
            }

            _isTyping = false;
        }
    }
}