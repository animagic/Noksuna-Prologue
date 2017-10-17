using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour {

    [SerializeField]
    Camera MinimapCamera;
    [SerializeField]
    Button ZoomOutButton;
    [SerializeField]
    Button ZoomInButton;

    [SerializeField]
    float MinimumZoomValue;

    [SerializeField]
    float MaximumZoomValue;

    [SerializeField]
    float ZoomStep;
	// Use this for initialization
	void Start () {
        MinimapCamera.orthographicSize = MinimumZoomValue + ZoomStep;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ZoomIn()
    {
        if (MinimapCamera.orthographicSize <= MinimumZoomValue)
            return;

        MinimapCamera.orthographicSize -= ZoomStep;
        ZoomOutButton.interactable = true;

        if (MinimapCamera.orthographicSize <= MinimumZoomValue)
        {
            ZoomInButton.interactable = false;
        }
    }

    public void ZoomOut()
    {
        if (MinimapCamera.orthographicSize >= MaximumZoomValue)
            return;

        MinimapCamera.orthographicSize += ZoomStep;
        ZoomInButton.interactable = true;

        if (MinimapCamera.orthographicSize >= MaximumZoomValue)
        {
            ZoomOutButton.interactable = false;
        }
        
    }
}
