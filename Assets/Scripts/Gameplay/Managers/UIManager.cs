using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : ExtendedMonoBehaviour {

    public static UIManager StaticUIManager;

    [Header("Major Panel Groupings")]
    [SerializeField]
    PlayerPanel CombatPlayerPanel;
    [SerializeField]
    SkillTree SkillTreePanel;
    [SerializeField]
    InventoryUI InventoryPanel;

    List<GameObject> ToggleablePanels = new List<GameObject>();

    [Header("Misc.")]
    [SerializeField]
    Text LevelText;
    [SerializeField]
    Text TNLText;
    [SerializeField]
    Text CurExpText;
    [SerializeField]
    GameObject AbilityPanel;
    [SerializeField]
    GameObject BuffPanel;
    [SerializeField]
    Text StatusEffectsTimers;

    [SerializeField]
    Button PylonAbilityButton;

    ActorAbilityManager PlayerActiveAbilities;

    [SerializeField]
    List<BaseStatusEffect> TestEffect;
    List<IconCooldown> BuffImages = new List<IconCooldown>();
    List<IconCooldown> AbilityImages = new List<IconCooldown>();

    private void Awake()
    {
        StaticUIManager = this;
        
    }
    // Use this for initialization
    void Start () {
        
        SetActiveAbilityImages();
        BuffImages = BuffPanel.GetComponentsInChildren<IconCooldown>().ToList();

        ToggleablePanels.Add(InventoryPanel.gameObject);
        for (int i = 0; i < ToggleablePanels.Count; i++)
            ToggleablePanels[i].SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //LevelText.text = Player.StaticPlayer.GetCurrentLevel().ToString();
        //TNLText.text = "TNL: " + Player.StaticPlayer.GetCurrentTNL().ToString();
        //CurExpText.text = "Current EXP: " + Player.StaticPlayer.GetCurrentExperience().ToString();

        UpdateCurrentEffectsOnPlayer();
	}


    void SetActiveAbilityImages()
    {

        AbilityImages = AbilityPanel.GetComponentsInChildren<IconCooldown>().ToList();
        Player.StaticPlayer.WithComponent<ActorAbilityManager>(x => PlayerActiveAbilities = x);
        List<BaseAbility> abs = PlayerActiveAbilities.GetActiveAbilityList();

        for(int i = 0; i < AbilityImages.Count; i++)
        {
            if (i < abs.Count)
            {
                AbilityImages[i].SetAbility(abs[i]);
            }
            else
                AbilityImages[i].ClearAbility();
        }
    }

    void UpdateCurrentEffectsOnPlayer()
    {
        //List<BaseStatusEffect> Effects = Player.StaticPlayer.GetCurrentBuffEffects();
        List<BaseStatusEffect> Effects = new List<BaseStatusEffect>();
        Dictionary<BaseStatusEffect, float> AllEffects = Player.StaticPlayer.GetStatusEffects();
        foreach(var kvp in AllEffects)
        {
            Effects.Add(kvp.Key);
        }
        for (int i = 0; i < BuffImages.Count; i++)
        {
            if (i < Effects.Count)
            {
                BuffImages[i].SetEffect(Effects[i]);
                BuffImages[i].SetFillValue(AllEffects[Effects[i]] / Effects[i].InitialDuration);
            }
            else
                BuffImages[i].ClearEffect();
        }

        if (AllEffects.Count > 0)
        {
            StatusEffectsTimers.text = "";
            foreach (var kvp in AllEffects)
            {
                StatusEffectsTimers.text += kvp.Key.Name + " - " + (int)kvp.Value + "cur/init: " + (int)(kvp.Key.CurrentDuration / kvp.Key.InitialDuration) + "\n";
            }
        }
        else
            StatusEffectsTimers.text = "No Status Effects";
    }

    public void UpdatePylonAbility(BaseAbility _ability)
    {
        PylonAbilityButton.image.sprite = _ability.Icon;
    }

    void ClearPylonAbility()
    {
        PylonAbilityButton.image.sprite = null;
    }

    public void AbilityButtonPress(int _index)
    {
        Player.StaticPlayer.UseAbility(_index);
        Debug.Log(PlayerActiveAbilities.GetActiveAbilityList()[_index].Icon + " button was pressed.");
        AbilityImages[_index].RunAbilityCooldown(PlayerActiveAbilities.GetActiveAbilityList()[_index].Cooldown);
    }

    public void PylonAbilityPress()
    {
        //Player.StaticPlayer.UsePylonAbility();
        EventManager.TriggerEvent(EventManager.EventNames.UsePylonAbility);
        Debug.Log("Player Pylon Ability button was pressed.");
        ClearPylonAbility();
    }

    /// <summary>
    /// Debug method only
    /// </summary>
    public void AddEffect()
    {
        foreach(BaseStatusEffect effect in  TestEffect)
        {
            Player.StaticPlayer.AddStatusEffect(effect, null);
        }
    }

    public void OpenInventory()
    {
        InventoryPanel.gameObject.SetActive(true);
    }

    public void OpenCharacterPanel()
    {
        OpenInventory();
    }
}
