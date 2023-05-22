using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class StartDialog : MonoBehaviour, IInteractable
{
    [SerializeField] private string _desk;
    [SerializeField] private DialogData _dialogData;
    [SerializeField] public UnityEvent onDialogEnd;
    public string Description { get => _desk; set => _desk=value; }

    private DialogBox _dialogBox;
    public void Interact(GameObject obj)
    {
        if (_dialogBox == null)
            _dialogBox=FindObjectOfType<DialogBox>();
        _dialogData.onDialogEnd = onDialogEnd;
        _dialogBox.StartDialog(_dialogData);
    }
}
