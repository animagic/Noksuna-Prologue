using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleProjectile : BaseProjectile {

   

	// Use this for initialization
	void Start () {
        Collider col = GetComponent<Collider>();
        Physics.IgnoreCollision(col, FiringEntity.GetComponent<Collider>());
        GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
		
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
            Destroy(gameObject);
        });
        other.gameObject.WithComponent<Terrain>(x => Destroy(gameObject));
        Debug.Log(other.name);
    }

    
}
