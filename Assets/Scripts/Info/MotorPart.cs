using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MotorPart : MonoBehaviour
{
    public PartData data;

    private XRGrabInteractable grabInteractable;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
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
        // Verifica si el interactor NO es un socket
        if (!(args.interactorObject is XRSocketInteractor))
        {
            InfoCanvasController.Instance.ShowInfo(data);
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // De nuevo, si lo soltó la mano, oculta; si lo soltó un socket, no hace nada.
        if (!(args.interactorObject is XRSocketInteractor))
        {
            InfoCanvasController.Instance.HideInfo();
        }
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