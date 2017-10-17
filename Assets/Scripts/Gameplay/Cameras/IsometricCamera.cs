using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCamera : MonoBehaviour {

    public Vector3 RelativePositionToPlayer = new Vector3(40, 57.5f, 0);
    public Vector3 CameraTransitionSpeed = new Vector3(2,2,2);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LateUpdate()
    {
        FollowPlayer();  
    }

    void FollowPlayer()
    {
        Vector3 newPos = RelativePositionToPlayer + Player.StaticPlayer.transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref CameraTransitionSpeed, .1f);
    }
 }
