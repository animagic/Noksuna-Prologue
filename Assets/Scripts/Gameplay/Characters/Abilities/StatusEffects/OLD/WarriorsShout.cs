using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatusEffects
{
    public class WarriorsShout : BaseStatusEffect
    {
        public override void TriggerStatusEffect()
        {
            if (!IsTriggered)
            {
                IsTriggered = true;
                Debug.Log(this.GetType().Name + " status effect triggered!");
                AfflictedCharacter.AddBuffToCharacter(this); 
            }
        }
    } 
}
