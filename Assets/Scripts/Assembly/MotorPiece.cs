using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MotorPiece : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    // Posición y rotación inicial
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
       // grabInteractable = GetComponent<XRGrabInteractable>();

        // Guardar transformaciones iniciales
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    public void ResetPart()
    {
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }
}
