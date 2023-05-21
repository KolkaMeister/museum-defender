using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeleeWeapon : Weapon
{
   [SerializeField] protected float animationTime;
   [SerializeField] protected Transform _damagePoint;
   [SerializeField] protected float _damageRadius;
   [SerializeField] protected float _damage;
    protected Coroutine _animRoutine;
    protected virtual void AttackAnimation()
    {
        if (_animRoutine != null)
        {
            StopCoroutine(_animRoutine);
            _animRoutine = null;
        }
        _animRoutine = StartCoroutine(AnimationCoroutine());
    }
    protected virtual IEnumerator AnimationCoroutine()
    { yield return null; }    
    protected virtual void DealDamage()
    {
        var lay = _attackLayer == 8 ? 256 : 8;
        var arr = Physics2D.OverlapCircleAll(_damagePoint.position, _damageRadius,lay);
        Debug.Log(lay);
        foreach (var item in arr) 
        {
            Debug.Log(item);
            var health= item.GetComponent<ITakeDamage>();
            if (health != null)
                health.TakeDamage(_damage);
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
