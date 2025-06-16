using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MotorPart : MonoBehaviour
{
    public PartData data;

    private XRGrabInteractable grabInteractable;

    // Posición y rotación inicial
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Guardar transformaciones iniciales
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        InfoCanvasController.Instance.ShowInfo(data);
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        InfoCanvasController.Instance.HideInfo();
    }

    public void ResetPart()
    {
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;

        // Si tiene Rigidbody y quieres evitar que se mueva bruscamente:
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
    }
}
