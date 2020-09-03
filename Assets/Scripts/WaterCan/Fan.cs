using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public float minForce = -2.0f;
    public float maxForce = 10.0f;

    public float minSwitchTimer = 1.0f;
    public float maxSwitchTimer = 3.0f;

    private float clock = 0.0f;
    private float actualForce = 0.0f;

    public ParticleSystem particleSystem;

    void Start()
    {
        clock = Random.Range(minSwitchTimer, maxSwitchTimer);
        actualForce = Random.Range(minForce, maxForce);
        GetComponent<Animator>().SetFloat("speed", actualForce);
    }

    // Update is called once per frame
    void Update()
    {
        clock -= Time.deltaTime;
        if (clock <= 0)
        {
            clock = Random.Range(minSwitchTimer, maxSwitchTimer);
            actualForce = Random.Range(minForce, maxForce);
            GetComponent<Animator>().SetFloat("speed", actualForce);
        }
    }

    private void LateUpdate()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
        int count = particleSystem.GetParticles(particles);
        for (int i = 0; i < count; i++)
        {
            particles[i].velocity = new Vector3(actualForce, particles[i].velocity.y, particles[i].velocity.z);
        }

        particleSystem.SetParticles(particles, count);
    }
}
