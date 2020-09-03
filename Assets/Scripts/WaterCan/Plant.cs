using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> events;
        events = new List<ParticleCollisionEvent>();
        ParticleSystem m_System = other.GetComponent<ParticleSystem>();
        ParticleSystem.Particle[] m_Particles;
        m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
        ParticlePhysicsExtensions.GetCollisionEvents(other.GetComponent<ParticleSystem>(), gameObject, events);
        float minDist = 1000.0f;
        int particleId = -1;
        int numParticlesAlive = m_System.GetParticles(m_Particles);
        foreach (ParticleCollisionEvent coll in events)
        {
            if (coll.intersection != Vector3.zero)
            {
                for (int i = 0; i < numParticlesAlive; i++)
                {
                    float tmp = ((Vector2)(m_System.transform.TransformPoint(m_Particles[i].position) - coll.intersection)).magnitude;
                    if(tmp < minDist && m_Particles[i].remainingLifetime > 0)
                    {
                        minDist = tmp;
                        particleId = i;
                    }
                    /*
                    if (Vector3.Magnitude(m_Particles[i].position - coll.intersection) < 3.5f && m_Particles[i].remainingLifetime > 0)
                    {
                        Debug.Log(Vector3.Magnitude(m_Particles[i].position - coll.intersection));
                        Debug.Log("Found Particle");
                        m_Particles[i].remainingLifetime = -1;
                        waterCount++;
                        break;
                    }
                    */
                    if(i==numParticlesAlive-1)
                    {
                        Debug.Log("MiDist = " + minDist);
                    }
                }
                if(particleId != -1)
                {
                    m_Particles[particleId].remainingLifetime = -1;
                    waterCount++;
                }
            }
        }
        m_System.SetParticles(m_Particles);
    }
}
