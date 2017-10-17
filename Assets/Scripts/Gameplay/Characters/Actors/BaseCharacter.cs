using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseCharacter : ExtendedMonoBehaviour {

    protected string CharacterName = "";

    /// <summary>
    /// This class is kept here to more easily maintain a proper list of Stats a player has if they happen to change..
    /// It is used to allow a Status Effect (Buff/Debuff) to effect multiple stats at the same time
    /// </summary>
    [Serializable]
    public class StatsForStatusEffects
    {
        public enum BaseCharacterStatsEnum
        {
            HEALTH,
            STRENGTH,
            DEXTERITY,
            VITALITY,
            INTELLIGENCE,
            MELEE_ATTACK,
            RANGED_ATTACK,
            MAGIC_ATTACK,
            PHYSICAL_DEFENSE,
            MAGIC_DEFENSE
        }
        public BaseCharacterStatsEnum Stat;
        [Tooltip("For multiplicatave values: 20% buff is 1.2, 10% debuff is .9, etc.  For Additive values, put the value to add.")]
        public float Power;
        public BaseStatusEffect.StatusEffectValueTypeEnum ValueType;
    }

    [Serializable]
    public class BaseCharacterPrimaryStats
    {
        public int BaseHealth = 200;
        public int CurrentHealth;

        public int BaseStrength = 5;
        public int BaseDexterity = 5;
        public int BaseVitality = 5;
        public int BaseIntelligence = 5;

        public int CurrentStrength;
        public int CurrentDexterity;
        public int CurrentVitality;
        public int CurrentIntelligence;

        [Tooltip("Base value for STR, DEX, VIT, INT stats.")]
        public int BasePrimaryStat = 5;
        [Tooltip("Value of each STR, DEX, VIT, INT that the character gains per level.")]
        public int BasePrimaryStatAdditive = 1;
        [Tooltip("Base health value that the character gains each level.")]
        public int BaseHealthAdditive = 200;
        [Tooltip("Level 0 Health")]
        public int StartingHealth = 200;

        public List<BaseStatusEffect> AdditiveBuffs = new List<BaseStatusEffect>();
        public List<BaseStatusEffect> MultiplicativeBuffs = new List<BaseStatusEffect>();
    }
    [SerializeField]
    protected BaseCharacterPrimaryStats PrimaryStats;
    public BaseCharacterPrimaryStats GetPrimaryStats() { return PrimaryStats; }

    [Serializable]
    public class BaseCharacterAttackStats
    {
        public int BaseMeleeAttack = 10;
        public int BaseRangedAttack = 10;
        public int BaseMagicAttack = 10;

        public int CurrentMeleeAttack;
        public int CurrentRangedAttack;
        public int CurrentMagicAttack;

        [Tooltip("Stat value the character gains for each level.")]
        public int BaseAttackValueAdditive = 10;

        //public List<BaseStatusEffect> AdditiveBuffs = new List<BaseStatusEffect>();
        //public List<BaseStatusEffect> MultiplicativeBuffs = new List<BaseStatusEffect>();
    }
    [SerializeField]
    protected BaseCharacterAttackStats AttackStats;
    public BaseCharacterAttackStats GetAttackStats() { return AttackStats; }

    [Serializable]
    public class BaseCharacterDefenseStats
    {
        public int BasePhysicalDefenseValue = 5;
        public int BaseMagicDefenseValue = 5;

        public int CurrentPhysicalDefense;
        public int CurrentMagicDefense;

        [Tooltip("Stat value the character gains for each level.")]
        public int BaseDefenseValueAdditive = 5;

        //public List<BaseStatusEffect> AdditiveBuffs = new List<BaseStatusEffect>();
        //public List<BaseStatusEffect> MultiplicativeBuffs = new List<BaseStatusEffect>();
    }
    [SerializeField]
    protected BaseCharacterDefenseStats DefenseStats;
    public BaseCharacterDefenseStats GetDefenseStats() { return DefenseStats; }

    [Serializable]
    public class BaseCharacterExperienceStats
    {
        public int CharacterLevel = 1;
        public int CharacterMaxLevel = 50;
        public int CharacterExperience = 0;
        public int CharacterExperienceTNL = 0;
    }
    [SerializeField]
    protected BaseCharacterExperienceStats ExperienceStats;
    public BaseCharacterExperienceStats GetExperienceStats() { return ExperienceStats; }

    [Tooltip("Only used for debug purposes.")]
    [SerializeField]
    protected List<BaseStatusEffect> AfflictedStatusEffects = new List<BaseStatusEffect>();
    protected Dictionary<BaseStatusEffect, float> StatusEffectDurationDictionary = new Dictionary<BaseStatusEffect, float>();
    Dictionary<BaseStatusEffect, float> ActiveTimers = new Dictionary<BaseStatusEffect, float>();

    [Serializable]
    public class CharacterActionClass
    {
        public ActorAbilityManager MyAbilityManager;
        public GameObject CurrentTarget;
    }
    public CharacterActionClass CharacterActionReferences;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region EXPERIENCE CONTROL METHODS
    public int GetCurrentLevel()
    {
        return ExperienceStats.CharacterLevel;
    }

    public int GetMaxLevel()
    {
        return ExperienceStats.CharacterMaxLevel;
    }

    public int GetCurrentExperience()
    {
        return ExperienceStats.CharacterExperience;
    }

    public int GetCurrentTNL()
    {
        return ExperienceStats.CharacterExperienceTNL;
    }

    public void AddExperience(int _exp)
    {
        int curTNL = ExperienceStats.CharacterExperienceTNL - ExperienceStats.CharacterExperience;
        if (_exp > curTNL)
        {
            int excessExp = _exp - curTNL;
            Debug.Log("excess: " + excessExp + "_exp: " + _exp + ", tnl: " + curTNL);
            ExperienceStats.CharacterExperience += curTNL;
            if (ExperienceStats.CharacterLevel < ExperienceStats.CharacterMaxLevel)
            {
                LevelUp();
                ExperienceStats.CharacterExperience = 0;
                AddExperience(excessExp);
                
            }
        }
        else
            ExperienceStats.CharacterExperience += _exp;
        EventManager.TriggerEvent(EventManager.EventNames.UpdatePlayerUIValues);
    }

    public virtual int SetCurrentLevelMaxExperience(int _playerLevel)
    {
        throw new NotImplementedException();
    }

    protected virtual void LevelUp()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region STAT CONTROL METHODS
    protected void SetBaseStatValues(int _playerLevel)
    {
        PrimaryStats.BaseStrength = PrimaryStats.BasePrimaryStat + (_playerLevel * PrimaryStats.BasePrimaryStatAdditive);
        PrimaryStats.BaseDexterity = PrimaryStats.BasePrimaryStat + (_playerLevel * PrimaryStats.BasePrimaryStatAdditive);
        PrimaryStats.BaseVitality = PrimaryStats.BasePrimaryStat + (_playerLevel * PrimaryStats.BasePrimaryStatAdditive);
        PrimaryStats.BaseIntelligence = PrimaryStats.BasePrimaryStat + (_playerLevel * PrimaryStats.BasePrimaryStatAdditive);

        PrimaryStats.BaseHealth = PrimaryStats.StartingHealth + (_playerLevel * PrimaryStats.BaseHealthAdditive) + (PrimaryStats.BaseVitality);
        PrimaryStats.CurrentHealth = PrimaryStats.BaseHealth;

        AttackStats.BaseMeleeAttack = AttackStats.BaseAttackValueAdditive + (int)(_playerLevel * AttackStats.BaseAttackValueAdditive * .5f) + (int)(PrimaryStats.BaseStrength * 2);
        AttackStats.BaseRangedAttack = AttackStats.BaseAttackValueAdditive + (int)(_playerLevel * AttackStats.BaseAttackValueAdditive * .5f) + (int)(PrimaryStats.BaseDexterity * 2);
        AttackStats.BaseMagicAttack = AttackStats.BaseAttackValueAdditive + (int)(_playerLevel * AttackStats.BaseAttackValueAdditive * .5f) + (int)(PrimaryStats.BaseIntelligence * 2);

        DefenseStats.BasePhysicalDefenseValue = DefenseStats.BaseDefenseValueAdditive + (int)(_playerLevel * DefenseStats.BaseDefenseValueAdditive * .5f);
        DefenseStats.BaseMagicDefenseValue = DefenseStats.BaseDefenseValueAdditive + (int)(_playerLevel * DefenseStats.BaseDefenseValueAdditive * .5f);

        UpdateAllStatValues();
        string s = string.Format("Level: {5}, STR/DEX/VIT/INT: {6}/{7}/{8}/{9}, Melee Attack: {0}, Ranged Attack: {1}, Magic Attack: {2}, PhysicalDefense: {3}, MagicDefense: {4}",
            AttackStats.BaseMeleeAttack,
            AttackStats.BaseRangedAttack,
            AttackStats.BaseMagicAttack,
            DefenseStats.BasePhysicalDefenseValue,
            DefenseStats.BaseMagicDefenseValue,
            _playerLevel,
            PrimaryStats.BaseStrength,
            PrimaryStats.BaseDexterity,
            PrimaryStats.BaseVitality,
            PrimaryStats.BaseIntelligence);
    }

    protected void UpdateAllStatValues()
    {
        UpdatePrimaryStatValues();
        UpdateAttackStatValues();
        UpdateDefenseStatValues();
    }

    protected void UpdatePrimaryStatValues()
    {
        int healthAdds = 0;
        int strAdds = 0;
        int dexAdds = 0;
        int vitAdds = 0;
        int intAdds = 0;
        if(PrimaryStats.AdditiveBuffs.Count > 0)
        {
            foreach(BaseStatusEffect effect in PrimaryStats.AdditiveBuffs)
            {
                foreach (StatsForStatusEffects effected in effect.StatsEffected)
                {
                    switch (effected.Stat)
                    {
                        case StatsForStatusEffects.BaseCharacterStatsEnum.HEALTH:
                            healthAdds += (int)effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.STRENGTH:
                            strAdds += (int)effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.DEXTERITY:
                            dexAdds += (int)effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.VITALITY:
                            vitAdds += (int)effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.INTELLIGENCE:
                            intAdds += (int)effected.Power;
                            break;
                    } 
                }
            }
        }
        
        PrimaryStats.CurrentStrength = PrimaryStats.BaseStrength + strAdds;
        PrimaryStats.CurrentDexterity = PrimaryStats.BaseDexterity + dexAdds;
        PrimaryStats.CurrentVitality = PrimaryStats.BaseVitality + vitAdds;
        PrimaryStats.CurrentIntelligence = PrimaryStats.BaseIntelligence + intAdds;

        float healthVals = 1;
        float strVals = 1;
        float dexVals = 1;
        float vitVals = 1;
        float intVals = 1;
        if (PrimaryStats.MultiplicativeBuffs.Count > 0)
        {
            foreach (BaseStatusEffect effect in PrimaryStats.MultiplicativeBuffs)
            {
                foreach (StatsForStatusEffects effected in effect.StatsEffected)
                {
                    switch (effected.Stat)
                    {
                        case StatsForStatusEffects.BaseCharacterStatsEnum.HEALTH:
                            healthVals *= effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.STRENGTH:
                            strVals *= effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.DEXTERITY:
                            dexVals *= effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.VITALITY:
                            vitVals *= effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.INTELLIGENCE:
                            intVals *= effected.Power;
                            break;
                    } 
                }
            }
        }

        int maxhealth = PrimaryStats.BaseHealth + healthAdds;
        int curHealth = PrimaryStats.CurrentHealth;
        int diff = maxhealth - curHealth;
        PrimaryStats.CurrentHealth = Mathf.RoundToInt((PrimaryStats.BaseHealth + healthAdds) * healthVals) - diff;

        PrimaryStats.CurrentStrength = PrimaryStats.BaseStrength * Mathf.RoundToInt(strVals);
        PrimaryStats.CurrentDexterity = PrimaryStats.BaseDexterity * Mathf.RoundToInt(dexVals);
        PrimaryStats.CurrentVitality = PrimaryStats.BaseVitality * Mathf.RoundToInt(vitVals);
        PrimaryStats.CurrentIntelligence = PrimaryStats.BaseIntelligence * Mathf.RoundToInt(intVals);
    }

    protected void UpdateAttackStatValues()
    {
        int magicAdds = 0;
        int meleeAdds = 0;
        int rangedAdds = 0;
        if(PrimaryStats.AdditiveBuffs.Count > 0)
        {
            foreach(BaseStatusEffect effect in PrimaryStats.AdditiveBuffs)
            {
                foreach (StatsForStatusEffects effected in effect.StatsEffected)
                {
                    switch (effected.Stat)
                    {
                        case StatsForStatusEffects.BaseCharacterStatsEnum.MAGIC_ATTACK:
                            magicAdds += (int)effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.MELEE_ATTACK:
                            meleeAdds += (int)effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.RANGED_ATTACK:
                            rangedAdds += (int)effected.Power;
                            break;
                    } 
                }
            }
        }
        AttackStats.CurrentMagicAttack = AttackStats.BaseMagicAttack + magicAdds;
        AttackStats.CurrentMeleeAttack = AttackStats.BaseMeleeAttack + meleeAdds;
        AttackStats.CurrentRangedAttack = AttackStats.BaseRangedAttack + rangedAdds;

        float magicVal = AttackStats.CurrentMagicAttack;
        float meleeVal = AttackStats.CurrentMeleeAttack;
        float rangedVal = AttackStats.CurrentRangedAttack;
        if(PrimaryStats.MultiplicativeBuffs.Count > 0)
        {
            foreach(BaseStatusEffect effect in PrimaryStats.MultiplicativeBuffs)
            {
                foreach (StatsForStatusEffects effected in effect.StatsEffected)
                {
                    switch (effected.Stat)
                    {
                        case StatsForStatusEffects.BaseCharacterStatsEnum.MAGIC_ATTACK:
                            magicVal *= effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.MELEE_ATTACK:
                            meleeVal *= effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.RANGED_ATTACK:
                            rangedVal *= effected.Power;
                            break;
                    } 
                }
            }
        }

        AttackStats.CurrentMagicAttack = Mathf.RoundToInt(magicVal);
        AttackStats.CurrentMeleeAttack = Mathf.RoundToInt(meleeVal);
        AttackStats.CurrentRangedAttack = Mathf.RoundToInt(rangedVal);
    }

    protected void UpdateDefenseStatValues()
    {
        int magicAdds = 0;
        int physicalAdds = 0;
        if (PrimaryStats.AdditiveBuffs.Count > 0)
        {
            foreach (BaseStatusEffect effect in PrimaryStats.AdditiveBuffs)
            {
                foreach (StatsForStatusEffects effected in effect.StatsEffected)
                {
                    switch (effected.Stat)
                    {
                        case StatsForStatusEffects.BaseCharacterStatsEnum.MAGIC_DEFENSE:
                            magicAdds += (int)effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.PHYSICAL_DEFENSE:
                            physicalAdds += (int)effected.Power;
                            break;
                    } 
                }
            }
        }
        DefenseStats.CurrentMagicDefense = DefenseStats.BaseMagicDefenseValue + magicAdds;
        DefenseStats.CurrentPhysicalDefense = DefenseStats.BasePhysicalDefenseValue + physicalAdds;

        float magicVal = DefenseStats.CurrentMagicDefense;
        float physicalVal = DefenseStats.CurrentPhysicalDefense;
        if (PrimaryStats.MultiplicativeBuffs.Count > 0)
        {
            foreach (BaseStatusEffect effect in PrimaryStats.MultiplicativeBuffs)
            {
                foreach (StatsForStatusEffects effected in effect.StatsEffected)
                {
                    switch (effected.Stat)
                    {
                        case StatsForStatusEffects.BaseCharacterStatsEnum.MAGIC_DEFENSE:
                            magicVal *= effected.Power;
                            break;
                        case StatsForStatusEffects.BaseCharacterStatsEnum.PHYSICAL_DEFENSE:
                            physicalVal *= effected.Power;
                            break;
                    } 
                }
            }
        }

        DefenseStats.CurrentMagicDefense = Mathf.RoundToInt(magicVal);
        DefenseStats.CurrentPhysicalDefense = Mathf.RoundToInt(physicalVal);
    }

    #endregion

    #region STATUS EFFECTS CONTROL METHODS
    /// <summary>
    /// This is only used by a Status Effect itself to add its buff to the player.  Is called by the Status Effect when it is Triggered.
    /// </summary>
    /// <param name="effect"></param>
    public void AddBuffToCharacter(BaseStatusEffect effect)
    {
        bool hasAdditive = false;
        bool hasMultiplicative = false;
        foreach (StatsForStatusEffects effected in effect.StatsEffected)
        {
            
            switch(effected.ValueType)
            {
                case BaseStatusEffect.StatusEffectValueTypeEnum.ADDITIVE:
                    hasAdditive = true;
                    break;
                case BaseStatusEffect.StatusEffectValueTypeEnum.MULTIPLICATIVE:
                    hasMultiplicative = true;
                    break;
            }
        }
        if (hasAdditive)
            PrimaryStats.AdditiveBuffs.Add(effect);
        if (hasMultiplicative)
            PrimaryStats.MultiplicativeBuffs.Add(effect);
        UpdateAllStatValues();
    }

    public Dictionary<BaseStatusEffect, float> GetStatusEffects()
    {
        return StatusEffectDurationDictionary;
    }

    /// <summary>
    /// ALL Status Effects are added to a character using this method.
    /// </summary>
    /// <param name="_effect"></param>
    public void AddStatusEffect(BaseStatusEffect _effect, BaseAbility _castingAbility)
    {
        BaseStatusEffect effectNew = ScriptableObject.CreateInstance(_effect.GetType()) as BaseStatusEffect;
        effectNew.Init(_effect, _castingAbility);
        effectNew.SetAfflictedCharacter(this);
        if (!AfflictedStatusEffects.Any(x => x.Name == _effect.Name) /*|| _effect.GetIsMultipleAllowed()*/)
        {
            AfflictedStatusEffects.Add(effectNew);
            StatusEffectDurationDictionary.Add(effectNew, effectNew.CurrentDuration);
        }
        else
        {
            Debug.Log(_effect.Name + " already exists on " + CharacterName);
            RemoveStatusEffect(_effect);
            AfflictedStatusEffects.Add(effectNew);
            StatusEffectDurationDictionary.Add(effectNew, effectNew.CurrentDuration);
        }
    }

    public void RemoveStatusEffect(BaseStatusEffect _effect)
    {
        AfflictedStatusEffects.Remove(_effect);
        StatusEffectDurationDictionary.Remove(_effect);
        CheckAndRemoveStatBuffs(_effect);
    }

    public List<BaseStatusEffect> GetCurrentBuffEffects()
    {
        List<BaseStatusEffect> curEffects = new List<BaseStatusEffect>();
        curEffects.AddRange(PrimaryStats.AdditiveBuffs);
        curEffects.AddRange(PrimaryStats.MultiplicativeBuffs);
        return curEffects;
    }
    void CheckAndRemoveStatBuffs(BaseStatusEffect _effect)
    {
        bool buffRemoved = false;
        if (PrimaryStats.AdditiveBuffs.Any(x => x == _effect))
        {
            buffRemoved = true;
            PrimaryStats.MultiplicativeBuffs.Remove(_effect);
        }
            PrimaryStats.AdditiveBuffs.Remove(_effect);
        if (PrimaryStats.MultiplicativeBuffs.Any(x => x == _effect))
        {
            buffRemoved = true;
            PrimaryStats.MultiplicativeBuffs.Remove(_effect);
        }
        if(buffRemoved)
            UpdateAllStatValues();
    }

    protected void CountDownStatusEffects()
    {
        CheckIfTriggerStatusEffect();
        ActiveTimers = new Dictionary<BaseStatusEffect, float>();
        foreach (KeyValuePair<BaseStatusEffect, float> timer in StatusEffectDurationDictionary)
        {
            float f = timer.Value - Time.deltaTime;
            ActiveTimers.Add(timer.Key, f);
        }
        foreach (KeyValuePair<BaseStatusEffect, float> timer in ActiveTimers)
        {
            if (timer.Value <= 0.0f)
            {
                RemoveStatusEffect(timer.Key);
            }
            else
                StatusEffectDurationDictionary[timer.Key] = timer.Value;
        }
    }

    protected void CheckIfTriggerStatusEffect()
    {
        foreach (KeyValuePair<BaseStatusEffect, float> effect in StatusEffectDurationDictionary)
        {
            effect.Key.IntervalTimer -= Time.deltaTime;
            if (effect.Key.IntervalTimer <= 0 || effect.Key.StatusEffectTriggerType == BaseStatusEffect.StatusEffectTriggerTypeEnum.CONSTANT)
            {
                effect.Key.IntervalTimer = effect.Key.TimeIntervalToActivate;
                effect.Key.TriggerStatusEffect();
            }
        }
    }

    #endregion

    #region DAMAGE AND HEALING METHODS
    public void TakeDamage(int damage)
    {
        PrimaryStats.CurrentHealth -= damage;
        Debug.Log("Damage taken: " + damage + ", Health left: " + PrimaryStats.CurrentHealth);
        if(PrimaryStats.CurrentHealth <= 0)
        {
            CharacterDeath();
        }
    }

    public void TakeDamagePercentage(float percentage)
    {
        int curHealth = PrimaryStats.CurrentHealth;
        PrimaryStats.CurrentHealth = (int)(PrimaryStats.CurrentHealth * percentage);
        int newHealth = PrimaryStats.CurrentHealth;
        Debug.Log("Damage taken: " + (curHealth - newHealth) + ", Health left: " + PrimaryStats.CurrentHealth);
        if (PrimaryStats.CurrentHealth <= 0)
        {
            CharacterDeath();
        }
    }

    public void CharacterDeath()
    {
        WithComponent<MeshRenderer>(x => x.enabled = false);
        WithComponent<SkinnedMeshRenderer>(x => x.enabled = false);
        Debug.Log(name + " has been killed");
        // DIEEEEEEEEEEEEEEEEEEq
    }
    #endregion
    #region TEST METHODS FOR TEST RUNNER
    public void Test_SetBaseStatValues(int _playerLevel)
    {
        SetBaseStatValues(_playerLevel);
    }

    public void Test_ResetToLevelOne()
    {
        ExperienceStats.CharacterLevel = 1;
        ExperienceStats.CharacterExperience = 0;
        SetCurrentLevelMaxExperience(ExperienceStats.CharacterLevel);
        SetBaseStatValues(ExperienceStats.CharacterLevel);
    }
    public void Test_LevelUp()
    {
        LevelUp();
    }

    public void Test_AddExperience()
    {
        AddExperience(1000);
    }

    #endregion
}
