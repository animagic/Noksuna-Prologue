using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

    bool draggingItem = false;
    GameObject currentDragObject;
    Vector2 touchOffset;
    Vector2 CurrentPointerPosition
    {
        get
        {
            return Input.mousePosition;
        }
    }
    bool hasInput
    {
        get
        {
            return Input.GetMouseButton(0);
        }
    }

    


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightBracket))
            Player.StaticPlayer.Test_AddExperience();
        if (Input.GetKeyDown(KeyCode.Alpha0))
            Player.StaticPlayer.Test_ResetToLevelOne();

        if (Input.GetKeyDown(KeyCode.G))
            Player.StaticPlayer.GetComponent<CharacterInventory>().EquipMainHand();
        if (Input.GetKeyDown(KeyCode.H))
            Player.StaticPlayer.GetComponent<CharacterInventory>().EquipOffHand();
        if(Input.GetKeyDown(KeyCode.X))
        {
            Player.StaticPlayer.GetComponent<CharacterInventory>().SwapWeaponHands();
        }

        //CheckDragging();
	}

    //void CheckDragging()
    //{
    //    if(hasInput)
    //    {
    //        DragOrPickup();
    //    }
    //    else
    //    {
    //        if (draggingItem)
    //            DropItem();
    //    }
            
    //}

    //void DragOrPickup()
    //{
    //    Vector2 inputPosition = CurrentPointerPosition;
    //    if(draggingItem)
    //    {
    //        currentDragObject.transform.position = inputPosition + touchOffset;
    //    }
    //    else
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
    //        RaycastHit rayInfo;
    //        RaycastHit[] touches = Physics.RaycastAll(ray);
    //        if(touches.Length > 0)
    //        {
    //            rayInfo = touches[0];
    //            Debug.Log(rayInfo.transform.name);
    //            if(rayInfo.transform.GetComponent<RectTransform>() != null)
    //            {
    //                draggingItem = true;
    //                currentDragObject = rayInfo.transform.gameObject;
    //                touchOffset = (Vector2)rayInfo.transform.position - inputPosition;
    //                currentDragObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    //            }
    //        }

    //    }
    //}

    //void DropItem()
    //{
    //    draggingItem = false;
    //    currentDragObject.transform.localScale = new Vector3(1f, 1f, 1f);
    //}
}
