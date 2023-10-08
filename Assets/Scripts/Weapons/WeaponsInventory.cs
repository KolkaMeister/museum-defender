using System;
using UnityEngine;

public class WeaponsInventory
{
    public delegate void WeaponsListChanged(Weapon _DropWep, Weapon _TakeWep);
    public WeaponsListChanged OnListChanged;

    public delegate void WeaponsUsageChanged(Weapon _current, Weapon _last);
    public WeaponsUsageChanged OnUseChanged;

    [SerializeField] private Weapon[] weapons = new Weapon[2];
    public Weapon[] Weapons => weapons;

    private int _index;
    public Weapon CurrentWeapon => weapons[_index];

    public void TakeWeapon(Weapon w)
    {
        if (weapons[0] && weapons[1])
        {
            Weapon oldWep = CurrentWeapon;
            weapons[_index] = w;
            OnListChanged?.Invoke(oldWep, w);
        }
        else
        {
            int index = Array.FindIndex(weapons, x => !x);
            weapons[index] = w;
            ChangeIndex(index);
            OnListChanged(null, w);
        }
    }

    public void DropWeapon(int index)
    {
        OnListChanged?.Invoke(weapons[index], null);
        weapons[index] = null;
    }

    public void ChangeIndex(int _ind)
    {
        var lastInd = _index;
        _index = _ind;
        OnUseChanged?.Invoke(weapons[_index], weapons[lastInd]);
    }
}