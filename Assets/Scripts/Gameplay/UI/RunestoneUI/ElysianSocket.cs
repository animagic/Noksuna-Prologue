using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElysianSocket : ExtendedMonoBehaviour, IDropHandler
{
    GameObject draggedItem;
    public ElysianStone SocketedStone;

    public void SetActiveSocketedStone(ElysianStone stone)
    {
        RunestoneUI.StaticRunestoneUI.SetRunestoneSocketTypes(stone);
        SocketedStone = stone;
        GetComponent<Image>().sprite = SocketedStone.UISprite;
        GetComponentInChildren<Text>().text = SocketedStone.GetName();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (!draggedItem && RunestoneLineItem.CurrentlyDraggedRunestone.BaseType == BaseRunestone.RunestoneType.ELYSIAN)
        {
            draggedItem = RunestoneLineItem.CurrentlyDraggedPlaceholder;
            draggedItem.transform.SetParent(transform);
            draggedItem.GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y);
            draggedItem.transform.position = transform.position;
            draggedItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
            SetActiveSocketedStone(RunestoneLineItem.CurrentlyDraggedRunestone as ElysianStone);
        }
        else
        {
            //pop a modal thing and ask if the player wants to destroy the old socketed stone in order to socket the new one;

        }

        Debug.Log("Should we switch Elysian stones this way?");
    }
}

