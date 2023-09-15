using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private bool _isAttackPressed;
    private Camera _camera;
    private PlayMenuMediator _mediator;

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


    // public void SetAimDirection(InputAction.CallbackContext context)
    // {
    //  if (Camera.main != null)
    //  _character.AimPos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    //}

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _character.Interact();
        }
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
        if (!context.performed)
            return;
        _character.ReloadWeapon();

    }

    public void SwitchWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        int resss = 0;
        if (int.TryParse(context.control.name, out resss))
            _character.SetCurrentWeaponIndex(resss - 1);
            
    }

    private void Update()
    {
        _character.AimPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (_isAttackPressed)
            _character.Attack();

    }
}