using System.Collections;
using UnityEditor;
using UnityEngine;
using Utility;

public class MeleeWeapon : Weapon
{
    [SerializeField] protected float animationTime;
    [SerializeField] protected Transform _damagePoint;
    [SerializeField] protected float _damageRadius;
    [SerializeField] protected float _damage;
    protected bool _isAnimRoutine;

    protected virtual void AttackAnimation()
    {
        if (_isAnimRoutine)
        {
            StopAllCoroutines();
            _isAnimRoutine = false;
        }

        StartCoroutine(AnimationCoroutine());
    }

    protected virtual IEnumerator AnimationCoroutine()
    {
        yield return null;
    }

    protected virtual void DealDamage()
    {
        int lay = _attackLayer == Idents.EnemyLayer
            ? Idents.EnemyLayer.ToLayerMask()
            : Idents.PlayerLayer.ToLayerMask();
        var colliders = Physics2D.OverlapCircleAll(_damagePoint.position, _damageRadius, lay);
        foreach (Collider2D item in colliders)
        {
            if(item.TryGetComponent(out ITakeDamage health))
                health.ChangeHealth(_damage);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = new Color(1, 1, 0, 0.1f);
        Handles.DrawSolidDisc(_damagePoint.transform.position, Vector3.forward, _damageRadius);
    }
#endif
}