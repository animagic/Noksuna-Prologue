using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : BaseTower {

    [SerializeField]
    GameObject FiringPoint;
    [SerializeField]
    ArrowProjectile Arrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandleRefire();
        if (AllTargets.Count > 0)
        {
            Target = AllTargets[0];
            if (CanFire)
                FireProjectile();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.WithComponent<Enemy>(x => AllTargets.Add(x.gameObject));
    }
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.WithComponent<Enemy>(x => AllTargets.Remove(other.gameObject));
    }

    public override void FireProjectile()
    {
        CanFire = false;
        ArrowProjectile _arrow = Instantiate(Arrow, FiringPoint.transform.position, Quaternion.identity);
        _arrow.Init(null, gameObject);
        _arrow.Target = AllTargets[0];
    }




}
