using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject bola;
    [SerializeField]
    
    void Start()
    {
        bola.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        bola.SetActive(true);
        
    }
}
