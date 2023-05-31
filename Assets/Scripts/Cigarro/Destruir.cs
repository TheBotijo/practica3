using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Destruir : MonoBehaviour
{
    public Inventario inventario;
    public Animator anim;
    public Collider colider;
    public AudioSource clip3;
    public GameObject postprocess;
    public Transform _player;
    public float DetectionDistance;
    public float delay = 2f;
    public GameObject pressE;
    // Start is called before the first frame update
    void Start()
    {
        inventario = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
        anim = GetComponent<Animator>();
        colider = GetComponent<Collider>();
    }

    // Update is called once per frame
    private void Update()
    {
        bool isPlayerClose = IsPlayerClose();
        if (isPlayerClose)
        {
            pressE.SetActive(true);
        }
        else
        {
            pressE.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // Cambiar la variable booleana "Tocar" a true en el Animator

        if(other.CompareTag("Player") && Input.GetButtonDown("Coger")) // Si el objeto que ha tocado el objeto con este script es el personaje
        {
            clip3.Play();
            anim.SetBool("Tocar", true);
            Destroy(colider);
            Destroy(gameObject, delay);
            Destroy(postprocess, delay);
            inventario.Cantidad++;
            
        }
    }

    private bool IsPlayerClose()
    {
        Vector3 direction = _player.position - transform.position;
        return Vector3.Distance(_player.position, transform.position) < DetectionDistance;
    }
}
