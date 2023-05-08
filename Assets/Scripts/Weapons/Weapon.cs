using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour,IInteractable
{
    [SerializeField] protected Ammo _ammoType;
    [SerializeField] protected string _desc;
    [SerializeField] protected string _name;
    [SerializeField] protected int _degreesUnactivRotation;
    [SerializeField] protected Transform _unactiveHoldPoint;
    [SerializeField] protected Transform _holdPoint;
    [SerializeField] protected int _maxAmmo;
    [SerializeField] protected int _currentAmmo;
    [SerializeField] protected Projectile proj;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Cooldown _fireCooldown;
    [SerializeField] protected float _reloadTime;
    [SerializeField] protected int _attackLayer;
    public Ammo AmmoType => _ammoType;
    public int DegreesUnactivRotation => _degreesUnactivRotation;
    public Vector3 PivotLocalUnactivePosHold=>_unactiveHoldPoint.localPosition;
    public Vector3 PivotLocalPosHold => _holdPoint.localPosition;
    public SpriteRenderer SpriteRenderer=>_spriteRenderer;
    public float ReloadTime => _reloadTime;
    public string Name => _name;
    public bool IsEmpty => _currentAmmo <= 0;
    public bool IsFull => _currentAmmo >= _maxAmmo;

    public int MaxAmmo => _maxAmmo;
    public string Description 
    {
        get { return _desc; }
        set { _desc = value; }
    }

    public void Interact(GameObject obj)
    {
        var p=obj.GetComponent<Character>();
        p.TakeWeapon(this);
    }
    public virtual void Attack()
    {

    }
    public virtual void Reload(int count)
    {
        _currentAmmo = count;
    }
    public void SetAttackLayer()
    {
        Debug.Log(LayerMask.LayerToName(transform.parent.gameObject.layer));
        Debug.Log(LayerMask.LayerToName(8));
        var lay = transform.parent.gameObject.layer;
        if (lay == 3)
            _attackLayer =  8;
        else
            _attackLayer = 3;

    }
}
