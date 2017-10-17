using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : BaseItem
{
    public enum WeaponTypeEnum
    {
        ONEHANDED_SWORD,
        TWOHANDED_SWORD,
        ONEHANDED_AXE,
        TWOHANDED_AXE
    }

    public enum WeaponSlotEnum
    {
        MAIN_HAND,
        OFF_HAND,
        EITHER_HAND
    }

    public WeaponTypeEnum WeaponType;
    public WeaponSlotEnum WeaponSlot;
    public int AttackValue;

    public void AssignWeaponStatValues(WeaponItem item)
    {
        WeaponType = item.WeaponType;
        WeaponSlot = item.WeaponSlot;
        AttackValue = item.AttackValue;
    }
}
