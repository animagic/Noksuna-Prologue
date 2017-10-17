using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityMapping", menuName = "Abilities/Mapping", order = 0)]
public class AbilityMappingDatabase : ScriptableObject {
    [SerializeField]
    ItemQuality.QualityTypesEnum AbilityQualityLevel;

    [SerializeField]
    List<AbilityMappingData> AbilityMappings;

    public List<AbilityMappingData> GetAbilityMappingList()
    {
        return AbilityMappings;
    }
}
