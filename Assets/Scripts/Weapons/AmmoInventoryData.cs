using System;
//using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Common;

using UnityEngine;

[Serializable]
public class AmmoInventoryData
{
    [SerializeField] public List<AmmoItemData> AmmoBags = new List<AmmoItemData>();

    public int GetAmmo(Ammo ammo) =>
        AmmoBags
            .Where(x => x.Type == ammo)
            .Sum(x => x.Count);

    public void AddAmmo(Ammo ammo, int count)
    {
        AmmoItemData item = AmmoBags.SingleOrDefault(x => x.Type == ammo);
        if(item != null)
        {
            item.Count += count;
            return;
        }

        AmmoBags.Add(new AmmoItemData { Count = count, Type = ammo });
    }

    public void ReduceAmmo(Ammo ammo, int count)
    {
        AmmoItemData item = AmmoBags.SingleOrDefault(x => x.Type == ammo);
        if(item != null)
        {
            item.Count -= count;
            if (item.Count < 0)
                item.Count = 0;
        }
    }
}

[Serializable]
public class AmmoItemData
{
    public Ammo Type;
    public int Count;
}