using Pools;
using UnityEngine;
using Zenject;

public class Bullet : Projectile
{
    private IPool<Bullet> _pool;

    [Inject]
    public void Construct(PoolLocator locator)
    {
        _pool = locator.Get<Bullet>();
    }
    
    public override void Shot(Vector2 dir, float speed, int layer)
    {
        _rb.velocity = dir * speed;
        _damageLayer = layer;
        Invoke(nameof(BackToPool), 0.5f);
    }

    protected override void BackToPool()
    {
        _pool.Push(this);
    }
}
