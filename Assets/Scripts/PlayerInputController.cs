using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] Character _character;
    public void MoveDirection( InputAction.CallbackContext context )
    {
        _character.MoveDirection = context.ReadValue<Vector2>();
    }
    public void SetAimDirection(InputAction.CallbackContext context)
    {
        if (Camera.main != null)
        _character.AimPos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _character.Interact();
        }
    }
    public void SwitchWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        int resss = 0;
        if (int.TryParse(context.control.name, out resss))
            _character.SetCurrentWeaponIndex(resss - 1);
            
    }
    public void ReloadWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        _character.ReloadWeapon();

    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        _character.Attack();

    }
}
