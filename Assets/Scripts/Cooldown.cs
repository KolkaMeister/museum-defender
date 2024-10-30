using System;
using UnityEngine;

[Serializable]
public class Cooldown
{
    [SerializeField] private float _resetValue;
    private float _timesUp;
    
    public bool IsReady => _timesUp <= Time.time;
    public float RemainedTime => Mathf.Max(_timesUp - Time.time, 0);

    public Cooldown(float value)
    {
        _resetValue = value;
    }

    public void Reset()
    {
        _timesUp = Time.time + _resetValue;
    }

    public void SetResetTime(float value)
    {
        _resetValue = value;
    }
}