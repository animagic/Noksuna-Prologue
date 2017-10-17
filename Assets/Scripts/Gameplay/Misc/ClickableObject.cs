using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

class ClickableObject : ExtendedMonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.WithComponent<RunestoneLineItem>(x =>
        {
            //if (x.MyRunestone.BaseType == BaseRunestone.RunestoneType.ELYSIAN)
            //    UIManager.StaticUIManager.GetComponentInChildren<RunestoneUI>().Test_ChangeElysianStone((ElysianStone)x.MyRunestone);
        });

    }
}

