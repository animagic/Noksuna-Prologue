using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pylon : MonoBehaviour {

    public enum PylonEnumType
    {
        OFFENSIVE_BUFF,
        DEFENSIVE_BUFF,
        EXPERIENCE_GAIN,
        ABILITY_GIVE
    }

    [Tooltip("Enum used to determine the required SOs that this Pylon should have access to.")]
    public PylonEnumType PylonType;

    [Tooltip("The ability this Pylon will give to the player.")]
    [SerializeField]
    BaseAbility PrimaryAbility;

    [Tooltip("Chance the Primary ability will be used instead of one of the Secondary abilities.")]
    [SerializeField]
    float PrimaryChance = .75f;

    [Tooltip("Additional abilities this Pylon has a chance to give the player instead of the Primary ability.")]
    [SerializeField]
    List<BaseAbility> SecondaryAbilities = new List<BaseAbility>();

    [SerializeField]
    BaseAbility _activeAbility;

    bool _playerInRange = false;

    // Use this for initialization
	void Start () {
        AssignActiveAbility();
        Debug_ColorPylon(Color.green);
	}
	
	// Update is called once per frame
	void Update () {
        CheckClicked();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.WithComponent<Player>(x => _playerInRange = true);
    }

    void AssignActiveAbility()
    {
        if (SecondaryAbilities.Count > 0)
        {
            float rnd = Random.Range(0, 1);
            if (rnd <= PrimaryChance)
                _activeAbility = PrimaryAbility;
            else
            {
                int rnd2 = Random.Range(0, SecondaryAbilities.Count - 1);
                _activeAbility = SecondaryAbilities[rnd2];
            }
        }
        else
            _activeAbility = PrimaryAbility;
    }

    void GivePlayerAbility()
    {
        if (_playerInRange && _activeAbility != null)
        {
            Player.StaticPlayer.GetComponent<ActorAbilityManager>().SetPylonAbility(_activeAbility);
            UIManager.StaticUIManager.UpdatePylonAbility(_activeAbility);
            _activeAbility = null;
        }
    }

    void GivePlayerOffensiveBuff()
    {

    }

    void GivePlayerDefensiveBuff()
    {

    }

    void GivePlayerExperienceBuff()
    {

    }

    void Debug_ColorPylon(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

    void CheckClicked()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayInfo;
            if (Physics.Raycast(ray, out rayInfo))
            {
                if (rayInfo.transform == this.transform)
                {
                    DoClickAction();
                }
            }
        }
#else
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit rayInfo;
                if (Physics.Raycast(ray, out rayInfo))
                {
                    if (rayInfo.transform == this.transform)
                    {
                        GivePlayerAbility();
                    }
                }
            }
        } 
#endif
    }

    void DoClickAction()
    {
        switch(PylonType)
        {
            case PylonEnumType.ABILITY_GIVE:
                GivePlayerAbility();
                break;
            case PylonEnumType.DEFENSIVE_BUFF:
                GivePlayerDefensiveBuff();
                break;
            case PylonEnumType.EXPERIENCE_GAIN:
                GivePlayerExperienceBuff();
                break;
            case PylonEnumType.OFFENSIVE_BUFF:
                GivePlayerOffensiveBuff();
                break;

        }
    }

}
