using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FollowTarget : MonoBehaviour
{
    public Transform leftHand;         // Asignar en el inspector
    public Transform leftController;   // Asignar en el inspector
    public Vector3 positionOffset = new Vector3(0.1f, 0.0f, 0.1f); // Ajustable
    public Vector3 rotationOffset = Vector3.zero;

    private Transform currentTarget;

    void Update()
    {
        // Detectar si la mano izquierda está activa
        bool isHandActive = false;
        InputDevice leftDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        if (leftDevice.isValid && leftDevice.TryGetFeatureValue(CommonUsages.isTracked, out bool isTracked) && isTracked)
        {
            isHandActive = true;
        }
        // Elegir target actual
        if (isHandActive && leftHand != null)
        {
            currentTarget = leftHand;
        }
        else if (leftController != null)
        {
            currentTarget = leftController;
        }
    }

    void LateUpdate()
    {
        if (currentTarget == null) return;

        // Posición con offset en el espacio local del target (más confiable)
        transform.position = currentTarget.TransformPoint(positionOffset);
        transform.rotation = currentTarget.rotation * Quaternion.Euler(rotationOffset);
    }
}