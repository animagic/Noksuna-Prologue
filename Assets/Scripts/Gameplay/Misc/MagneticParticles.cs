using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticParticles : MonoBehaviour
{
    public ParticleSystem p;
    public ParticleSystem.Particle[] particles;
    public Transform Target;
    public float affectDistance;
    float sqrDist;
    Transform thisTransform;



    void Start()
    {
        sqrDist = affectDistance * affectDistance;
    }


    

    public void MagnetizeParticles()
    {
        
   		particles = new ParticleSystem.Particle[p.particleCount];

		p.GetParticles(particles);

		for (int i = 0; i < particles.GetUpperBound(0); i++)
		{

			float ForceToAdd = (particles[i].startLifetime - particles[i].remainingLifetime) * (10 * Vector3.Distance(Target.position, particles[i].position));
            particles[i].velocity = (Target.position - particles[i].position).normalized * ForceToAdd;

		}

		p.SetParticles(particles, particles.Length);

		var main = p.main;
        main.simulationSpeed = 1;

    }

    public void ChangeParticleSpeed()
    {
        var mainz = p.main;
        mainz.simulationSpeed = 0.25f; 



    }
}

