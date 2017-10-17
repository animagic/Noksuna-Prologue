using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityMappingData {

    [SerializeField]
    BaseAbility.AttackTypeEnum ElysianStoneType;
    [SerializeField]
    List<JuronianStone.JuronianStoneTypeEnum> JuronianTypes;
    [SerializeField]
    BaseAbility AbilityToMap;

    public BaseAbility.AttackTypeEnum GetElysianStoneType()
    {
        return ElysianStoneType;
    }

    public List<JuronianStone.JuronianStoneTypeEnum> GetJuronianRequirements()
    {
        return JuronianTypes;
    }

    public BaseAbility GetMappedAbility()
    {
        return AbilityToMap;
    }
}
