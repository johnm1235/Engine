using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class TagSocket : XRSocketInteractor
{
    [Tooltip("Tag requerido para encajar.")]
    public string requiredTag = "Battery";

    // Guardamos el padre original
    private Dictionary<Transform, Transform> originalParents = new Dictionary<Transform, Transform>();

    protected override void Awake()
    {
        base.Awake();
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.CompareTag(requiredTag);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        if (!base.CanSelect(interactable)) return false;
        return interactable.transform.CompareTag(requiredTag);
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        Transform obj = args.interactableObject.transform;
        if (!originalParents.ContainsKey(obj))
        {
            originalParents[obj] = obj.parent; // Guardamos el padre original
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Transform obj = args.interactableObject.transform;

        // Restauramos el padre original
        if (originalParents.ContainsKey(obj))
        {
            Vector3 localPos = obj.localPosition;
            Quaternion localRot = obj.localRotation;

            obj.SetParent(originalParents[obj]);

            obj.localPosition = localPos;
            obj.localRotation = localRot;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        Transform obj = args.interactableObject.transform;
        if (originalParents.ContainsKey(obj))
        {
            obj.SetParent(originalParents[obj]);
            originalParents.Remove(obj);
        }
    }
}
