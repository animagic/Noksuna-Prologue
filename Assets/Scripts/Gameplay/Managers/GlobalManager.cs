using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour {

    public static GlobalManager StaticGlobalManager;

    private void Awake()
    {
        if(StaticGlobalManager == null)
        {
            StaticGlobalManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(StaticGlobalManager != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
