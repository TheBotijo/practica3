using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Policia : MonoBehaviour
{
    public HealthSystem Vida;
    public Animator Animator;
    public WanderWaypoint Speed;
    public ParticleSystem particle;
    //Animaci� de daño y muerte
    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Colision")
        {
            if (Vida.health <= 0)
            {
                
                //isDeath = true;
                Animator.SetTrigger("IsDead");
                Speed.Speed = 0;
                Speed.GetComponent<WanderWaypoint>().enabled=false;
                
                //Se acaba el juego

            }
            else
            {
                //Recibe daño
                Animator.SetTrigger("Damage");
                particle.Play();
                Vida.Damage(10);
                
            }

        }
    }
}
