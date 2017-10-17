using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSkillSlot : MonoBehaviour {

    [SerializeField]
    BaseAbility Ability;

    // Use this for initialization
    void Start () {
        SetIcon();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetIcon()
    {
        GetComponent<Image>().sprite = Ability.Icon;
    }

    //public void SetSpreadType()
    //{
    //    int curIndex = (int)Ability.VFX.GetComponent<BaseProjectile>().GetSpreadType();
    //    curIndex++;
    //    if (curIndex > (int)BaseProjectile.SpreadTypeEnum.__MAX_DONT_USE - 1)
    //        curIndex = 0;
    //    Ability.VFX.GetComponent<BaseProjectile>().SetSpreadType((BaseProjectile.SpreadTypeEnum)curIndex);
    //}

    //public void AddDamage(int damage)
    //{
    //    Ability.AbilityPower += damage;
    //    Ability.VFX.GetComponent<BaseProjectile>().Damage = damage;
    //}
}
