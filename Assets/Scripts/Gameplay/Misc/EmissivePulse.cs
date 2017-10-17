using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissivePulse : MonoBehaviour {

    [SerializeField]
    float emissive_floor;
    [SerializeField]
    float emissive_ceiling;

    Renderer rend;
    Material mat;
    Color baseEmissionColor;

    // Use this for initialization
    void Start () {

        rend = GetComponent<Renderer>();
        rend.material = Instantiate(rend.material);
        mat = rend.material;
        baseEmissionColor = mat.GetColor("_EmissionColor");

    }
	
	// Update is called once per frame
	void Update () {
        float emission = Mathf.PingPong(Time.time, emissive_ceiling - emissive_floor);
        Color c = baseEmissionColor * Mathf.LinearToGammaSpace(emissive_floor + emission);
        mat.SetColor("_EmissionColor", c);
	}

    private void OnDestroy()
    {
        mat.SetColor("_EmissionColor", baseEmissionColor);
    }
}
