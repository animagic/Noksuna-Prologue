using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatusEffects
{
    public class Paralyze : BaseStatusEffect
    {

        public override void TriggerStatusEffect()
        {
            Debug.Log(this.GetType().Name + " status effect triggered!");
            if (Random.Range(0, 1) <= ChanceToActivate)
            {
                //paralyze the character
                Debug.Log("Need a Trigger Effect for Paralyze");
            }
        }
    } 
}
