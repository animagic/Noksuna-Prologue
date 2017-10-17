using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : BaseProjectile {

    // Use this for initialization
    void Start () {
        WithComponent<Rigidbody>(x => rb = x);
    }
	
	// Update is called once per frame
	void Update () {
        MoveProjectile();
	}
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.WithComponent<Enemy>(x =>
        {
            //x.TakeDamage(Damage, FiringEntity);
            x.TakeDamage(Damage);
            Destroy(gameObject);
        });
    }

    public override void MoveProjectile()
    {
        Vector3 heading = Target.transform.position - transform.position;
        Vector3 direction = heading / heading.magnitude;
        rb.AddForce(direction * MovementSpeed, ForceMode.VelocityChange);
        transform.LookAt(Target.transform.position);
    }
}
