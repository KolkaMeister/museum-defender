using Pools;
using UnityEngine;
using Zenject;

public class Bullet : Projectile
{
    private Pool<Bullet> _pool;

    [Inject]
    public void Construct(Pool<Bullet> pool)
    {
        _pool = pool;
    }
    
    private void OnEnable()
    {
        Invoke(nameof(BackToPool), 0.5f);
    }

    public override void Shot(Vector2 dir, float speed, int layer)
    {
        _rb.velocity = dir * speed;
        _damageLayer = layer;
    }

    protected override void BackToPool()
    {
        _pool.Push(this);
    }
}
