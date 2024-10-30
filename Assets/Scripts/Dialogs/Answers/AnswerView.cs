using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs.Answers
{
    public class AnswerView : MonoBehaviour, IItemRenderer<string>
    {
        public Button Button;
        public TMP_Text Text; 
        
        public void SetData(string data, int index)
        {
            Text.text = data;
        }
    }
}