using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRunestone {

    public enum RunestoneAugmentType
    {
        JURONIAN,
        ARCANE
    }
    public enum RunestoneType
    {
        ELYSIAN,
        JURONIAN,
        ARCANE
    }
    [Header("Base Values")]
    [SerializeField]
    protected string RuneStoneName;
    public RunestoneType BaseType;
    [SerializeField]
    protected ItemQuality.QualityTypesEnum Quality;

    public Sprite UISprite;

    protected void RollQuality()
    {
        Quality = ItemQuality.RandomizeQuality(ItemQuality.QualityTypesEnum.Legendary, ItemQuality.QualityTypesEnum.Common);
    }

    public string GetName()
    {
        return RuneStoneName;
    }
}
