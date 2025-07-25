using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AssemblyValidator : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    public Transform correctPosition;
    public Transform startPosition;

    public float positionTolerance = 0.01f;
    public float rotationTolerance = 5f;

    private Collider[] colliders;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        // Obtener todos los colliders en este objeto
        colliders = GetComponents<Collider>();
    }

    public bool IsStepComplete()
    {
        // Validar posici�n
        float distance = Vector3.Distance(transform.position, correctPosition.position);
        if (distance > positionTolerance) return false;

        // Validar rotaci�n
        float angle = Quaternion.Angle(transform.rotation, correctPosition.rotation);
        if (angle > rotationTolerance) return false;

        grabInteractable.enabled = false;

        // Desactivar todos los colliders
        if (colliders != null)
        {
            foreach (var col in colliders)
                col.enabled = false;
        }

        return true;
    }

    public void ResetValidation()
    {
        if (startPosition != null)
        {
            transform.SetPositionAndRotation(startPosition.position, startPosition.rotation);
        }

        // Reactivar los colliders y el grab interactable al resetear
        if (colliders != null)
        {
            foreach (var col in colliders)
                col.enabled = true;
        }
        if (grabInteractable != null)
            grabInteractable.enabled = true;
    }
}