using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class ClassPersistantProperty<TType> where TType : class
{
    public delegate void OnValueChanged(TType value);
    public OnValueChanged OnChanged;
    
    private TType _value;

    public TType Value
    {
        get => _value;
        set
        {
            if (_value.Equals(value))
                return;
            _value = value;
            OnChanged?.Invoke(_value);
        }
    }

    public ClassPersistantProperty(TType value)
    {
        _value = value;
    }
}