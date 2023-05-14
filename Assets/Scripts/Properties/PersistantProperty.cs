using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PersistantProperty<TType>
{
    public delegate void OnValueChanged(TType newValue, TType oldValue);

    public event OnValueChanged OnChanged;
    [SerializeField] private TType _value;
    public TType Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (_value.Equals(value)) return;
            var oldValue = _value;
            _value = value;
            OnChanged?.Invoke(_value, oldValue);
        }
    }
    public PersistantProperty(TType _default)
    {
        _value= _default;
    }
}
