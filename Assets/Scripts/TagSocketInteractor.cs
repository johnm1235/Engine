using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TagSocketInteractor : XRSocketInteractor
{
    [Tooltip("Tag requerido para encajar.")]
    public string requiredTag = "Battery";

    [Tooltip("Orden de ensamblaje (0,1,2...).")]
    public int assemblyOrder = 0;

    [Tooltip("Referencia al manager de orden.")]
    public AssemblyOrderManager orderManager;

    protected override void Awake()
    {
        base.Awake();
        if (orderManager == null)
            orderManager = FindObjectOfType<AssemblyOrderManager>();
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        // Solo consideramos tag correcto aquí para permitir el hover visual
        return base.CanHover(interactable) && interactable.transform.CompareTag(requiredTag);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        if (!base.CanSelect(interactable)) return false;
        if (!interactable.transform.CompareTag(requiredTag)) return false;

        // Solo permite seleccionar si las piezas anteriores están completadas
        return orderManager != null && orderManager.ArePreviousOrdersCompleted(assemblyOrder);
    }

    // Aquí elegimos qué material mostrar en hover
    protected override Material GetHoveredInteractableMaterial(IXRHoverInteractable interactable)
    {
        // Si no está en orden, devolvemos el material de "Can't Hover"
        bool prevOk = orderManager != null && orderManager.ArePreviousOrdersCompleted(assemblyOrder);
        return prevOk ? interactableHoverMeshMaterial : interactableCantHoverMeshMaterial;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        orderManager?.MarkAsPlaced(assemblyOrder);
    }
}
