using System;
using System.Reflection;
using UI;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private NotesUI _notesUI;
    [SerializeField] private MiniMapController _miniMapController; 
    [SerializeField] private bool _isAttackPressed;
    private Camera _camera;
    private PlayMenuMediator _mediator;
    private PlayerInput _playerInput;
    private Vector2 _mousePosition;

    private void Awake()
    {
        _camera = Camera.main;
        _mediator = FindObjectOfType<PlayMenuMediator>();
    }
    private void Start()
    {
        
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
            _isAttackPressed = true;
        if(context.canceled)
            _isAttackPressed = false;
    }


    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed)
            _character.Dash();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
            _character.Interact();
    }

    private void MoveDirection( InputAction.CallbackContext context )
    {
        _character.MoveDirection = context.ReadValue<Vector2>();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(_notesUI.IsOpen())
            {
                _notesUI.Hide();
            }
            else
            {
                _mediator.SwitchMenu();
                PauseMenu.Toggle();
            }
            
        }
    }
    public void ToggleNotes(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _notesUI.Toggle();
            Debug.Log("_notesUI Toggled");
        }
    }
    public void ReloadWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
            _character.ReloadWeapon();
    }

    public void SetMousePosition(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }

    public void SwitchWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        _character.SetCurrentWeaponIndex_2();
    }
    public void SetWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        WeaponNumber index = context.control == context.action.controls[0]
            ? WeaponNumber.First : WeaponNumber.Second;
        _character.SetCurrentWeaponIndex((int)index);
    }
    public void ToggleMap(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            _miniMapController.Toggle();
            

        }
            return;
            
    }
    private void Update()
    {
        _character.AimPos = _camera.ScreenToWorldPoint(_mousePosition);
        if (_isAttackPressed)
            _character.Attack();
    }

    private enum WeaponNumber
    {
        First = 0,
        Second = 1
    }
}