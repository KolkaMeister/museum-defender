using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private int _spread;
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
        var aimVector = (_projSpawnPos.position - _holdPoint.position).normalized;
        var finalRad = Mathf.Atan2(aimVector.x, aimVector.y) + Random.Range(-_spread,_spread)*Mathf.Deg2Rad;
        var finalForceVec = new Vector3(Mathf.Sin(finalRad),Mathf.Cos(finalRad), _projSpawnPos.position.z);
        obj.AddForce(finalForceVec, _force);
        _currentAmmo--;
        _fireCooldown.Reset();
    }
    public override void Reload(int count)
    {
        base.Reload(count);
    }
}
