using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActorAbilityManager : MonoBehaviour {

    BaseAbility PylonAbility;

    [Tooltip("The max number of abilities this Actor can have Active.  Any abilities beyond this number will not be used.")]
    [SerializeField]
    int MaxActiveAbilityCount = 6;
    [Tooltip("Ability List for the Actor's currently usable abilities.")]
    [SerializeField]
    List<BaseAbility> ActiveAbilities = new List<BaseAbility>();

    [Tooltip("Ability list for all of the Actor's available/learned abilities")]
    [SerializeField]
    List<BaseAbility> LearnedAbilities = new List<BaseAbility>();

    BaseCharacter _character;
    float CastTimer;
    bool timerComplete = false;

	// Use this for initialization
	void Start () {
        _character = GetComponent<BaseCharacter>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<BaseAbility> GetActiveAbilityList()
    {
        return ActiveAbilities;
    }

    public List<BaseAbility> GetLearnedAbilityList()
    {
        return LearnedAbilities;
    }

    public void LearnNewAbility(BaseAbility _ability)
    {
        LearnedAbilities.Add(_ability);
    }

    public void SetAbilityActive(BaseAbility _ability)
    {
        if (ActiveAbilities.Count < MaxActiveAbilityCount)
            ActiveAbilities.Add(_ability);
        else
            Debug.Log("Already at Max Active ability count");
    }

    public void ReplaceActiveAbility(BaseAbility _ability, int _index)
    {
        if (ActiveAbilities.Count > _index)
            ActiveAbilities[_index] = _ability;
        else
            SetAbilityActive(_ability);
    }

    public void SetPylonAbility(BaseAbility _ability)
    {
        PylonAbility = _ability;
    }

    public void UsePylonAbility()
    {
        UseAbility(PylonAbility);
        PylonAbility = null;
    }

    public void UseActiveAbility(int _index)
    {
        if(ActiveAbilities.Count > _index)
        {
            UseAbility(ActiveAbilities[_index]);
        }
        else
        {
            Debug.Log("Index: " + _index + " does not exist in Ability List");
        }
    }

    void UseAbility(BaseAbility _ability)
    {
        Debug.Log("Casting " + _ability);
        switch(_ability.AbilityTargetMethod)
        {
            case BaseAbility.AbilityTargetMethodEnum.GROUND_POSITIONED:
                //Do some fanciness to wait for a Mouse input or tap... or something
                DoGroundPositionAbility(_ability);
                break;
            case BaseAbility.AbilityTargetMethodEnum.DIRECTIONAL:
                DoDirectionalAbility(_ability);
                break;
            case BaseAbility.AbilityTargetMethodEnum.TARGET_POSITIONED:
                DoTargetPositionAbility(_ability);
                break;
        }
    }

    void DoDirectionalAbility(BaseAbility _ability)
    {
        //FaceTarget();
        switch (_ability.AbilityCastTimeType)
        {
            case BaseAbility.AbilityCastTimeTypeEnum.INSTANT:
                _ability.CastAbilityAtDirection(gameObject, transform.forward);
                break;
            case BaseAbility.AbilityCastTimeTypeEnum.CHANNELED:
                _ability.CastAbilityAtDirection(gameObject, transform.forward);
                RunCastTime(_ability);
                break;
            case BaseAbility.AbilityCastTimeTypeEnum.TIMED:
                RunCastTime(_ability);
                StartCoroutine(WaitForCastTime(_ability));
                break;
        }
    }

    void DoGroundPositionAbility(BaseAbility _ability)
    {
        Debug.Log("Ground targeted code not implemented yet");
    }

    void DoTargetPositionAbility(BaseAbility _ability)
    {
        FaceTarget();
        switch (_ability.AbilityCastTimeType)
        {
            case BaseAbility.AbilityCastTimeTypeEnum.INSTANT:
                _ability.CastAbilityOnTarget(gameObject, _character.CharacterActionReferences.CurrentTarget);
                break;
            case BaseAbility.AbilityCastTimeTypeEnum.CHANNELED:
                _ability.CastAbilityOnTarget(gameObject, _character.CharacterActionReferences.CurrentTarget);
                RunCastTime(_ability);
                break;
            case BaseAbility.AbilityCastTimeTypeEnum.TIMED:
                RunCastTime(_ability);
                StartCoroutine(WaitForCastTime(_ability));
                break;
        }
    }

    void CheckTargetAndCastAbility(BaseAbility _ability)
    {
        if (_character.CharacterActionReferences.CurrentTarget == null)
            _ability.CastAbilityAtDirection(gameObject, transform.forward);
        else
        {
            _ability.CastAbilityOnTarget(gameObject, _character.CharacterActionReferences.CurrentTarget);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = new Vector3();
        
        if (_character.CharacterActionReferences.CurrentTarget)
        {
            Vector3 heading = _character.CharacterActionReferences.CurrentTarget.transform.position - transform.position;
            direction = heading / heading.magnitude;
        }
        else
            direction = transform.forward;
        direction = new Vector3(0, direction.y, 0);
        transform.rotation = Quaternion.Euler(direction);
    }

    void RunCastTime(BaseAbility _ability)
    {
        //add something here for a channeling bar?
        
        CastTimer = 0;
        while(CastTimer <= _ability.CastTime)
        {
            CastTimer += Time.deltaTime / _ability.CastTime;
        }
    }

    IEnumerator WaitForCastTime(BaseAbility _ability)
    {
        yield return new WaitForSeconds(_ability.CastTime);
        switch(_ability.AbilityTargetMethod)
        {
            case BaseAbility.AbilityTargetMethodEnum.SELF:
                _ability.CastAbilityOnSelf(gameObject);
                break;
            case BaseAbility.AbilityTargetMethodEnum.TARGET_POSITIONED:
                _ability.CastAbilityOnTarget(gameObject, _character.CharacterActionReferences.CurrentTarget);
                break;
            case BaseAbility.AbilityTargetMethodEnum.DIRECTIONAL:
                _ability.CastAbilityAtDirection(gameObject, transform.forward);
                break;
            case BaseAbility.AbilityTargetMethodEnum.GROUND_POSITIONED:
                //SOME FANCINESS
                break;
        }
    }


}
