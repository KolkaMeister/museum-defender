using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Button _button;
    [SerializeField] private float _typeInterval = 0.3f;
    [SerializeField] private Animator _animator;
    private static readonly int _openKey = Animator.StringToHash("Open");
    private static readonly int _closeKey = Animator.StringToHash("Close");
    private DialogData _data;

    private int _itemIndex;
    private bool _typeText;
    private bool _typeName;

    public void StartDialog(DialogData data)
    {
        StopTypingRoutines();
        Clear();
        _data = data;
        _itemIndex = 0;
        _animator.SetTrigger(_openKey);
    }

    public void OnOpenAnimationEnd()
    {
        TypeFragment();
        _button.enabled = true;
    }

    public void Click()
    {
        Debug.Log("CLICK");
        if (_typeName || _typeText)
            SkipAnimation();
        else if (_itemIndex >= _data.Dialogs.Length - 1)
            Close();
        else
            NextItem();
    }

    private void Clear()
    {
        _text.text = null;
        _name.text = null;
    }

    private void TypeFragment()
    {
        StartCoroutine(TypeTextRoutine(_data.Dialogs[_itemIndex].Text));
        StartCoroutine(TypeNameRoutine(_data.Dialogs[_itemIndex].Name));
    }

    private void SkipAnimation()
    {
        StopTypingRoutines();
        _text.text = _data.Dialogs[_itemIndex].Text;
        _name.text = _data.Dialogs[_itemIndex].Name;
    }

    private void StopTypingRoutines()
    {
        StopAllCoroutines();
        _typeName = false;
        _typeText = false;
    }

    private void NextItem()
    {
        _itemIndex++;
        Clear();
        TypeFragment();
    }

    private void Close()
    {
        Clear();
        _button.enabled = false;
        _animator.SetTrigger(_closeKey);
        _data.OnDialogEnd?.Invoke();
    }

    private IEnumerator TypeTextRoutine(string text)
    {
        _typeText = true;
        foreach (char t in text)
        {
            _text.text += t;
            yield return new WaitForSeconds(_typeInterval);
        }

        _typeText = false;
    }

    private IEnumerator TypeNameRoutine(string text)
    {
        _typeName = true;
        foreach (char t in text)
        {
            _name.text += t;
            yield return new WaitForSeconds(_typeInterval);
        }

        _typeName = false;
    }
}