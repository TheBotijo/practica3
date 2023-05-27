using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject bola;
    [SerializeField]
    float destroyTime = 5;
    void Start()
    {
        bola.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        bola.SetActive(true);
        Destroy(bola, destroyTime);
    }
}
