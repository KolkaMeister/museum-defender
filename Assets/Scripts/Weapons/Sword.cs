using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeWeapon
{
    [SerializeField] private Transform _sprite;
    [SerializeField] float[] holdRotations;
    protected override void AttackAnimation()
    {
    }
        protected override IEnumerator AnimationCoroutine()
        {
            float startTime = Time.time;
            float end = startTime + animationTime;
            while (Time.time < end)
            {
                
                yield return null;
            }
        }
}
