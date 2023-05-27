using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraChange : MonoBehaviour
{

    public GameObject ThirdCam;
    public GameObject FirstCam;
    public GameObject crosshit;
    public int CamMode;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Camera")) 
        {
            if (CamMode == 1)
            {
                CamMode = 0;
            }
            else
            {
                CamMode += 1;
            }
        } 
        StartCoroutine(CamChange());
    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds (0.01f);
        if (CamMode == 1)
        {
            crosshit.SetActive(false);
            ThirdCam.SetActive(true);
            FirstCam.SetActive(false);
            ThirdCam.GetComponent<AudioListener>().enabled = true;
            FirstCam.GetComponent<AudioListener>().enabled = false;
        }
        if(CamMode == 0)
        {
            crosshit.SetActive(true);
            ThirdCam.SetActive(false);
            FirstCam.SetActive(true);
            ThirdCam.GetComponent<AudioListener>().enabled = false;
            FirstCam.GetComponent<AudioListener>().enabled = true;
        }
    }
}
