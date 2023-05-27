using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, 10);
        Gizmos.DrawWireSphere(transform.position, 10);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5);

    }
}
