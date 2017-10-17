using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Runestones/Elysian Stone")]
public class ElysianStone : BaseRunestone
{
    [SerializeField]
    protected List<BaseAbility> AllAbilities = new List<BaseAbility>();
    [SerializeField]
    protected BaseAbility AbilityAffected;
    public BaseAbility GetAbilityAffected() { return AbilityAffected; }
    [SerializeField]
    protected BaseAbility.AttackTypeEnum StoneType;
    [SerializeField]
    bool isComplete = false;

    [Header("ElysianStone Slots")]
    [SerializeField]
    ElysianChildSlots[] RunestoneSlots;
    int SlotsCounts = 1;

    GameObject ElysianBuffDebuffModel;
    GameObject ElysianMainModel;

    void Start()
    {
        
        Init();
    }

    public void Init()
    {
        
        RandomizeAttributes();
        
    }

    public void RandomizeAttributes()
    {
        BaseType = RunestoneType.ELYSIAN;
        RollStoneType();
        RollQuality();
        //RollAffectedAbility();
        RollRuneSlots();
        SetName();
    }
    #region RANDOMIZERS
    void SetName()
    {
        if (AbilityAffected != null)
        {
            RuneStoneName = string.Format("{0} Elysian Stone of {1}", Quality.ToString(), AbilityAffected.AbilityName); 
        }
        else
        {
            RuneStoneName = string.Format("{0} Elysian Stone", Quality.ToString());
        }
    }

    void RollAffectedAbility()
    {
        AllAbilities = Resources.LoadAll("Scriptable Objects/Abilities", typeof(BaseAbility)).Cast<BaseAbility>().ToList().Where(x => x.AttackType == StoneType).ToList();
        AbilityAffected = AllAbilities[Random.Range(0, AllAbilities.Count)];
        AbilityAffected.SetMyElysianStoneQuality(Quality);
    }

    void RollStoneType()
    {
        StoneType = (BaseAbility.AttackTypeEnum)(Random.Range(0, (int)BaseAbility.AttackTypeEnum.zzzNUM_TYPES));
    }

    void RollRuneSlots()
    {
        switch(Quality)
        {
            case ItemQuality.QualityTypesEnum.Common:
                SlotsCounts = 0;
                break;
            case ItemQuality.QualityTypesEnum.Uncommon:
                SlotsCounts = 1;
                break;
            case ItemQuality.QualityTypesEnum.Rare:
                SlotsCounts = 2;
                break;
            case ItemQuality.QualityTypesEnum.Epic:
                SlotsCounts = 3;
                break;
            case ItemQuality.QualityTypesEnum.Legendary:
                SlotsCounts = 4;
                break;
            case ItemQuality.QualityTypesEnum.Artifact:
                SlotsCounts = 4;
                break;
        }
        RunestoneSlots = new ElysianChildSlots[SlotsCounts];

        if (RunestoneSlots.Length == 1)
        {
            RunestoneSlots[0] = new ElysianChildSlots();
            RunestoneSlots[0].AugmentSlot = RunestoneAugmentType.JURONIAN;
        }
        else
        {
            for (int i = 0; i < RunestoneSlots.Length; i++)
            {
                RunestoneSlots[i] = new ElysianChildSlots();
                if (i == RunestoneSlots.Length - 1)
                {
                    RunestoneSlots[i].AugmentSlot = RunestoneAugmentType.ARCANE;
                }
                else
                    RunestoneSlots[i].AugmentSlot = RunestoneAugmentType.JURONIAN;
            }
        }
    }
    #endregion
    //void DoArcaneAlteration(BaseAbility _ability)
    //{
    //    List<BaseProjectile> projectiles = _ability.VFX.GetComponentsInChildren<BaseProjectile>().ToList();
    //    foreach (BaseProjectile p in projectiles)
    //    {
    //        p.SetSpreadType(ArcaneStoneSlot.SpreadType);
    //    }
    //}

    public void SetAugment(BaseRunestone stone, int index)
    {
        RunestoneSlots[index].SlottedStone = stone;
        Debug.Log("My name: " + RuneStoneName + " has the following stones socketed");
        int slottedCount = 0;
        foreach(ElysianChildSlots child in RunestoneSlots)
        {
            if (child.SlottedStone != null)
            {
                slottedCount++;
                Debug.Log(child.SlottedStone.GetName()); 
            }
        }
        if(slottedCount == RunestoneSlots.Length)
        {
            // set the stone as complete
            isComplete = true;
            // Grab ALL of the mapping data for my quality level
            List<AbilityMappingData> AbilityMapping = new List<AbilityMappingData>();
            AbilityMapping = (Resources.Load<AbilityMappingDatabase>("Scriptable Objects/" + Quality + "AbilityMapping") as AbilityMappingDatabase).GetAbilityMappingList();
            // Put only my ability types in a separate list
            List<AbilityMappingData> MyElysianTypeMaps = new List<AbilityMappingData>();
            foreach(AbilityMappingData map in AbilityMapping)
            {
                if (map.GetElysianStoneType() == StoneType)
                    MyElysianTypeMaps.Add(map);
            }
            // Look through my possible abilities for where my J stones match the mapping data
            List<bool> JuronianTypeMatches = new List<bool>();
            List<BaseAbility> availableAbils = new List<BaseAbility>();
            foreach (AbilityMappingData map in MyElysianTypeMaps)
            {
                // Get a list of all Juronian types for this mapping
                var JuronianMatches = map.GetJuronianRequirements();
                // Look through the Slotted stones...
                foreach(ElysianChildSlots child in RunestoneSlots)
                {
                    JuronianStone j = (JuronianStone)child.SlottedStone;
                    // ...And match them against any of the J types in the mapping
                    bool found = JuronianMatches.Any(x => x == j.GetStoneType());
                    // if the stone matches one of the J stones in the mapping, add it to our list
                    if (found)
                        JuronianTypeMatches.Add(found);
                    // if the count of list of correct matches is the same as the number of stones slotted, then set the ability from this mapping
                    if (JuronianTypeMatches.Count == RunestoneSlots.Length)
                    {
                        AbilityAffected = map.GetMappedAbility();
                        AbilityAffected.SetMyElysianStoneQuality(Quality);
                        SetName();
                        RunestoneUI.StaticRunestoneUI.GetComponentInChildren<ElysianSocket>().GetComponentInChildren<Text>().text = GetName();
                    }
                        
                }
            }
            
        }
    }

    public ElysianChildSlots[] GetChildSlots()
    {
        return RunestoneSlots;
    }

    public class ElysianChildSlots
    {
        public RunestoneAugmentType AugmentSlot;
        public BaseRunestone SlottedStone;
    }
}
