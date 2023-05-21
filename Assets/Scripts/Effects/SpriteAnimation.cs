using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private int frameRate;
    [SerializeField] private AnimationClip[] clips;
    private bool isPlaying = true;
    private SpriteRenderer _renderer;
    private float SecondsPerFrame;
    private int currentFrame;
    private float spriteUpdateTime;
    private int currentClip;

    void Start()
    {
        SecondsPerFrame = 1f / frameRate;
        _renderer = GetComponent<SpriteRenderer>();
        StartAnimation();
    }
    void OnEnable()
    {
        spriteUpdateTime = Time.time + SecondsPerFrame;
    }
    private void OnBecameVisible()
    {
        enabled = isPlaying;
    }
    private void OnBecameInvisible()
    {
        enabled = false;
    }
    private void StartAnimation()
    {
        spriteUpdateTime = Time.time + SecondsPerFrame;
        enabled = isPlaying = true;
        currentFrame = 0;
    }
    public void SetClip(string name)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i].name == name)
            {
                currentClip = i;
                StartAnimation();
                return;
            }
        }
        enabled = isPlaying = false;
    }
    void Update()
    {
        if (spriteUpdateTime > Time.time) return;
        var clip = clips[currentClip];
        if (currentFrame >= clip.sprites.Length)
        {
            if (clip.loop)
            {
                currentFrame = 0;
            }
            else
            {
                enabled = isPlaying = clip.allowNextClip;
                clip.onComplete?.Invoke();
                if (clip.allowNextClip)
                {
                    currentFrame = 0;
                    currentClip = (int)Mathf.Repeat(currentClip + 1, clips.Length);
                }
            }
            return;
        }
        _renderer.sprite = clip.sprites[currentFrame];
        spriteUpdateTime += SecondsPerFrame;
        currentFrame++;
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
    [Serializable]
    public class AnimationClip
    {
        [SerializeField] public String name;
        [SerializeField] public Sprite[] sprites;
        [SerializeField] public bool loop;
        [SerializeField] public bool allowNextClip;
        [SerializeField] public UnityEvent onComplete;
    }
}