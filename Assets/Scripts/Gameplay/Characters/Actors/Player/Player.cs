using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    public static Player StaticPlayer;
    //BaseCharacter()
    //{
    //    AttackValue = 10;
    //    MagicAttackValue = 5;
    //    RangedAttackValue = 15;

    //    DefenseValue = 25;
    //    MagicDefenseValue = 25;
    //}
    //[Serializable]
    //public class PlayerActionClass
    //{
    //    public ActorAbilityManager MyAbilityManager;
    //    public GameObject CurrentTarget;
    //}
    //public PlayerActionClass PlayerAction;

    [Serializable]
    public class PlayerExperienceValuesClass
    {
        [SerializeField]
        public float ExperienceBaseValue = 1000;
        [SerializeField]
        public float ExperienceAdditive = 750;
        [SerializeField]
        public float ExperienceTotalMultiplier = 1.375f;
    }
    public PlayerExperienceValuesClass PlayerExperienceValues;

    

    private void Awake()
    {
        StaticPlayer = this;
        WithComponent<ActorAbilityManager>(x => CharacterActionReferences.MyAbilityManager = x);
    }

    // Use this for initialization
    void Start () {
        RegisterStartupEvents();

        SetCurrentLevelMaxExperience(ExperienceStats.CharacterLevel);
        SetBaseStatValues(ExperienceStats.CharacterLevel);
	}
	
	// Update is called once per frame
	void Update () {
        if (StatusEffectDurationDictionary.Count > 0)
        {
            CountDownStatusEffects();
        }

        // THIS SHOULD PROBABLY BE MOVED TO BE RECALC ONLY WHEN A BUFF IS ADDED AND REMOVED FROM THE CHARACTER
        //UpdateAllStatValues();
    }

    #region STATUS CONTROL METHODS
    protected override void LevelUp()
    {
        ExperienceStats.CharacterLevel += 1;
        SetCurrentLevelMaxExperience(ExperienceStats.CharacterLevel);
        SetBaseStatValues(ExperienceStats.CharacterLevel);
    }

    public override int SetCurrentLevelMaxExperience(int _playerLevel)
    {
        ExperienceStats.CharacterExperienceTNL = (int)Mathf.Round(((_playerLevel * PlayerExperienceValues.ExperienceBaseValue) + PlayerExperienceValues.ExperienceAdditive) * PlayerExperienceValues.ExperienceTotalMultiplier);
        Debug.Log("Level: " + _playerLevel + ", TNL: " + ExperienceStats.CharacterExperienceTNL);
        return ExperienceStats.CharacterExperienceTNL;
    }

    #endregion

    #region ABILITY CONTROL METHODS
    // Button click method, passing the index of the button
    public void UseAbility(int _index)
    {
        CharacterActionReferences.MyAbilityManager.UseActiveAbility(_index);
        //if(PlayerAction.CurrentTarget != null)
        //    PlayerAction.MyAbilityManager.GetActiveAbilityList()[_index].CastAbility(this.gameObject, PlayerAction.CurrentTarget);
        //else
        //    PlayerAction.MyAbilityManager.GetActiveAbilityList()[_index].CastAbility(this.gameObject, transform.forward);
    }

    public void UsePylonAbility()
    {
        Debug.Log("UsePylonAbility on Player script called");
        CharacterActionReferences.MyAbilityManager.UsePylonAbility();
    }
    #endregion
    #region EVENT CONTROLLER
    void RegisterStartupEvents()
    {
        if(EventManager.StaticEventManager)
        {
            EventManager.StartListening(EventManager.EventNames.UsePylonAbility, UsePylonAbility);
        }
    }

    void StopStartupEvents()
    {
        if (EventManager.StaticEventManager)
        {
            EventManager.StartListening(EventManager.EventNames.UsePylonAbility, UsePylonAbility);
        }
    }


    #endregion





}
