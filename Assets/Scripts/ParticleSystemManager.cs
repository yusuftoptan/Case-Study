using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public static ParticleSystemManager instance;

    private ParticleSystem[] _particleSystems;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            this.enabled = false;

        _particleSystems = GetComponentsInChildren<ParticleSystem>(true);
    }

    public void Play()
    {
        for(int i = 0; i < _particleSystems.Length; i++)
        {
            _particleSystems[i].Stop();
            _particleSystems[i].Clear();
            _particleSystems[i].Play();
        }
    }
}
