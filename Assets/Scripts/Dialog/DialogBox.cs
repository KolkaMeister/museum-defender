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
    [SerializeField] private float _typeInterval=0.3f;
    [SerializeField] private Animator _animator;
    static readonly int OpenKey = Animator.StringToHash("Open");
    static readonly int CloseKey = Animator.StringToHash("Close");
    private DialogData _data;

    private int itemIndex = 0;
    private Coroutine typeTextRoutine;
    private Coroutine typeNameRoutine;
    public void StartDialog(DialogData data)
    {
        StopTypingRoutines();
        Clear();
        _data = data;
        itemIndex= 0;
        _animator.SetTrigger(OpenKey);
    }
    private void Clear()
    {
        _text.text = null;
        _name.text = null;
    }
    public void OnOpenAnimationEnd()
    {
        TypeFragment();
        _button.enabled = true;
    }
    private void TypeFragment()
    {
       typeTextRoutine= StartCoroutine(TypeTextRoutine(_data.data[itemIndex].Text));
       typeNameRoutine= StartCoroutine(TypeNameRoutine(_data.data[itemIndex].Name));
    }
    public void Click()
    {
        Debug.Log("CLICL");
        if (typeNameRoutine != null || typeTextRoutine != null)
        {
            SkipAnimation();
        }
        else if(itemIndex >= _data.data.Length - 1)
        {
            Close();
        }else
        {
            NextItem();
        }
    }
    private void SkipAnimation()
    {
        StopTypingRoutines();
        _text.text = _data.data[itemIndex].Text;
        _name.text = _data.data[itemIndex].Name;
    }
    private void StopTypingRoutines()
    {
        StopTypeNameRoutine();
        StopTypeTextRoutine();
    }    


    private void NextItem()
    {
        itemIndex++;
        StopTypingRoutines();
        Clear();
        TypeFragment();
    }
    public void Close()
    {
        StopTypingRoutines();
        Clear();
        _button.enabled = false;
        _animator.SetTrigger(CloseKey);
        _data.onDialogEnd?.Invoke();
    }
    private void StopTypeTextRoutine()
    {
        if (typeTextRoutine != null)
            StopCoroutine(typeTextRoutine);
        typeTextRoutine = null;
    }
    private void StopTypeNameRoutine()
    {
        if (typeNameRoutine != null)
            StopCoroutine(typeNameRoutine);
        typeNameRoutine = null;
    }
    private IEnumerator TypeTextRoutine(string text)
    {

        for (int i = 0; i < text.Length; i++)
        {
            _text.text += text[i];
            yield return new WaitForSeconds(_typeInterval);
        }
        StopTypeTextRoutine();

    }
    private IEnumerator TypeNameRoutine(string text)
    {

        for (int i = 0; i < text.Length; i++)
        {
            _name.text += text[i];
            yield return new WaitForSeconds(_typeInterval);
        }
        StopTypeNameRoutine();
    }
}
