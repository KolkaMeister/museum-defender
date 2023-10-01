using System;
using Di;
using Pools;
using UnityEngine;

public class Bullet : Projectile
{
    private Pool<Bullet> _pool;

    [Inject]
    public void Construct(PoolLocator locator)
    {
        _pool = locator.GetPool<Bullet>();
    }
    
    private void OnEnable()
    {
        Invoke(nameof(DestroyOnHit), 0.5f);
    }

    public override void Shot(Vector2 dir, float speed, int layer)
    {
        _rb.velocity = dir * speed;
        _damageLayer = layer;
    }

    private void OnDisable()
    {
        _rb.velocity = Vector2.zero;
    }

    protected override void DestroyOnHit()
    {
        _pool.Push(this);
    }
}
