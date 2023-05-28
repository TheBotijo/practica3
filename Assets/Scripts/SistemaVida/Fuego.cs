using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Fuego : MonoBehaviour
{
    public ParticleSystem _particleSystem;
    public HealthSystem Damage;
    public ParticleCollisionEvent[] collisionEvents;
    

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        _particleSystem.Play();
    }
    public void SetNoise(float strength)
    {
        var noise = _particleSystem.noise;
        noise.strength = strength;
    }
    private void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
             Debug.Log("quema");
             Damage.Damage(0.5f);
        }
        
    }
}
