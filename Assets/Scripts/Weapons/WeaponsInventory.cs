using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsInventory
{
    public delegate void WeaponsListChanged(Weapon _DropWep, Weapon _TakeWep);
    public WeaponsListChanged OnListChanged;
    public delegate void WeaponsUsageChanged(Weapon _current, Weapon _last);
    public WeaponsUsageChanged OnUseChanged;
    [SerializeField] private Weapon[] weapons = new Weapon[2];
    public Weapon[] Weapons => weapons;

    private int _index=0;
    public Weapon CurrentWeapon => weapons[_index];
    
    public void TakeWeapon( Weapon w)
    {
        if (weapons[0]!=null && weapons[1]!=null)
        {
            var oldWep = CurrentWeapon;
            var newWep = w;
            weapons[_index] = w;
            OnListChanged(oldWep,newWep);
        }else
        {
            int counter = 0;
            foreach (var item in weapons)
            {
                if (item==null)
                {
                    weapons[counter] = w;
                    ChangeIndex(counter);
                    OnListChanged(null, w);
                    return;
                }
                counter++;
            }
        }
    }
    public void DropWeapon(int index)
    {
        OnListChanged(weapons[index],null);
        weapons[index] = null;
    }
    public void ChangeIndex(int _ind)
    {
        var lastInd = _index;
        _index = _ind;
        OnUseChanged?.Invoke(weapons[_index], weapons[lastInd]);
    }
}
