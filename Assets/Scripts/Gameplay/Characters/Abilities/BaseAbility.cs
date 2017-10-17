using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseAbility : ScriptableObject
{
    public enum AbilityDamageTypeEnum
    {
        FIRE,
        ICE,
        LIGHTNING,
        WIND,
        WATER,
        EARTH,
        POISON,
        HOLY,
        HEAL,
        NORMAL
    }
    public enum AttackTypeEnum
    {
        MELEE,
        RANGED,
        MAGIC,
        BUFF,
        DEBUFF,

        zzzNUM_TYPES
    }
    //public enum AbilityTypeEnum
    //{
    //    NEEDS_A_TYPE,
    //    OFFENSIVE_ABILITY,
    //    OFFENSIVE_BUFF,
    //    DEFENSIVE_ABILITY,
    //    DEFENSIVE_BUFF,
    //    SUPPORT_BUFF,
    //}
    public enum AbilityTargetTypeEnum
    {
        SINGLE,
        AOE
    }
    public enum AbilityTargetMethodEnum
    {
        SELF,
        DIRECTIONAL,
        TARGET_POSITIONED,
        GROUND_POSITIONED
    }
    public enum AbilityCastTimeTypeEnum
    {
        INSTANT,
        TIMED,
        CHANNELED
    }

    [Header("Standard Values")]
    public string AbilityName;
    [Tooltip("The character's specific attack value is added to this to determine the overall power of the ability")]
    public int AbilityPower;
    public int Range;

    [Header("Ability Settings")]
    public AttackTypeEnum AttackType;
    //public AbilityTypeEnum AbilityType;
    public AbilityTargetTypeEnum TargetType;
    public AbilityDamageTypeEnum SpellDamageType;
    [Tooltip("This is used as a reference only for finding specific levels of spells based on Runestone quality")]
    public ItemQuality.QualityTypesEnum AbilityQuality;

    [Header("Cast Settings")]
    public AbilityCastTimeTypeEnum AbilityCastTimeType;
    public AbilityTargetMethodEnum AbilityTargetMethod;
    public float CastTime;
    public float Cooldown;
    public bool IsDirectionalIfNoTarget = false;
    
    [Header("Related Objects")]
    public Sprite Icon;
    public GameObject VFX;
    public ItemQuality.QualityTypesEnum MyElysianStoneQuality = ItemQuality.QualityTypesEnum.Common;

    //[SerializeField]
    //protected BaseSpell Spell;
    //public BaseSpell GetSpell()
    //{
    //    return Spell;
    //}

    public List<BaseStatusEffect> StatusEffectsForTarget = new List<BaseStatusEffect>();

    public List<BaseStatusEffect> StatusEffectsForSelf = new List<BaseStatusEffect>();

    public virtual void CastAbilityOnSelf(GameObject caster)
    {
        GameObject _spell = Instantiate(VFX, caster.transform.position, Quaternion.identity) as GameObject;
        AddStatusEffectsToSelf(caster);
    }

    public virtual void CastAbilityOnTarget(GameObject caster, GameObject target)
    {
        bool canCast = false;
        if (target == null)
        {
            List<RaycastHit> hits = Physics.SphereCastAll(caster.transform.position, 25.0f, caster.transform.forward).ToList();
            List<GameObject> enemies = new List<GameObject>();
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.GetComponent<Enemy>())
                {
                    enemies.Add(hit.transform.gameObject);
                }
            }
            if (enemies.Count > 0)
            {
                enemies = enemies.OrderBy(x => x.transform.position).ToList();
                target = enemies[0];
                canCast = true;
            }
        }
        else
            canCast = true;
        if (canCast && Vector3.Distance(caster.transform.position, target.transform.position) <= Range)
        {
            Vector3 targetCenter = target.GetComponent<Collider>().bounds.center;
            GameObject _spell = Instantiate(VFX, targetCenter, target.transform.rotation) as GameObject;
            Collider col = _spell.GetComponent<Collider>();
            Physics.IgnoreCollision(col, caster.GetComponent<Collider>());
            AddStatusEffectsToSelf(caster);
        }
        else if (IsDirectionalIfNoTarget)
            CastAbilityAtDirection(caster, caster.transform.forward);
        else
            Debug.Log("Cannot cast spell on target.  No targets found or targets are out of range.");
    }

    public virtual void CastAbilityAtDirection(GameObject caster, Vector3 direction)
    {
        Vector3 casterCenter = caster.GetComponent<Collider>().bounds.center;
        GameObject _spell = Instantiate(VFX, casterCenter, caster.transform.rotation) as GameObject;
        // Grab and initialize values for the spell's projectile parent (an empty "holder")
        BaseProjectile projectile = _spell.GetComponent<BaseProjectile>();
        projectile.Init(this, caster);
        AddStatusEffectsToSelf(caster);

    }

    void AddStatusEffectsToSelf(GameObject caster)
    {
        BaseCharacter _character = caster.GetComponent<BaseCharacter>();
        foreach(BaseStatusEffect effect in StatusEffectsForSelf)
        {
            _character.AddStatusEffect(effect, this);
        }
    }

    public void SetMyElysianStoneQuality(ItemQuality.QualityTypesEnum qual)
    {
        MyElysianStoneQuality = qual;
    }

    public void DoArcaneStoneAlteration(ArcaneStone stone)
    {
        switch(stone.ArcaneType)
        {
            case ArcaneStone.ArcaneStoneType.PROJECTILE_MODIFIER:
                (VFX.GetComponent<BaseProjectile>() as ParentProjectileHolder).SetProjectileType(stone.SpreadType);
                break;
            case ArcaneStone.ArcaneStoneType.STATUS_EFFECT_ADDITION:
                switch(stone.GetStatusEffect().TargetToEffect)
                {
                    case BaseStatusEffect.TargetToEffectEnum.SELF:
                        StatusEffectsForSelf.Add(stone.GetStatusEffect());
                            break;
                    case BaseStatusEffect.TargetToEffectEnum.TARGET:
                        StatusEffectsForTarget.Add(stone.GetStatusEffect());
                        break;
                }
                break;
        }
    }

}
