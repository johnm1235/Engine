using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResettablePart : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    public void ResetPart()
    {
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;
    }
}
