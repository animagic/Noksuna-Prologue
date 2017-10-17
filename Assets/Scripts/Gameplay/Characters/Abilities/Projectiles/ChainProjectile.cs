using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChainProjectile : BaseProjectile {

    // Use this for initialization
    void Start () {
        IgnoreMyCasterCollider();
        MoveMe();
        FindMyImmediateChildProjectiles();
    }
	
	// Update is called once per frame
	void Update () {
        //CheckChildrenToDestroyParent();
	}

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.WithComponent<BaseCharacter>(x =>
        {
            x.TakeDamage(Damage);
            foreach (BaseStatusEffect effect in AbilityThatCastMe.StatusEffectsForTarget)
            {
                x.AddStatusEffect(effect, AbilityThatCastMe);
            }
            //InitChildren();
            
            //CheckSpreadTypeAndActivate();
        });
        other.gameObject.WithComponent<Terrain>(x => Destroy(gameObject));
        Debug.Log(other.name);
    }

    void IgnoreMyCasterCollider()
    {
        Collider col = GetComponent<Collider>();
        Physics.IgnoreCollision(col, FiringEntity.GetComponent<Collider>());
        
    }

    void MoveMe()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Impulse);
    }

    void FindMyImmediateChildProjectiles()
    {
        List<BaseProjectile> MyChildren = new List<BaseProjectile>();
        foreach (BaseProjectile p in GetComponentsInChildren<BaseProjectile>(true).ToList())
        {
            if (p.transform.parent.name == gameObject.name)
                MyChildren.Add(p);
        }
        //ChildProjectiles = MyChildren;
    }

    
}
