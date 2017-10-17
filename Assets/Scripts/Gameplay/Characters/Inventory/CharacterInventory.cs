using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{

    [SerializeField]
    List<BaseItem> InventoryItems = new List<BaseItem>();
    [SerializeField]
    List<BaseRunestone> InventoryRunestones = new List<BaseRunestone>();
    [SerializeField]
    int MaxInventory;

    public GameObject MainHand;
    HandHoldPoint MainHandHoldPoint;
    public GameObject OffHand;
    HandHoldPoint OffHandHoldPoint;
    Dictionary<ArmorItem.ArmorSlotEnum, ArmorItem> ArmorSet = new Dictionary<ArmorItem.ArmorSlotEnum, ArmorItem>();


    // Use this for initialization
    void Start()
    {
        MainHandHoldPoint = GetComponentsInChildren<HandHoldPoint>().Where(x => x.HoldPointPosition == HandHoldPoint.HandHoldPositionEnum.MAIN_HAND).Single();
        OffHandHoldPoint = GetComponentsInChildren<HandHoldPoint>().Where(x => x.HoldPointPosition == HandHoldPoint.HandHoldPositionEnum.OFF_HAND).Single();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToInventory(BaseItem item)
    {
        InventoryItems.Add(item);
    }
    public void RemoveFromInventory(BaseItem item)
    {
        InventoryItems.Remove(item);
    }
    
    public List<BaseItem> GetItemInventory()
    {
        return InventoryItems;
    }

    public void AddRunestoneToInventory(BaseRunestone stone)
    {
        InventoryRunestones.Add(stone);
    }
    public void RemoveFromInventory(BaseRunestone stone)
    {
        InventoryRunestones.Remove(stone);
    }
    public List<BaseRunestone> GetRunestoneInventory()
    {
        return InventoryRunestones;
    }

    void SortItems()
    {
        InventoryItems = InventoryItems.OrderBy(x => x.ItemType).ToList();
    }

    public void EquipMainHand()
    {

        if (MainHand == null)
            MainHand = Instantiate(InventoryItems[0].GameModel, MainHandHoldPoint.transform.position, Quaternion.identity, MainHandHoldPoint.transform);
            
    }

    public void EquipOffHand()
    {
        if(OffHand == null)
            OffHand = Instantiate(InventoryItems[1].GameModel, OffHandHoldPoint.transform.position, Quaternion.identity, OffHandHoldPoint.transform);
    }

    public void SwapWeaponHands()
    {
        GameObject tempMain = MainHand;
        GameObject tempOff = OffHand;

        OffHand.transform.parent = MainHandHoldPoint.transform;
        OffHand.transform.position = MainHandHoldPoint.transform.position;
        OffHand = tempMain;

        MainHand.transform.parent = OffHandHoldPoint.transform;
        MainHand.transform.position = OffHandHoldPoint.transform.position;
        MainHand = tempOff;

    }
}
