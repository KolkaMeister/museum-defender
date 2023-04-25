using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Common;
using UnityEngine;

[Serializable]
public class AmmoInventoryData
{
    [SerializeField] public List<AmmoItemData> AmmoBags= new List<AmmoItemData>();

    public int CheckAmmo(Ammo _a)
    {
        int totalAmmo=0;
        foreach (var item in AmmoBags) 
        {
            if (item.Type==_a)
            {
                totalAmmo += item.Count;
            }
        }
        return totalAmmo;
    }
    public void AddAmmo(Ammo _a, int count)
    {
        foreach (var item in AmmoBags)
        {
            if (item.Type==_a)
            {
                item.Count += count;
                return;
            }
        }
        AmmoBags.Add(new AmmoItemData() { Count = count, Type = _a });
    }
    public void GetAmmo(Ammo _a, int count)
    {
        foreach (var item in AmmoBags)
        {
            if (item.Type==_a)
            {
                item.Count -= count;
                if (item.Count < 0)
                    item.Count = 0;
            }
        }
    }
}
[Serializable]
public class AmmoItemData
{
    public Ammo Type;
    public int Count;
}

