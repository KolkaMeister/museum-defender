using System;
using UnityEngine;

[Serializable]
public class PersistantProperty<TType>
{
    public delegate void OnValueChanged(TType newValue, TType oldValue);

    public event OnValueChanged OnChanged;
    [SerializeField] private TType _value;
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
    public PersistantProperty(TType _default)
    {
        _value= _default;
    }
}
