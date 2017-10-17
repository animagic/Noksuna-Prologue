using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : ExtendedMonoBehaviour {

    public GameObject Target;
    public List<GameObject> AllTargets;

    [SerializeField]
    float FireRate = 2.0f;
    [SerializeField]
    float CooldownTimer = 2.0f;
    public bool CanFire = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public virtual void FireProjectile()
    {

    }
    public void HandleRefire()
    {
        if (!CanFire)
        {
            CooldownTimer -= Time.deltaTime;
            if (CooldownTimer <= 0.0f)
            {
                CooldownTimer = FireRate;
                CanFire = true;
            } 
        }
    }
}
