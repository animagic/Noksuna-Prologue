using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuronianStone : BaseRunestone
{
    public enum JuronianStoneTypeEnum
    {
        SCORCHED,
        GLACIAL,
        SHADOW,
        DIVINE
    }

    protected JuronianStoneTypeEnum StoneType;
    public JuronianStoneTypeEnum GetStoneType() { return StoneType; }
    [Tooltip("The stat to alter when not tethered to an Elysian Stone.")]
    public BaseCharacter.StatsForStatusEffects.BaseCharacterStatsEnum AlterableStat;
	// Use this for initialization
	void Start () {
        Init();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Init()
    {
        
    }

    public void RandomizeAttributes()
    {
        BaseType = RunestoneType.JURONIAN;
        RandomizeType();
        RollQuality();
        RandomizeAlterableStat();
        SetName();
    }

    void SetName()
    {
        RuneStoneName = string.Format("{0} Juronian Stone of {1}", Quality.ToString(), AlterableStat);
    }

    public string GetName()
    {
        return RuneStoneName;
    }

    void RandomizeType()
    {
        StoneType = (JuronianStoneTypeEnum)Random.Range(0, 3);
    }

    void RandomizeAlterableStat()
    {
        AlterableStat = (BaseCharacter.StatsForStatusEffects.BaseCharacterStatsEnum)Random.Range(0, 9);
    }

}
