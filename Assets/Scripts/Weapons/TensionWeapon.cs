using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TensionWeapon : RangeWeapon
{
    [SerializeField] protected Transform _arrowHoldPoint;
    [SerializeField] protected Projectile _strungArrow;

    private void Start()
    {
        if (_currentAmmo > 0)
            SetArrow();
    }
    public override void Attack()
    {
        if (_strungArrow == null)
            return;
        _strungArrow.transform.parent = null;
        _strungArrow.transform.localScale = Vector2.one;
        _strungArrow.AddForce((_arrowHoldPoint.position - _holdPoint.position).normalized, _force);
        _strungArrow.GetComponent<Collider2D>().enabled = true;
        _strungArrow.damageLayer = _attackLayer;
        _strungArrow = null;
        _currentAmmo--;
    }
    public override void Reload(int count)
    {
        base.Reload(count);
        SetArrow();
    }
    private void SetArrow()
    {
        _strungArrow = Instantiate<Projectile>(proj, transform);
        _strungArrow.transform.localPosition = _arrowHoldPoint.localPosition;
        _strungArrow.GetComponent<Collider2D>().enabled = false;
    }
}
