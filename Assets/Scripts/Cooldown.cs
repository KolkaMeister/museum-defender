using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cooldown
{
    [SerializeField] private float _resetValue;
    private float timesUp = 0;
    public bool IsReady => timesUp <= Time.time;

    public float RemainedTime => Mathf.Max(timesUp - Time.time,0);
    public Cooldown(float _value)
    {
        _resetValue = _value;
    }
    public void Reset()
    {
        timesUp = Time.time + _resetValue;
    }
    public void SetResetTime(float _value)
    {
        _resetValue = _value;
    }
}
