using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private bool _isAttackPressed;
    private Camera _camera;
    private PlayMenuMediator _mediator;

    private Vector2 _mousePosition;

    private void Awake()
    {
        _camera = Camera.main;
        _mediator = FindObjectOfType<PlayMenuMediator>();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
            _isAttackPressed = true;
        if(context.canceled)
            _isAttackPressed = false;
    }


     public void SetMousePosition(InputAction.CallbackContext context)
     {
         _mousePosition = context.ReadValue<Vector2>();
     }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
            _character.Interact();
    }

    public void MoveDirection( InputAction.CallbackContext context )
    {
        _character.MoveDirection = context.ReadValue<Vector2>();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if(context.started)
            _mediator.SwitchMenu();
    }

    public void ReloadWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
            _character.ReloadWeapon();
    }

    public void SwitchWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        WeaponNumber index = context.control == context.action.controls[0] 
            ? WeaponNumber.First : WeaponNumber.Second;
        _character.SetCurrentWeaponIndex((int)index);
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