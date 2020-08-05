using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartParticles : MonoBehaviour
{
    public ParticleSystem[] LeftParticles;
    public ParticleSystem[] RightParticles;

    public void doDriftParticles(float direction)
    {
        foreach (ParticleSystem p in LeftParticles)
        {
            p.Play();
        }
        foreach (ParticleSystem p in RightParticles)
        {
            p.Play();
        }

    }

    public void stopDriftParticles()
    {
        foreach (ParticleSystem p in LeftParticles)
        {
            p.Stop();
        }
        foreach (ParticleSystem p in RightParticles)
        {
            p.Stop();
        }
    }

    public void setParticleColor(Color c)
    {
        foreach (ParticleSystem p in LeftParticles)
        {
            ParticleSystem.MainModule m = p.main;
            m.startColor= c;
        }
        foreach (ParticleSystem p in RightParticles)
        {
            ParticleSystem.MainModule m = p.main;
            m.startColor = c;
        }
    }
}
