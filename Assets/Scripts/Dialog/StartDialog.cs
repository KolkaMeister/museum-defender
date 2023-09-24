using System;
using UnityEngine;
using UnityEngine.Events;

public class StartDialog : MonoBehaviour, IInteractable
{
    public UnityEvent OnDialogEnd;
    public string Description { get => _desk; set => _desk=value; }
    
    [SerializeField] private DialogData _dialogData;
    [SerializeField] private string _desk;
    private DialogBox _dialogBox;

    private void Start()
    {
        _dialogBox = FindObjectOfType<DialogBox>();
    }

    public void Interact(Character obj)
    {
        _dialogData.OnDialogEnd = OnDialogEnd;
        _dialogBox.StartDialog(_dialogData);
    }
}
