using System.Collections;
using UnityEngine;

public class Spear : MeleeWeapon
{
    [SerializeField] private GameObject _splash;
    [SerializeField] private Vector3 _spriteBasePos;
    [SerializeField] private Vector3 _spriteAttackPos;
    [SerializeField] private Transform _sprite;
    [SerializeField] private Transform _splashSpawnPos;

    public override void Attack()
    {
        if (!_fireCooldown.IsReady) return;
        
        AnimateAttack();
        DealDamage();
        _fireCooldown.Reset();
        SpawnSplash();
        _audioSource.Play();
    }

    protected override IEnumerator AnimationCoroutine()
    {
        _isAnimRoutine = true;
        float startTime = Time.time;
        float secondStageStartTime = startTime + animationTime * 2 / 5;
        float end = startTime + animationTime;
        while (Time.time < end)
        {
            if (Time.time < secondStageStartTime)
                _sprite.localPosition = Vector3.Lerp(_spriteAttackPos, _spriteBasePos,
                    (Time.time - startTime) / (animationTime * 2 / 5));
            else
                _sprite.localPosition = Vector3.Lerp(_spriteAttackPos, _spriteBasePos,
                    (Time.time - secondStageStartTime) / (animationTime * 3 / 5));
            yield return null;
        }

        _isAnimRoutine = false;
    }

    private void SpawnSplash()
    {
        GameObject obj = Instantiate(_splash, _splashSpawnPos.position, transform.rotation);
        obj.transform.localScale = transform.lossyScale;
    }
}