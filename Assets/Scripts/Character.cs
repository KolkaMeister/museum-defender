using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _VelMulti;
    [SerializeField] private GameObject _weaponsHolder;
    private WeaponsInventory _weaponInventory=new WeaponsInventory();
    private Vector2 _moveDirection = new Vector2(0,0);
    private Vector2 _aimPos = new Vector2(1,1);
    public Vector2 MoveDirection {
        get { return _moveDirection; }
        set 
        {
            _moveDirection = value;
        } 
    }
    public Vector2 AimPos {
        get { return _aimPos; }
        set 
        {
            _aimPos = value;
            CalculateScale(value);
            CalculateWeaponRotation(value);
        }
    }


    public void Update()
    {
        Velocty();
    }
    private void Velocty()
    {
        _rb.velocity = _moveDirection * _VelMulti;
    }
    private void CalculateScale(Vector2 val)
    {
        var dir = (val.x - transform.position.x);
        if ((val.x-transform.position.x) < 0) { transform.localScale = new Vector2(-1, 1); }
        if ((val.x - transform.position.x) > 0) { transform.localScale = new Vector2(1, 1); }
    }
    private void CalculateWeaponRotation(Vector2 val)
    {
        var direction = val - new Vector2(_weaponsHolder.transform.position.x, _weaponsHolder.transform.position.y);
        var rad = Mathf.Atan(direction.y/direction.x);
        // Debug.Log(rad);
        _weaponsHolder.transform.rotation= Quaternion.Euler(0,0,(180/Mathf.PI)*rad);
    }
}
