using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private int _spread;
    [SerializeField] private Transform _projSpawnPos;
    [SerializeField] protected float _force;

    public override void Attack()
    {
        if (!_fireCooldown.IsReady || _currentAmmo < 1) return;

        float angle = GetSpreadAngle();
        Projectile obj = Instantiate(_proj, _projSpawnPos.transform.position,
            Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
        
        Vector3 dir = GetFinalDir(angle);
        obj.Rb.velocity = transform.right * _force;
        
        obj.Shot(dir, _force, _attackLayer);
        _currentAmmo--;
        _fireCooldown.Reset();
    }

    private Vector3 GetFinalDir(float angle) =>
        new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), _projSpawnPos.position.z);

    private float GetSpreadAngle()
    {
        Vector3 aimVector = (_projSpawnPos.position - _holdPoint.position).normalized;
        return Mathf.Atan2(aimVector.x, aimVector.y) + Random.Range(-_spread, _spread) * Mathf.Deg2Rad;
    }
}