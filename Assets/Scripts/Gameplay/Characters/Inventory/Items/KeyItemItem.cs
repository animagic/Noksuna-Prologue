using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemItem : BaseItem
{
    public float SomeSpecialFancyNumber;

    public void AssignKeyItemStatValues(KeyItemItem item)
    {
        SomeSpecialFancyNumber = item.SomeSpecialFancyNumber;
    }
}
