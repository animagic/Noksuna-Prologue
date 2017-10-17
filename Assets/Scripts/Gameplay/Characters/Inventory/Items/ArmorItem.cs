using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorItem : BaseItem {

    public enum ArmorSlotEnum
    {
        HEAD,
        BODY,
        HANDS,
        LEGS,
        FEET,
        EARRING,
        NECK,
        RING,
        SHIELD
    }
    public ArmorSlotEnum ArmorSlot;

    public int ArmorValue;

    public ArmorItem()
    {
        ItemType = ItemTypeEnum.ARMOR;
        isStackable = false;
    }

    public void AssignArmorStatValues(ArmorItem item)
    {
        ArmorSlot = item.ArmorSlot;
        ArmorValue = item.ArmorValue;
    }


}
