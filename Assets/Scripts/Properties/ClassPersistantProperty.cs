using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class ClassPersistantProperty<TType> where TType : class
{
    private TType _value;

    public delegate void OnValueChanged(TType _value);

    public OnValueChanged OnChanged;

    public TType Value
    {
        get => _value;
        set
        {
            if (_value == value)
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