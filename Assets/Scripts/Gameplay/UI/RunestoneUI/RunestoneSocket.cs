using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RunestoneSocket : ExtendedMonoBehaviour, IDropHandler
{
    GameObject draggedItem;
    public BaseRunestone SocketedStone;
    public BaseRunestone.RunestoneAugmentType AugmentType;
    public int MySocketIndex = 0;
   

    public void OnDrop(PointerEventData eventData)
    {
        if (!draggedItem && (RunestoneLineItem.CurrentlyDraggedRunestone.BaseType == BaseRunestone.RunestoneType.JURONIAN && AugmentType == BaseRunestone.RunestoneAugmentType.JURONIAN))
        {
            SetStone();
            DestroyDraggedItems();
        }
        else if (!draggedItem && (RunestoneLineItem.CurrentlyDraggedRunestone.BaseType == BaseRunestone.RunestoneType.ARCANE && AugmentType == BaseRunestone.RunestoneAugmentType.ARCANE))
        {
            SetStone();
            RunestoneUI.CurrentElysianStone.GetAbilityAffected().DoArcaneStoneAlteration(RunestoneLineItem.CurrentlyDraggedRunestone as ArcaneStone);
            DestroyDraggedItems();
        }
        else
        {
            //pop a modal thing and ask if the player wants to destroy the old socketed stone in order to socket the new one;

        }
    }

    void SetStone()
    {
        draggedItem = RunestoneLineItem.CurrentlyDraggedPlaceholder;
        //draggedItem.transform.SetParent(transform);
        //draggedItem.GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y);
        //draggedItem.transform.position = transform.position;
        SocketedStone = RunestoneLineItem.CurrentlyDraggedRunestone;
        GetComponent<Image>().sprite = SocketedStone.UISprite;
        GetComponent<Image>().color = Color.white;
        RunestoneUI.CurrentElysianStone.SetAugment(SocketedStone, MySocketIndex);
    }

    void DestroyDraggedItems()
    {
        Destroy(RunestoneLineItem.CurrentlySelectedLineItem);
        Destroy(RunestoneLineItem.CurrentlyDraggedPlaceholder);
    }
}

