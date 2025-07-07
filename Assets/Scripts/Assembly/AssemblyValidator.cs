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

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    public bool IsStepComplete()
    {
        // Validar posición
        float distance = Vector3.Distance(transform.position, correctPosition.position);
        if (distance > positionTolerance) return false;

        // Validar rotación
        float angle = Quaternion.Angle(transform.rotation, correctPosition.rotation);
        if (angle > rotationTolerance) return false;
        grabInteractable.enabled = false;
        return true;
    }

    public void ResetValidation()
    {
        if (startPosition != null)
        {
            transform.SetPositionAndRotation(startPosition.position, startPosition.rotation);
        }
    }

}
