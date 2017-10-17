using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickToMove : ExtendedMonoBehaviour {

    NavMeshAgent MyNavAgent;
    GameObject PointSphere;

    List<string> LayersToRaycast = new List<string>()
    {
        "Walkable",
        "Water",
        "Terrain"
    };
    List<string> LayersToIgnore = new List<string>();

    private void Awake()
    {
        WithComponent<NavMeshAgent>(x => MyNavAgent = x);
        GetAllLayers();

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveToClick();
    }

    void MoveToClick()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject(-1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayInfo;

                if (Physics.Raycast(ray, out rayInfo, 5000, LayerMask.GetMask(LayersToRaycast.ToArray())))
                {
                    Debug.Log("mouse click raycast hit: " + rayInfo.transform.name);
                    MoveToClosestNavPoint(rayInfo);
                } 
            }
        }
#else
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit rayInfo;
                if (Physics.Raycast(ray, out rayInfo))
                {
                    MoveToClosestNavPoint(rayInfo);
                }
            }
        } 
#endif
    }

    void TurnOffPointSphere()
    {
        PointSphere.SetActive(false);
    }

    void MoveToClosestNavPoint(RaycastHit hitInfo)
    {
        RayCastPointEffect(hitInfo);
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(hitInfo.point, out navHit, 25.0f, NavMesh.AllAreas))
        {
            MyNavAgent.destination = navHit.position;
            GetComponent<CharacterNavigation>().SetTargetPosition(navHit.position);
        }
            
    }

    void RayCastPointEffect(RaycastHit hitInfo)
    {
        CancelInvoke(TurnOffPointSphere);
        //Debug.Log(hitInfo.point);
        if (PointSphere == null)
        {
            PointSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            PointSphere = Instantiate(PointSphere, hitInfo.point, Quaternion.identity);
            PointSphere.transform.localScale = new Vector3(.25f, .25f, .25f);
            PointSphere.GetComponent<MeshRenderer>().material.color = Color.magenta;
            PointSphere.GetComponent<Collider>().isTrigger = true;
            Invoke(TurnOffPointSphere, 5.0f);
        }
        else
        {
            PointSphere.transform.position = hitInfo.point;
            PointSphere.SetActive(true);
            Invoke(TurnOffPointSphere, 5.0f);
        }
    }

    void GetAllLayers()
    {
        for(int i = 0; i <= 31; i++)
            if(LayerMask.LayerToName(i).Length > 0 && !LayersToRaycast.Any(x => x == LayerMask.LayerToName(i)))
            {
                //Debug.Log(i + " - " + LayerMask.LayerToName(i));
                LayersToIgnore.Add(LayerMask.LayerToName(i));
            }
    }


}
