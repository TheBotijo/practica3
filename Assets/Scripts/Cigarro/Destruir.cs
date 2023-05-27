using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruir : MonoBehaviour
{
    public Inventario inventario;
    public Animator anim;
    public Collider colider;
    public AudioSource clip3;
    public float delay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        inventario = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
        anim = GetComponent<Animator>();
        colider = GetComponent<Collider>();
    }

    // Update is called once per frame

    private void OnTriggerStay(Collider other)
    {
         // Cambiar la variable booleana "Tocar" a true en el Animator
        if (other.CompareTag("Player") && Input.GetButtonDown("Coger")) // Si el objeto que ha tocado el objeto con este script es el personaje
        {
            clip3.Play();
            anim.SetBool("Tocar", true);
            Destroy(colider);
            Destroy(gameObject, delay);
            inventario.Cantidad++;
            
        }

    }
}
