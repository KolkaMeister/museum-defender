using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnBar : MonoBehaviour
{
    [SerializeField] private Image _hpImage;
    private PersistantProperty<float> _hp;
    private float _maxHp;
    void Start()
    {
        _hp = GetComponent<Character>().Health;
        _maxHp = _hp.Value;
        _hp.OnChanged += _barChange;

    }

    private void _barChange(float newValue, float oldValue)
    {
        _hpImage.fillAmount = newValue / _maxHp;
    }
}
