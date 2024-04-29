using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Reload : MonoBehaviour
{
    [SerializeField] private Image _relImage;
    private Character _character;
    void Start()
    {
        _character = GameObject.Find("Player").GetComponent<Character>();
        _character.reloadInfo += ReloadFilled;
    }

    void Update()
    {
        
    }
    public void ReloadFilled(float value)
    {
        _relImage.fillAmount = value;
    }
}
