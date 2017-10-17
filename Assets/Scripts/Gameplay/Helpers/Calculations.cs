using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculations {

    public static bool CalcHit(GameObject caster, GameObject target)
    {
        //float hitNum = caster.Luck / 2 + caster.Hit - target.Eva - target.Luck;
        //if (caster.Hit >= 255)
        //    return true;
        //if (Random.Range(0, 100) < hitNum)
        //    return true;
        //else return false;
        throw new NotImplementedException();
    }

    public static bool CalcCrit(GameObject caster, GameObject target)
    {
        //float critNum = (caster.Luck + 1) / 256 * 100;
        //if (Random.Range(0, 100) < critNum)
        //{
        //    return true;
        //}
        //else return false;
        throw new NotImplementedException();
    }

    public static int CalcDamage(GameObject caster, GameObject target)
    {
        throw new NotImplementedException();
    }
}
