using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TagDetectorGhostDisplay : MonoBehaviour
{
    [Header("Configuración")]
    public string acceptedTag;      // El tag que debe tener el objeto para interactuar con la sombra
    public Material transparentMaterial; // Material transparente
    public Material normalMaterial;     // Material normal

    private GameObject currentObject; // Objeto que está dentro del área de la sombra
    private XRGrabInteractable grabInteractable; // Interactuable para saber si el objeto se está agarrando
    private bool isInside = false; // Para verificar si el objeto está dentro del área

    private void Start()
    {
        // Cambiar al material transparente al inicio
        ChangeGhostMaterial(transparentMaterial);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(acceptedTag))
        {
            currentObject = other.gameObject;
            grabInteractable = currentObject.GetComponent<XRGrabInteractable>();

            // Cambiar al material normal cuando el objeto entra
            ChangeGhostMaterial(normalMaterial);

            isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(acceptedTag))
        {
            // Cambiar al material transparente si el objeto sale del área
            ChangeGhostMaterial(transparentMaterial);

            isInside = false;
            currentObject = null;
            grabInteractable = null;
        }
    }

    private void Update()
    {
        if (isInside && currentObject != null && grabInteractable != null && !grabInteractable.isSelected)
        {
            // Si el objeto se ha soltado dentro del área de la sombra
            currentObject.transform.position = transform.position;
            currentObject.transform.rotation = transform.rotation;

            // Cambiar al material transparente después de soltar el objeto
            ChangeGhostMaterial(transparentMaterial);

            // Limpiar variables
            isInside = false;
            currentObject = null;
            grabInteractable = null;
        }
    }

    private void ChangeGhostMaterial(Material material)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }
    }
}
