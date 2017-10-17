using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RunestoneUI : MonoBehaviour {

    public static RunestoneUI StaticRunestoneUI;

    public ElysianSocket SocketHolder;
    public static ElysianStone CurrentElysianStone;
    public List<RunestoneSocket> StoneSockets = new List<RunestoneSocket>();
    [SerializeField]
    GameObject StoneLineItem;
    [SerializeField]
    GameObject DraggedRunestoneHolder;

    [Header("Runestone Content Views")]
    [SerializeField]
    GameObject ElysianStoneInventoryContentPanel;
    [SerializeField]
    GameObject JuronianStoneInventoryControlPanel;
    [SerializeField]
    GameObject ArcaneStoneInventoryControlPanel;

    [Header("Runestone List Parents")]
    [SerializeField]
    List<GameObject> ListParents;


    CharacterInventory inv;
    private void Awake()
    {
        StaticRunestoneUI = this;
    }
    // Use this for initialization
    void Start () {
        inv = Player.StaticPlayer.GetComponent<CharacterInventory>();
        Test_AddStonesToInventory();
        for(int i = 1; i < ListParents.Count; i++)
        {
            ListParents[i].SetActive(false);
        }
        for(int j = 0; j < StoneSockets.Count; j++)
        {
            StoneSockets[j].gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject GetLineItemHolder()
    {
        return DraggedRunestoneHolder;
    }

    void Test_AddStonesToInventory()
    {
        for (int i = 0; i < 10; i++)
        {

            ElysianStone eStone = new ElysianStone();
            eStone.RandomizeAttributes();
            inv.AddRunestoneToInventory(eStone);

            JuronianStone jStone = new JuronianStone();
            jStone.RandomizeAttributes();
            inv.AddRunestoneToInventory(jStone);

            ArcaneStone aStone = new ArcaneStone();
            aStone.RandomizeAttributes();
            inv.AddRunestoneToInventory(aStone);
        }
        PopulateRunestonePanels();
        ResizeContentView(ElysianStoneInventoryContentPanel);
        ResizeContentView(JuronianStoneInventoryControlPanel);
        ResizeContentView(ArcaneStoneInventoryControlPanel);
    }

    void PopulateRunestonePanels()
    {
        foreach(BaseRunestone b in inv.GetRunestoneInventory())
        {
            GameObject tempLineItem = Instantiate(StoneLineItem);

            switch (b.BaseType)
            {
                case BaseRunestone.RunestoneType.ARCANE:
                    ArcaneStone a = (ArcaneStone)b;
                    tempLineItem.GetComponentInChildren<Text>().text = a.GetName();
                    AddRunestoneToPanel(tempLineItem, ArcaneStoneInventoryControlPanel, b);
                    break;
                case BaseRunestone.RunestoneType.ELYSIAN:
                    ElysianStone e = (ElysianStone)b;
                    tempLineItem.GetComponentInChildren<Text>().text = e.GetName();
                    AddRunestoneToPanel(tempLineItem, ElysianStoneInventoryContentPanel, b);
                    break;
                case BaseRunestone.RunestoneType.JURONIAN:
                    JuronianStone j = (JuronianStone)b;
                    tempLineItem.GetComponentInChildren<Text>().text = j.GetName();
                    AddRunestoneToPanel(tempLineItem, JuronianStoneInventoryControlPanel, b);
                    break;
            }
        }
    }

    void AddRunestoneToPanel(GameObject lineItem, GameObject contentView, BaseRunestone stone)
    {
        lineItem.transform.SetParent(contentView.transform, false);
        lineItem.GetComponent<RunestoneLineItem>().MyRunestone = stone;
    }

    void ResizeContentView(GameObject gameObject)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, 20 * 30);
    }

    public void ShowStoneList(int index)
    {
        for(int i = 0; i < ListParents.Count; i++)
        {
            if (i == index)
                ListParents[i].SetActive(true);
            else
                ListParents[i].SetActive(false);
        }
    }

    public void SetRunestoneSocketTypes(ElysianStone stone)
    {
        CurrentElysianStone = null;
        CurrentElysianStone = stone;
        Debug.Log("Current static elysian stone: " + CurrentElysianStone.GetName());
        ElysianStone.ElysianChildSlots[] stoneSlots = stone.GetChildSlots();
        int count = stoneSlots.Length;
        for(int i = 0; i < StoneSockets.Count; i++)
        {
            if(i < count)
            {
                StoneSockets[i].gameObject.SetActive(true);
                StoneSockets[i].AugmentType = stoneSlots[i].AugmentSlot;

                if (stoneSlots[i].SlottedStone == null)
                {
                    StoneSockets[i].GetComponent<Image>().sprite = null;
                    if (StoneSockets[i].AugmentType == BaseRunestone.RunestoneAugmentType.ARCANE)
                    {
                        StoneSockets[i].GetComponent<Image>().color = Color.magenta;
                    }
                    else
                    {
                        StoneSockets[i].GetComponent<Image>().color = Color.blue;
                    } 
                }
                else
                {
                    StoneSockets[i].GetComponent<Image>().color = Color.white;
                    StoneSockets[i].SocketedStone = stoneSlots[i].SlottedStone;
                    StoneSockets[i].GetComponent<Image>().sprite = StoneSockets[i].SocketedStone.UISprite;
                }
            }
            else
            {
                StoneSockets[i].gameObject.SetActive(false);
            }
        }
    }
}
