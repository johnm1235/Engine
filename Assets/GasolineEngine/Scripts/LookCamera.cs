using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    public Camera targetCamera; // Cámara objetivo que el Canvas debe mirar

    // Update is called once per frame
    void Update()
    {
        if (targetCamera != null)
        {
            // Hacer que el Canvas mire hacia la cámara
            transform.LookAt(targetCamera.transform);

            // Opcional: Invertir el eje para que el Canvas no esté al revés
            transform.rotation = Quaternion.LookRotation(transform.position - targetCamera.transform.position);
        }
    }
}
