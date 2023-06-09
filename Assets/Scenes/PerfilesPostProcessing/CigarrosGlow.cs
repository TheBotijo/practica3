using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CigarrosGlow : MonoBehaviour
{

    private PostProcessVolume _postVolume;
    private Bloom _postBloom;
    public Destruir destruir;
    public Transform player;
    public Transform cigarro ;
    public float distancia1 = 20;
    public float distancia2 = 18;
    public float distancia3 = 16;
    public float distancia4 = 15;
    // Start is called before the first frame update
    void Start()
    {
        _postVolume = GetComponent<PostProcessVolume>();
        _postVolume.profile.TryGetSettings(out _postBloom);
        destruir = GameObject.Find("Cigarette").GetComponent<Destruir>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float dist = Vector3.Distance(cigarro.position, player.position);
        if (dist < distancia1 && dist > distancia2)
        {
            _postBloom.intensity.value = 5;
        }
        else if(dist < distancia2 && dist > distancia3)
        {
        _postBloom.intensity.value = 10;
        }
        else if (dist < distancia3 && dist > distancia4)
        {
            _postBloom.intensity.value = 20;
        }
        else if (dist < 3)
        {
            //pressE.SetActive(true);
            destruir.pressEOn();
            _postBloom.intensity.value = 50;
        }
        else if(dist < distancia4)
        {
            //pressE.SetActive(true);
            _postBloom.intensity.value = 30;
            destruir.pressEOff();
        }
    }
}
