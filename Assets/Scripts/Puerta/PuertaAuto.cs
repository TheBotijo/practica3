using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PuertaAuto : MonoBehaviour
{
    public Inventario inventario;
    public Animator PuertaAnim;
    public AudioSource clip;
    public AudioSource clip2;



    void OnTriggerEnter(Collider other)
            {           
                if (other.CompareTag("Player") && inventario.Cantidad == 5)
                {
                    PuertaAnim.SetTrigger("Open");
                    clip.Play();
                    clip2.Stop();
                }
            } 

            void OnTriggerExit(Collider other )
            {
                if (other.CompareTag("Player") && inventario.Cantidad == 5)
                {
                    PuertaAnim.SetTrigger("Close");
                }
            }
       
    
}

