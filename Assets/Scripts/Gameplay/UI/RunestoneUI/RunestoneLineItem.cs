using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RunestoneLineItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject CurrentlyDraggedPlaceholder;
    public static BaseRunestone CurrentlyDraggedRunestone;
    public static GameObject CurrentlySelectedLineItem;
    public GameObject RuneDragObjectPrefab;
    GameObject tempRuneDragObject;
    Vector2 startPosition;
    Transform startParent;
    CanvasGroup cgroup;

    public BaseRunestone MyRunestone;

    [SerializeField]
    Sprite ElysianSprite;
    [SerializeField]
    Sprite JuronianSprite_Glacial;
    [SerializeField]
    Sprite JuronianSprite_Scorched;
    [SerializeField]
    Sprite JuronianSprite_Divine;
    [SerializeField]
    Sprite JuronianSprite_Shadow;
    [SerializeField]
    Sprite ArcaneSprite;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;
        tempRuneDragObject = Instantiate(RuneDragObjectPrefab, UIManager.StaticUIManager.GetComponentInChildren<RunestoneUI>().GetLineItemHolder().transform);
        Sprite s = tempRuneDragObject.GetComponent<Image>().sprite;
        switch(MyRunestone.BaseType)
        {
            case BaseRunestone.RunestoneType.ARCANE:
                s = ArcaneSprite;
                break;
            case BaseRunestone.RunestoneType.ELYSIAN:
                s = ElysianSprite;
                break;
            case BaseRunestone.RunestoneType.JURONIAN:
                JuronianStone j = (JuronianStone)MyRunestone;
                switch(j.GetStoneType())
                {
                    case JuronianStone.JuronianStoneTypeEnum.DIVINE:
                        s = JuronianSprite_Divine;
                        break;
                    case JuronianStone.JuronianStoneTypeEnum.SHADOW:
                        s = JuronianSprite_Shadow;
                        break;
                    case JuronianStone.JuronianStoneTypeEnum.GLACIAL:
                        s = JuronianSprite_Glacial;
                        break;
                    case JuronianStone.JuronianStoneTypeEnum.SCORCHED:
                        s = JuronianSprite_Scorched;
                        break;
                }
                break;
        }
        MyRunestone.UISprite = s;
        tempRuneDragObject.GetComponent<Image>().sprite = s;
        CurrentlyDraggedPlaceholder = tempRuneDragObject;
        CurrentlyDraggedRunestone = MyRunestone;
        CurrentlySelectedLineItem = gameObject;
        //transform.SetParent(UIManager.StaticUIManager.GetComponentInChildren<RunestoneUI>().GetLineItemHolder().transform);
        tempRuneDragObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        tempRuneDragObject.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CurrentlyDraggedPlaceholder = null;
        CurrentlyDraggedRunestone = null;
        CurrentlySelectedLineItem = null;
        if(transform.parent != startParent)
        {
            Player.StaticPlayer.GetComponent<CharacterInventory>().RemoveFromInventory(MyRunestone);
            
            Destroy(gameObject);
        }
        else
        {
            RuneDragObjectPrefab.transform.position = startPosition;
            Destroy(tempRuneDragObject);
            transform.SetParent(startParent);
        }

        tempRuneDragObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
