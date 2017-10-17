using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ItemCreator : EditorWindow
{
    Vector2 _scrollPos;
    BaseItem TempBaseItem;

    ArmorItem TempArmor;
    WeaponItem TempWeapon;
    ConsumableItem TempConsumable;
    KeyItemItem TempKeyItem;

    int fieldHeight = 15;
    int textFieldWidth = 300;

    int pickerHeight = 100;
    int pickerWidth = 300;

    const string armorPath = @"/Resources/Scriptable Objects/Items/Armors/";
    const string weaponPath = @"/Resources/Scriptable Objects/Items/Weapons/";
    const string consumablePath = @"/Resources/Scriptable Objects/Items/Consumables/";
    const string keyItemPath = @"/Resources/Scriptable Objects/Items/KeyItems/";

    [MenuItem("Noksuna Tools/Creators/Items")]
    public static void Init()
    {
        ItemCreator window = EditorWindow.GetWindow<ItemCreator>();
        window.minSize = new Vector2(700, 300);
        window.titleContent = new GUIContent("Item Creator","Item Creator");
        window.Show();
    }

    private void OnEnable()
    {
        RefreshNewObjects();
    }

    private void OnGUI()
    {
        DisplayEditor();
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }

    void RefreshNewObjects()
    {
        TempBaseItem = new BaseItem();
        TempArmor = new ArmorItem();
        TempWeapon = new WeaponItem();
        TempConsumable = new ConsumableItem();
        TempKeyItem = new KeyItemItem();
    }

    void AddItemToDatabaseGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        TempBaseItem.ItemName = EditorGUILayout.TextField("Item Name: ", TempBaseItem.ItemName, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        TempBaseItem.ItemType = (BaseItem.ItemTypeEnum)EditorGUILayout.EnumPopup("Item Type: ", TempBaseItem.ItemType, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        TempBaseItem.HighestQuality = (ItemQuality.QualityTypesEnum)EditorGUILayout.EnumPopup("Highest Quality: ", TempBaseItem.HighestQuality, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        TempBaseItem.LowestQuality = (ItemQuality.QualityTypesEnum)EditorGUILayout.EnumPopup("Lowest Quality: ", TempBaseItem.LowestQuality, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        TempBaseItem.RequiredLevel = EditorGUILayout.IntField("Required Level: ", TempBaseItem.RequiredLevel, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        TempBaseItem.InventoryImage = (Sprite)EditorGUILayout.ObjectField("Inventory Sprite: ", TempBaseItem.InventoryImage, typeof(Sprite), false, GUILayout.Width(pickerWidth));
        TempBaseItem.GameModel = (GameObject)EditorGUILayout.ObjectField("In-game Model: ", TempBaseItem.GameModel, typeof(GameObject), false, GUILayout.Width(pickerWidth));
        TempBaseItem.isStackable = EditorGUILayout.Toggle("Is Stackable?: ", TempBaseItem.isStackable);
        TempBaseItem.maxStackNumber = EditorGUILayout.IntField("Max Stack Count: ", TempBaseItem.maxStackNumber, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        TempBaseItem.isUnique = EditorGUILayout.Toggle("Is Unique Item?: ", TempBaseItem.isUnique);
        EditorGUILayout.Space();
        if(TempBaseItem.ItemType == BaseItem.ItemTypeEnum.ARMOR)
        {
            EditorGUILayout.LabelField("Armor Fields");
            TempArmor.ArmorSlot = (ArmorItem.ArmorSlotEnum)EditorGUILayout.EnumPopup("Armor Type: ", TempArmor.ArmorSlot, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
            TempArmor.ArmorValue = EditorGUILayout.IntField("Armor Value: ", TempArmor.ArmorValue, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        }
        if(TempBaseItem.ItemType == BaseItem.ItemTypeEnum.WEAPON)
        {
            EditorGUILayout.LabelField("Weapon Fields");
            TempWeapon.WeaponType = (WeaponItem.WeaponTypeEnum)EditorGUILayout.EnumPopup("Weapon Type: ", TempWeapon.WeaponType, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
            TempWeapon.WeaponSlot = (WeaponItem.WeaponSlotEnum)EditorGUILayout.EnumPopup("Weapon Slot: ", TempWeapon.WeaponSlot, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
            TempWeapon.AttackValue = EditorGUILayout.IntField("Attack Value: ", TempWeapon.AttackValue, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        }
        if(TempBaseItem.ItemType == BaseItem.ItemTypeEnum.CONSUMABLE)
        {
            EditorGUILayout.LabelField("Consumable Fields");
            TempConsumable.ConsumableType = (ConsumableItem.ConsumableTypeEnum)EditorGUILayout.EnumPopup("Consumable Type: ", TempConsumable.ConsumableType, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));

        }
        if(TempBaseItem.ItemType == BaseItem.ItemTypeEnum.KEY_ITEM)
        {
            EditorGUILayout.LabelField("Key Item Fields");
            TempKeyItem.SomeSpecialFancyNumber = EditorGUILayout.FloatField("Fancy Fake Number: ", TempKeyItem.SomeSpecialFancyNumber, GUILayout.Width(textFieldWidth), GUILayout.Height(fieldHeight));
        }
        
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("Create Item", GUILayout.Width(100), GUILayout.Height(25)))
        {
            if (String.IsNullOrEmpty(TempBaseItem.ItemName))
            {
                Debug.Log("The Item needs a name!");
                return;
            }

            List<string> allItems = new List<string>();
            List<string> strippedNames = new List<string>();
            switch (TempBaseItem.ItemType)
            {
                case BaseItem.ItemTypeEnum.ARMOR:
                    allItems = Directory.GetFiles(Application.dataPath + armorPath).ToList();
                    break;
                case BaseItem.ItemTypeEnum.WEAPON:
                    allItems = Directory.GetFiles(Application.dataPath + weaponPath).ToList();
                    break;
                case BaseItem.ItemTypeEnum.CONSUMABLE:
                    allItems = Directory.GetFiles(Application.dataPath + consumablePath).ToList();
                    break;
                case BaseItem.ItemTypeEnum.KEY_ITEM:
                    allItems = Directory.GetFiles(Application.dataPath + keyItemPath).ToList();
                    break;
            }
            foreach (string s in allItems)
            {
                string fileName = s.Remove(s.Length - 6);
                strippedNames.Add(fileName);
            }
            bool exists = strippedNames.Any(x => x == TempBaseItem.ItemName);
            if(exists)
            {
                Debug.Log("This name is already taken");
                return;
            }
            else
            {
                switch (TempBaseItem.ItemType)
                {
                    case BaseItem.ItemTypeEnum.ARMOR:
                        CreateArmor(TempBaseItem, TempArmor);
                        break;
                    case BaseItem.ItemTypeEnum.WEAPON:
                        CreateWeapon(TempBaseItem, TempWeapon);
                        break;
                    case BaseItem.ItemTypeEnum.CONSUMABLE:
                        CreateConsumable(TempBaseItem, TempConsumable);
                        break;
                    case BaseItem.ItemTypeEnum.KEY_ITEM:
                        CreateKeyItem(TempBaseItem, TempKeyItem);
                        break;
                }
            }
            RefreshNewObjects();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    void CreateArmor(BaseItem tempItem, ArmorItem newArmor)
    {
        ArmorItem armor = ScriptableObject.CreateInstance("ArmorItem") as ArmorItem;
        armor.AssignBaseItemValues(TempBaseItem);
        armor.AssignArmorStatValues(newArmor);
        AssetDatabase.CreateAsset(armor, @"Assets" + armorPath + tempItem.ItemName + ".asset");
        AssetDatabase.SaveAssets();
    }
    void CreateWeapon(BaseItem tempItem, WeaponItem newWeapon)
    {
        WeaponItem weapon = ScriptableObject.CreateInstance("WeaponItem") as WeaponItem;
        weapon.AssignBaseItemValues(tempItem);
        weapon.AssignWeaponStatValues(newWeapon);
        AssetDatabase.CreateAsset(weapon, @"Assets" + weaponPath + tempItem.ItemName + ".asset");
        AssetDatabase.SaveAssets();
    }

    void CreateConsumable(BaseItem tempItem, ConsumableItem newConsumable)
    {
        ConsumableItem cons = ScriptableObject.CreateInstance("ConsumableItem") as ConsumableItem;
        cons.AssignBaseItemValues(tempItem);
        cons.AssignConsumableStatValues(newConsumable);
        AssetDatabase.CreateAsset(cons, @"Assets" + consumablePath + tempItem.ItemName + ".asset");
        AssetDatabase.SaveAssets();
    }

    void CreateKeyItem(BaseItem tempItem, KeyItemItem newKeyItem)
    {
        KeyItemItem key = ScriptableObject.CreateInstance("KeyItemItem") as KeyItemItem;
        key.AssignBaseItemValues(tempItem);
        key.AssignKeyItemStatValues(newKeyItem);
        AssetDatabase.CreateAsset(key, @"Assets" + keyItemPath + tempItem.ItemName + ".asset");
        AssetDatabase.SaveAssets();
    }

    void DisplayEditor()
    {
        AddItemToDatabaseGUI();
    }
}
