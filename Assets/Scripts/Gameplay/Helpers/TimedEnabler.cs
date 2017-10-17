using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEnabler : MonoBehaviour {

    //[SerializeField]
    //GameObject ObjectToTurnOn;
    //[SerializeField]
    //float TimeToTurnObjectOn;

    [SerializeField]
    List<EnablerData> ObjectsToAlterState = new List<EnablerData>();

	// Use this for initialization
	void Start () {
        //Invoke("TurnMeOn", TimeToTurnObjectOn);
        foreach(EnablerData e in ObjectsToAlterState)
        {
            StartCoroutine(AlterState(e));
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator AlterState(EnablerData objectData)
    {
        Debug.Log(objectData.ObjectToAlterState + " Coroutine started");
        yield return new WaitForSeconds(objectData.TimeToAlter);
        if (objectData.IsTurnOn)
            objectData.ObjectToAlterState.SetActive(true);
        else
            objectData.ObjectToAlterState.SetActive(false);
    }

    //void TurnMeOn()
    //{
    //    ObjectToTurnOn.SetActive(true);
    //}

    [Serializable]
    public class EnablerData
    {
        public GameObject ObjectToAlterState;
        [Tooltip("Time in seconds before the Coroutine will fire")]
        public float TimeToAlter;
        public bool IsTurnOn = true;
    }
}
