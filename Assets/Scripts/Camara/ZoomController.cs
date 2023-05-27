using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZoomController : MonoBehaviour
{
    [SerializeField]
    private float ScrollSpeed = 18;

    public Camera WhichIsZoomCam;
    private Camera ZoomCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        ZoomCamera = WhichIsZoomCam;
    }

    // Update is called once per frame
    void Update()
    {
        //Cambiar el fieldOfView de la camera segons el input de la roda ratolí i la velocitat assignada
        ZoomCamera.fieldOfView -= ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera.fieldOfView = Mathf.Clamp(ZoomCamera.fieldOfView, 30, 70);

        if (SuperZoom())
        {
            //quantitat de superzoom a aplicar
            ZoomCamera.fieldOfView = 10;
        }

    }

    private bool SuperZoom()
    {
        //Mentre estigui presionada la Z "GetKey"
        return Input.GetButton("Superzoom"); 
    }
}
