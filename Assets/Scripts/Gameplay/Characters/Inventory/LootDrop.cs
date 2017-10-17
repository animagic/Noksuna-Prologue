using UnityEngine;

class LootDrop : ExtendedMonoBehaviour
{
    BaseItem Item;
    [SerializeField]
    BaseItem.ItemTypeEnum Type;
    [SerializeField]
    ItemQuality.QualityTypesEnum Quality;
    ItemQuality.QualityTypesEnum MaxQuality = ItemQuality.QualityTypesEnum.Legendary;
    ItemQuality.QualityTypesEnum LowestQuality = ItemQuality.QualityTypesEnum.Common;


    public void Init()
    {
        RollItemType();
        Quality = ItemQuality.RandomizeQuality(MaxQuality, LowestQuality);
    }

    public void RollItemType()
    {
        Type = (BaseItem.ItemTypeEnum)Random.Range(0, (int)BaseItem.ItemTypeEnum.zzzMaxTypes - 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.WithComponent<Player>(x => x.GetComponent<CharacterInventory>().AddToInventory(Item));
    }



}
