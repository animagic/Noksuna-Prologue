using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {

    [SerializeField]
    Slider PlayerHealth;
    [SerializeField]
    Slider PlayerExperience;
    [SerializeField]
    Text PlayerLevelText;

	// Use this for initialization
	void Start () {
        UpdatePlayerUIValues();
        RegisterStartupEvents();
        PlayerHealth.maxValue = Player.StaticPlayer.GetPrimaryStats().BaseHealth;
        
    }
	
	// Update is called once per frame
	void Update () {
        
        
    }

    private void OnEnable()
    {
        RegisterStartupEvents();
    }

    public void UpdatePlayerUIValues()
    {
        PlayerExperience.maxValue = Player.StaticPlayer.GetExperienceStats().CharacterExperienceTNL;
        PlayerExperience.value = Player.StaticPlayer.GetExperienceStats().CharacterExperience;
        PlayerLevelText.text = Player.StaticPlayer.GetExperienceStats().CharacterLevel.ToString();
    }

    void RegisterStartupEvents()
    {
        if (EventManager.StaticEventManager)
        {
            EventManager.StartListening(EventManager.EventNames.UpdatePlayerUIValues, UpdatePlayerUIValues);
        }
    }

    void StopStartupEvents()
    {
        if (EventManager.StaticEventManager)
        {
            EventManager.StopListening(EventManager.EventNames.UpdatePlayerUIValues, UpdatePlayerUIValues);
        }
    }


}
