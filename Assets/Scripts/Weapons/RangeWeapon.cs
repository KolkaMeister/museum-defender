using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private Transform _projSpawnPos;
    [SerializeField] protected float _force;
    public override void Attack()
    {
        if (!_fireCooldown.IsReady)
        {
            return;
        }
        if (_currentAmmo < 1)
            return;
        var obj = Instantiate<Projectile>(proj, _projSpawnPos.transform.position, Quaternion.identity);
        obj.damageLayer = _attackLayer;
        obj.AddForce((_projSpawnPos.position - _holdPoint.position).normalized, _force);
        _currentAmmo--;
        _fireCooldown.Reset();
    }
    public override void Reload(int count)
    {
        base.Reload(count);
    }
}
