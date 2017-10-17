using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneStone : BaseRunestone
{
    public BaseProjectile.SpreadTypeEnum SpreadType;
    public enum ArcaneStoneType
    {
        PROJECTILE_MODIFIER,
        STATUS_EFFECT_ADDITION
    }
    public ArcaneStoneType ArcaneType;

    BaseStatusEffect StatusEffect;

	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Init()
    {
        
    }

    public void RandomizeAttributes()
    {
        BaseType = RunestoneType.ARCANE;
        RollQuality();
        RandomizeType();
        SetName();
    }

    public void SetName()
    {
        RuneStoneName = Quality + " Arcane Stone of Stuff";
    }

    void RandomizeType()
    {
        //ArcaneType = (ArcaneStoneType)Random.Range(0, 1);
        ArcaneType = ArcaneStoneType.STATUS_EFFECT_ADDITION;
    }

    void RandomizeSpread()
    {
        SpreadType = (BaseProjectile.SpreadTypeEnum)Random.Range((int)BaseProjectile.SpreadTypeEnum.SINGLE, (int)BaseProjectile.SpreadTypeEnum.__MAX_DONT_USE - 1);
    }

    public BaseStatusEffect GetStatusEffect()
    {
        return StatusEffect;
    }

    

}
