using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private int _spread;
    [SerializeField] private Transform _projSpawnPos;
    [SerializeField] protected float _force;

    public override void Attack()
    {
        if (!_fireCooldown.IsReady || _currentAmmo < 1) return;

        Projectile obj = Instantiate(_proj, _projSpawnPos.transform.position, Quaternion.identity);
        Vector3 dir = GetFinalDir();
        obj.AddForce(dir, _force);
        obj.Shot(dir, _force, _attackLayer);
        _currentAmmo--;
        _fireCooldown.Reset();
    }

    private Vector3 GetFinalDir()
    {
        // TODO: Correct!!!
        var aimVector = (_projSpawnPos.position - _holdPoint.position).normalized;
        var finalRad = Mathf.Atan2(aimVector.x, aimVector.y) + Random.Range(-_spread, _spread) * Mathf.Deg2Rad;
        var finalForceVec = new Vector3(Mathf.Sin(finalRad), Mathf.Cos(finalRad), _projSpawnPos.position.z);
        return finalForceVec;
    }
}