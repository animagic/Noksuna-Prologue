using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    [SerializeField]
    RunestoneUI RunestoneUIPanel;
    [SerializeField]
    GameObject WeaponUIPanel;
    [SerializeField]
    GameObject ArmorUIPanel;
    [SerializeField]
    GameObject ItemUIPanel;
    

    List<GameObject> PanelList = new List<GameObject>();
    

	// Use this for initialization
	void Start () {
        PanelList.Add(RunestoneUIPanel.gameObject);
        PanelList.Add(WeaponUIPanel);
        PanelList.Add(ArmorUIPanel);
        PanelList.Add(ItemUIPanel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseInventory()
    {
        gameObject.SetActive(false);
    }

    void TurnOffMainPanels()
    {
        int cnt = PanelList.Count;
        for (int i = 0; i < cnt; i++)
            PanelList[i].SetActive(false);
    }

    public void ActivateRunestoneUI()
    {
        TurnOffMainPanels();
        RunestoneUIPanel.gameObject.SetActive(true);
    }

    public void ActivateWeaponUI()
    {
        TurnOffMainPanels();
        WeaponUIPanel.SetActive(true);
    }

    public void ActivateArmorUI()
    {
        TurnOffMainPanels();
        ArmorUIPanel.SetActive(true);
    }

    public void ActivateItemUI()
    {
        TurnOffMainPanels();
        ItemUIPanel.SetActive(true);
    }
}
