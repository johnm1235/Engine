using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Transform targetObject;      // Objeto al que rotar
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // Eje de rotación (por defecto, gira en Y)
    [SerializeField] private float rotationSpeed = 90f;   // Grados por segundo

    private bool rotating;

    // Variables para guardar la posición/rotación inicial
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        if (targetObject != null)
        {
            initialPosition = targetObject.localPosition;
            initialRotation = targetObject.localRotation;
        }
    }

    void Update()
    {
        if (rotating && targetObject != null)
        {
            targetObject.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rotating = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rotating = false;
    }

    // Método para restaurar la posición/rotación inicial del objeto
    public void ResetObject()
    {
        if (targetObject != null)
        {
            targetObject.localPosition = initialPosition;
            targetObject.localRotation = initialRotation;
        }
    }
}