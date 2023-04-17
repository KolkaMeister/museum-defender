using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private Transform _projSpawnPos;
    [SerializeField] private float _force;

    public override void Attack()
    {
        var obj = Instantiate<Projectile>(proj, _projSpawnPos.transform.position, Quaternion.identity);
        obj.AddForce(transform.rotation.eulerAngles, _force);
    }
    public override void Reload()
    {
    }
}
