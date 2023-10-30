using System;
using UnityEngine;

[Serializable]
public class PersistantProperty<TType>
{
    [SerializeField] private TType _value;

    public delegate void OnValueChanged(TType newValue, TType oldValue);
    public event OnValueChanged OnChanged;

    public TType Value
    {
        get => _value;
        set
        {
            if (_value.Equals(value)) return;
            TType oldValue = _value;
            _value = value;
            OnChanged?.Invoke(_value, oldValue);
        }
    }

    public PersistantProperty(TType @default)
    {
        _value = @default;
    }
}