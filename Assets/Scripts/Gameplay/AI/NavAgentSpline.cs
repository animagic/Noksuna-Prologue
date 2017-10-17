using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgentSpline : MonoBehaviour {

    public static NavAgentSpline StaticNavAgentSpline;

    [SerializeField]
    List<Transform> SplineNavPoints = new List<Transform>();

    private void Awake()
    {
        StaticNavAgentSpline = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnDrawGizmos()
    {
        if (SplineNavPoints.Count > 0)
        {
            for (int i = 0; i < SplineNavPoints.Count; i++)
            {
                var nextIndex = i + 1;
                if (nextIndex == SplineNavPoints.Count)
                    nextIndex = i;
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(SplineNavPoints[i].position, SplineNavPoints[nextIndex].position);
            }
        }
    }
   
    public List<Transform> GetNavPoints()
    {
        return SplineNavPoints;
    }
}
