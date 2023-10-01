using Pools;
using UnityEngine;
using Zenject;

public class RangeWeapon : Weapon
{
    [SerializeField] private int _spread;
    [SerializeField] private Transform _projSpawnPos;
    [SerializeField] protected float _force;

    protected Pool<Bullet> _pool;

    [Inject]
    public void Construct(Pool<Bullet> pool)
    {
        _pool = pool;
    }
    
    public override void Attack()
    {
        if (!_fireCooldown.IsReady || _currentAmmo < 1) return;

        float angle = GetSpreadAngle();
        Projectile obj = _pool.Pop(_projSpawnPos.transform.position,
            Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
        
        Vector2 dir = GetFinalDir(angle);
        obj.Shot(dir, _force, _attackLayer);
        _currentAmmo--;
        _fireCooldown.Reset();
    }

    private Vector2 GetFinalDir(float angle) =>
        new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

    private float GetSpreadAngle()
    {
        Vector3 aimVector = (_projSpawnPos.position - _holdPoint.position).normalized;
        return Mathf.Atan2(aimVector.y, aimVector.x) + Random.Range(-_spread, _spread) * Mathf.Deg2Rad;
    }
}