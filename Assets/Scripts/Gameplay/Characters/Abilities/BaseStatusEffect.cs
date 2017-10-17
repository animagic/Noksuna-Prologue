using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseStatusEffect : ScriptableObject {

    public string Name;
    [Tooltip("Max Duration this effect can last for, given any time increasing buffs")]
    public float MaxDuration;
    [Tooltip("The Standard Duration that this buff uses before calculating any time increasing buffs")]
    public float InitialDuration;
    [Tooltip("This is a timer and anything you put here will be overwritten automatically.")]
    public float CurrentDuration;

    [Tooltip("The time, in seconds, this effect checks to triggers itself")]
    public float TimeIntervalToActivate;
    [Tooltip("This is a timer and anything you put here will be overwritten automatically.")]
    public float IntervalTimer;
    [Tooltip("% checks if it is less than this value to determine if the status effect triggers or not for Interval based effects")]
    [Range(0,1)]
    public float ChanceToActivate;

    //[Tooltip("For multiplicatave values: 20% buff is 1.2, 10% debuff is .9, etc.  For Additive values, put the value to add.")]
    //public float Power;
    [Tooltip("Icon to be used for the UI to show the effect")]
    public Sprite Icon;
    [Tooltip("Particle Effect to be used for a visual effect on the character (if any)")]
    public ParticleSystem VFX;

    protected ItemQuality.QualityTypesEnum MyQuality;

    public List<BaseCharacter.StatsForStatusEffects> StatsEffected = new List<BaseCharacter.StatsForStatusEffects>();

    public enum TargetToEffectEnum
    {
        SELF,
        TARGET
    }
    public TargetToEffectEnum TargetToEffect;
    public enum StatusEffectValueTypeEnum
    {
        MULTIPLICATIVE,
        ADDITIVE
    }
    //public StatusEffectValueTypeEnum StatusEffectValueType;

    public enum StatusEffectTriggerTypeEnum
    {
        CONSTANT,
        INTERVAL
    }
    public StatusEffectTriggerTypeEnum StatusEffectTriggerType;

    protected BaseCharacter AfflictedCharacter;
    protected bool IsTriggered = false;
    [Tooltip("If true, this effect can be stacked on the character and all will have their own timers and trigger chances")]
    [SerializeField]
    protected bool IsMultipleAllowed = false;

    public BaseStatusEffect()
    {
        Name = "";
        MaxDuration = 0.0f;
        InitialDuration = 0.0f;
        CurrentDuration = 0.0f;
        TimeIntervalToActivate = 3.0f;
        ChanceToActivate = .5f;
        //Power = 0.0f;
    }

    public void SetAfflictedCharacter(BaseCharacter _character)
    {
        AfflictedCharacter = _character;
    }

    public virtual void TriggerStatusEffect()
    {

    }

    public void Init(BaseStatusEffect _effect, BaseAbility _castingAbility)
    {
        Name = _effect.Name;
        MaxDuration = _effect.MaxDuration;
        InitialDuration = _effect.InitialDuration;
        CurrentDuration = _effect.InitialDuration;
        TimeIntervalToActivate = _effect.TimeIntervalToActivate;
        IntervalTimer = _effect.TimeIntervalToActivate;
        ChanceToActivate = _effect.ChanceToActivate;
        Icon = _effect.Icon;
        VFX = _effect.VFX;
        StatsEffected = _effect.StatsEffected;
        MyQuality = _castingAbility.MyElysianStoneQuality;
        StatusEffectTriggerType = _effect.StatusEffectTriggerType;
        IsTriggered = false;
        IsMultipleAllowed = false;
    }


    public bool GetIsMultipleAllowed()
    {
        return IsMultipleAllowed;
    }
    //public virtual void CountDownStatusEffect()
    //{
    //    CurrentDuration -= (Time.fixedDeltaTime / TimeIntervalToActivate);
    //    CheckIfTriggerStatusEffect();
    //}

    //protected virtual void CheckIfTriggerStatusEffect()
    //{
    //    Debug.Log("current Duration of status effect: " + CurrentDuration % TimeIntervalToActivate);
    //    if (CurrentDuration % TimeIntervalToActivate == 0)
    //    {
    //        if (Random.Range(0, 1) <= ChanceToActivate)
    //            TriggerStatusEffect();
    //    }
    //}
}
