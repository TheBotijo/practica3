using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePotion : MonoBehaviour
{
    public Animator anim;
    public Collider colider;
    public AudioSource clip3;
    public HealthSystem health;
    
    public float delay = 2f;
    // Start is called before the first frame update
    void Start()
    {        
        anim = GetComponent<Animator>();
        colider = GetComponent<Collider>();
    }

    // Update is called once per frame

    private void OnTriggerStay(Collider other)
    {
        health= other.GetComponent<HealthSystem>();
        // Cambiar la variable booleana "Tocar" a true en el Animator
        if (other.CompareTag("Player")) // Si el objeto que ha tocado el objeto con este script es el personaje
        {
            clip3.Play();
            anim.SetBool("Coger", true);
            health.Heal(50);
            Destroy(colider);
            Destroy(gameObject, delay);
            
        }

    }
}
