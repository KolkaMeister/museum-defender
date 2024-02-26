using System.Collections;
using UnityEngine;

public class Sword : MeleeWeapon
{
    private static readonly int _attack = Animator.StringToHash("Attack");
    [SerializeField] private Animator _animator;
    
    public override void Attack()
    {
        if (!_fireCooldown.IsReady) return;

        AnimateAttack();
        DealDamage();
        _fireCooldown.Reset();
    }

    protected override void AnimateAttack()
    {
        _animator.SetTrigger(_attack);
    }
}