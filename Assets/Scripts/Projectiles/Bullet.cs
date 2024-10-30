using Infrastructure.Timers;
using Pools;
using UnityEngine;
using Zenject;

public class Bullet : Projectile
{
    private IPool<Bullet> _pool;
    [SerializeField] private Timer _lifetime;

    [Inject]
    public void Construct(PoolLocator locator)
    {
        _pool = locator.Get<Bullet>();
    }

    public override void Shot(Vector2 dir, float speed, int layer)
    {
        _rb.velocity = dir * speed;
        _damageLayer = layer;
        TimerManager.AddTimer(_lifetime = 0.5f);
    }

    private void Update()
    {
        if (_lifetime <= 0)
        {
            BackToPool();
        }
    }

    protected override void BackToPool()
    {
        _pool.Push(this);
    }
}