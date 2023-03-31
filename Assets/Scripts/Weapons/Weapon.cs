using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] float _fireRate;

    public string Name => _name;
    public float FireRate => _fireRate;
}
