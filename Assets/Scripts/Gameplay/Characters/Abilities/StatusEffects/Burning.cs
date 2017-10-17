using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatusEffects
{
    public class Burning : BaseStatusEffect
    {
        public override void TriggerStatusEffect()
        {
            Debug.Log(this.GetType().Name + "Status effect triggered!");
            float dmg = 0;
            foreach (var effect in StatsEffected)
            {
                if (effect.Stat == BaseCharacter.StatsForStatusEffects.BaseCharacterStatsEnum.HEALTH)
                {
                    dmg = effect.Power * ItemQuality.QualityDOTMultipliers[MyQuality];
                }
            }
            AfflictedCharacter.TakeDamage((int)dmg);
        }
    } 
}
