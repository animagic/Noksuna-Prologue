using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public static class ItemQuality
{
    public enum QualityTypesEnum
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Artifact,

        zzzNumQualities
    }

    public static Dictionary<QualityTypesEnum, float> QualityChanceDictionary = new Dictionary<QualityTypesEnum, float>()
    {
        { QualityTypesEnum.Common, 100.0f},
        { QualityTypesEnum.Uncommon, 40.0f },
        { QualityTypesEnum.Rare, 20.0f },
        { QualityTypesEnum.Epic, 10.0f },
        { QualityTypesEnum.Legendary, 5.0f }
    };

    public static Dictionary<QualityTypesEnum, float> QualityStatMultipliers = new Dictionary<QualityTypesEnum, float>()
    {
        { QualityTypesEnum.Common, 1.0f },
        { QualityTypesEnum.Uncommon, 1.0f },
        { QualityTypesEnum.Rare, 1.2f },
        { QualityTypesEnum.Epic, 1.5f },
        { QualityTypesEnum.Legendary, 2.0f }
    };

    public static Dictionary<QualityTypesEnum, float> QualityDOTMultipliers = new Dictionary<QualityTypesEnum, float>()
    {
        { QualityTypesEnum.Common, .10f },
        { QualityTypesEnum.Uncommon, .15f },
        { QualityTypesEnum.Rare, .20f },
        { QualityTypesEnum.Epic, .275f },
        { QualityTypesEnum.Legendary, .40f }
    };

    public static QualityTypesEnum RandomizeQuality(QualityTypesEnum highestQualityPossibleForItem, QualityTypesEnum lowestQualityPossibleForItem)
    {
        QualityTypesEnum itemQuality = QualityTypesEnum.Common;
        if (highestQualityPossibleForItem == lowestQualityPossibleForItem)
            return lowestQualityPossibleForItem;
        int rand = Random.Range(0, 100);
        if (rand <= QualityChanceDictionary[highestQualityPossibleForItem])
        {
            itemQuality = highestQualityPossibleForItem;
        }
        else
        {
            int index = (int)highestQualityPossibleForItem;
            if (index == 0)
                 itemQuality = QualityTypesEnum.Common;
            else
                return RandomizeQuality((QualityTypesEnum)((int)highestQualityPossibleForItem - 1), lowestQualityPossibleForItem);
        }
        return itemQuality; 
    }
}