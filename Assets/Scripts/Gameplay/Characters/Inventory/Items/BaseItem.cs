using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseItem : ScriptableObject {

    public string ItemName;

    public enum ItemTypeEnum
    {
        WEAPON,
        ARMOR,
        CONSUMABLE,
        KEY_ITEM,
        zzzMaxTypes
    };
    public ItemTypeEnum ItemType;
    public ItemQuality.QualityTypesEnum HighestQuality;
    public ItemQuality.QualityTypesEnum LowestQuality;

    public bool isSetStoneCount = false;
    public int StoneCount;

    [Header("Character Elements")]
    [SerializeField]
    public int RequiredLevel = 1;

    [Header("Visual Elements")]
    [SerializeField]
    public Sprite InventoryImage;
    [SerializeField]
    public GameObject GameModel;

    [Header("Inventory Elements")]
    [SerializeField]
    public bool isStackable;
    [SerializeField]
    public int maxStackNumber;
    [Tooltip("If an Item is Unique, the player can only hold one of them at a time in Inventory")]
    [SerializeField]
    public bool isUnique;

    public void AssignBaseItemValues(BaseItem item)
    {
        ItemName = item.ItemName;
        ItemType = item.ItemType;
        HighestQuality = item.HighestQuality;
        LowestQuality = item.LowestQuality;
        RequiredLevel = item.RequiredLevel;
        InventoryImage = item.InventoryImage;
        GameModel = item.GameModel;
        isStackable = item.isStackable;
        maxStackNumber = item.maxStackNumber;
        isUnique = item.isUnique;
    }

}
