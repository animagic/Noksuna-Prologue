using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


public class Enemy : BaseCharacter {

    //[SerializeField]
    GameObject AttackingTower;
    [SerializeField]
    int Health = 50;
    [SerializeField]
    int MaxHealth = 50;
    [SerializeField]
    float MaxMovementSpeed = 2.0f;

    NavMeshAgent MyAgent;
    List<Transform> NavPoints = new List<Transform>();
    Vector3 curDest = new Vector3();
    int curNavPointIndex = 0;
    Vector3 startPos = new Vector3();


	[Serializable]
	public class EnemyAnimationClass
	{
		[HideInInspector]
		public Animator EnemyAnimator;
		public string DeathTriggerString = "Dead";
		public string BiteTriggerString = "Bite";
		public string SlashTriggerString = "Slash";
		public string DamageTriggerString = "Damage";
        public bool isRunning;
        public string EnemyRunningString = "isRunning";

    }
	public EnemyAnimationClass EnemyAnimation;

	// Use this for initialization
	void Start () {
        EnemyAnimation.EnemyAnimator = GetComponent<Animator>();
        EnemyAnimation.isRunning = true;
        MyAgent = GetComponent<NavMeshAgent>();
        MyAgent.speed = MaxMovementSpeed;
        //NavPoints = NavAgentSpline.StaticNavAgentSpline.GetNavPoints();
        startPos = transform.position;
        Health = MaxHealth;
        //Invoke("CycleThroughNavPoints", 1.0f);
        SetBaseStatValues(ExperienceStats.CharacterLevel);
        UpdateAllStatValues();


	}
	
	// Update is called once per frame
	void Update () {
        if (NavPoints.Count > 0)
        {
            if (Vector3.Distance(transform.position, curDest) < 1.0f && curNavPointIndex < NavPoints.Count)
                CycleThroughNavPoints();

            // only here for testing
            if (Vector3.Distance(transform.position, NavPoints[NavPoints.Count - 1].position) < 1.0f)
                ResetToStart(); 
        }

        if (StatusEffectDurationDictionary.Count > 0)
        {
            CountDownStatusEffects();
        }
    }

    void SetPosition(Vector3 _pos)
    {
        Vector3 newPos = new Vector3(_pos.x, transform.position.y, _pos.z);
        curDest = newPos;
        MyAgent.destination = newPos;
    }

    void CycleThroughNavPoints()
    {
        SetPosition(NavPoints[curNavPointIndex].position);
        curNavPointIndex++;

        //testing some stuff
        //if (EnemyAnimation.isRunning == true)
        //{
        //EnemyAnimation.EnemyAnimator.SetBool(EnemyAnimation.EnemyRunningString, EnemyAnimation.isRunning);
        // }

    }

    public void ResetToStart()
    {
        // Have to turn the NavAgent off before doing a direct movement or it will try and use the NavMesh to move
        if (Health <= 0)
        {
            Health = MaxHealth;
        }
        MyAgent.enabled = false;
        transform.position = startPos;
        MyAgent.enabled = true;
        
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        curNavPointIndex = 0;
        CycleThroughNavPoints();
    }

    //public void TakeDamage(int _damage)
    //{
    //    ReduceHealth(_damage);

    //}

    //public void TakeDamage(int _damage, GameObject _attacker)
    //{
    //    AttackingTower = _attacker;
    //    ReduceHealth(_damage);
    //    EnemyAnimation.EnemyAnimator.SetTrigger(EnemyAnimation.DamageTriggerString);
    //}

    public void Die()
    {
        gameObject.SetActive(false);
        if (AttackingTower)
            AttackingTower.WithComponent<BaseTower>(x => x.AllTargets.Remove(gameObject));
        
        
        Invoke(ResetToStart, 1.0f);
    }

    //void ReduceHealth(int _damage)
    //{
    //    Debug.Log("Reduce health called");
    //    Health -= _damage;
    //    if (Health <= 0)
    //    {
    //        Health = 0;
    //        Invoke(Die, 10.0f);
    //        EnemyAnimation.EnemyAnimator.SetTrigger(EnemyAnimation.DeathTriggerString);
    //    }
            

    //}
}
