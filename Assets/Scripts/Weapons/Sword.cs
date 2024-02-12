using System.Collections;
using UnityEngine;
using Zenject;

public class Sword : MeleeWeapon
{
    private static readonly int _attack = Animator.StringToHash("Attack");
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _attackCollider;
    
    private Player _player;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }
    
    public override void Attack()
    {
        if (!_fireCooldown.IsReady) return;

        StartCoroutine(AnimationCoroutine());
        AnimateAttack();
        DealDamage();
        _fireCooldown.Reset();
    }

    protected override void AnimateAttack()
    {
        _animator.SetTrigger(_attack);
    }

    protected override IEnumerator AnimationCoroutine()
    {
        SetActiveAttack(true);
        yield return new WaitForSeconds(animationTime);
        SetActiveAttack(false);
    }

    private void SetActiveAttack(bool value)
    {
        _collider.enabled = !value;
        _attackCollider.enabled = value;
    }
}