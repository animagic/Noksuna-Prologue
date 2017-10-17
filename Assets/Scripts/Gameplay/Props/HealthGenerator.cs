using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;


//Script needs to be adjusted for IOS!!!

public class HealthGenerator : MonoBehaviour {



    [Serializable]
    public class HealthGen_Anim
    {
        
        public Animator HealthGeneratorAnimator;
        public string ActivateTriggerString = "ActivateTrigger";
        public string ReadyTriggerString = "ReadyTrigger";
        }
    public HealthGen_Anim HG_anim;



    bool isReady;





	// Use this for initialization




	void Start () {
        isReady = true;
			}


    void Update()
    {

        if (Input.GetMouseButtonDown(0))

        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "HealthGenerator" && isReady == true)
                {
                    ActivateHealthGenerator();
                }
            }
        }
    }


    void ActivateHealthGenerator ()
    {
        HG_anim.HealthGeneratorAnimator.SetTrigger(HG_anim.ActivateTriggerString);
        isReady = false;
        StartCoroutine(CooldownStart());
    }

    IEnumerator CooldownStart()
    {
        yield return new WaitForSeconds(30);
        HealthGeneratorReady();

    }


    void HealthGeneratorReady()
    {
        HG_anim.HealthGeneratorAnimator.SetTrigger(HG_anim.ReadyTriggerString);
        isReady = true;
    }

}







 
