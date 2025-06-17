using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TagSocket : XRSocketInteractor
{
    [Tooltip("Tag requerido para encajar.")]
    public string requiredTag = "Battery";

    protected override void Awake()
    {
        base.Awake();
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        // Solo consideramos tag correcto para permitir el hover
        return base.CanHover(interactable) && interactable.transform.CompareTag(requiredTag);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        // Primero revisa si se puede seleccionar
        if (!base.CanSelect(interactable)) return false;

        // Luego valida el tag requerido
        return interactable.transform.CompareTag(requiredTag);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        // Aquí puedes agregar lógica adicional si lo deseas.
    }
}