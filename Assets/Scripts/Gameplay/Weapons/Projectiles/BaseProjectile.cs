using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : ExtendedMonoBehaviour {

    public enum SpreadTypeEnum
    {
        SINGLE,
        CONE,
        SQUARE,
        THREE_SIXTY,

        __MAX_DONT_USE
    }

    protected GameObject FiringEntity;
    protected BaseAbility AbilityThatCastMe;
    [HideInInspector]
    public GameObject Target;
    [HideInInspector]
    public Rigidbody rb;

    [HideInInspector]
    public int Damage = 5;
    public float MaxVelocity = 1.0f;
    public float MovementSpeed = 1.0f;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void MoveProjectile()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    /// <summary>
    /// Use this in the casting code to pass the caster, ability and set the damage of the projectile
    /// </summary>
    /// <param name="_ability"></param>
    /// <param name="caster"></param>
    public virtual void Init(BaseAbility _ability, GameObject caster)
    {
        AbilityThatCastMe = _ability;
        FiringEntity = caster;
        Damage = _ability.AbilityPower;
    }

}
