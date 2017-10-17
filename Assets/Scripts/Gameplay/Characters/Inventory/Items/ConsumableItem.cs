using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : BaseItem
{
    public enum ConsumableTypeEnum
    {
        HEAL,
        BUFF,
        HEAL_BUFF
    }
    public ConsumableTypeEnum ConsumableType;

    public void AssignConsumableStatValues(ConsumableItem item)
    {
        ConsumableType = item.ConsumableType;
    }
}
