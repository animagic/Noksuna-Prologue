using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class Fire : BaseAbility
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //public BaseSpell GetSpellToCast()
        //{
        //    return Spell;
        //}

        public List<BaseStatusEffect> GetStatusToCast()
        {
            return StatusEffectsForTarget;
        }

        public override void CastAbilityOnTarget(GameObject caster, GameObject target)
        {
            base.CastAbilityOnTarget(caster, target);
        }

        public override void CastAbilityAtDirection(GameObject caster, Vector3 direction)
        {
            base.CastAbilityAtDirection(caster, direction);
        }
    } 
}
