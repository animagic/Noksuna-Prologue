using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavigation : MonoBehaviour {

    [SerializeField]
    Transform target;
    Vector3 Position;

    NavMeshAgent agent;
    [SerializeField]
    bool StopMovement = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (StopMovement)
        {
            if (target)
            {
                agent.SetDestination(target.position);
            }
            else
                agent.SetDestination(Position);
            agent.isStopped = true; 
        }

    }

    public void SetTargetPosition(Vector3 _pos)
    {
        Position = _pos;
    }
}
