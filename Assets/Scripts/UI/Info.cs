using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Info : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    public bool info = true;   

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (Input.GetKeyDown("f3") && info == true)
        {
            textMesh.text = ("F2 to see controls");
            textMesh.fontSize = 10;
            textMesh.fontStyle = FontStyles.Italic;
            info = false;
        }
        if (Input.GetKeyDown("f2") && info == false)
        {
            textMesh.text = ("C for POV and mouse wheel for zoom\r\nZ for superzoom\r\nLeft Click to melee\r\nQ to change weapons\r\nDuff bottles heals you\r\nF3 to close");
            textMesh.fontSize = 18;
            info = true;
        }

    }
}
