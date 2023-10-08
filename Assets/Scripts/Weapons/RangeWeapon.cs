using Pools;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class RangeWeapon : Weapon
{
    [SerializeField] private int _spread;
    [FormerlySerializedAs("_projSpawnPos"),SerializeField] private Transform _projSpawn;
    [SerializeField] protected float _force;

    private IPool<Bullet> _bulletPool;

    [Inject]
    public virtual void Construct(PoolLocator locator)
    {
        _bulletPool = locator.Get<Bullet>();
    }
    
    public override void Attack()
    {
        if (!_fireCooldown.IsReady || _currentAmmo < 1) return;

        float angle = GetSpreadAngle();
        Projectile obj = _bulletPool.Pop(_projSpawn.position,
            Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
        
        Vector2 dir = GetFinalDir(angle);
        obj.Shot(dir, _force, _attackLayer);
        _currentAmmo--;
        _fireCooldown.Reset();
    }

    private Vector2 GetFinalDir(float angle) => new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

    private float GetSpreadAngle()
    {
        Vector3 aimVector = _projSpawn.position - _holdPoint.position;
        return Mathf.Atan2(aimVector.y, aimVector.x) + Random.Range(-_spread, _spread) * Mathf.Deg2Rad;
    }
}