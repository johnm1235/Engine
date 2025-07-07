using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyStep : MonoBehaviour
{
    public string stepName;
    public GameObject pieceRoot;
    public Transform startPosition;

    [Header("Sub-piezas que pertenecen a este paso")]
    public List<AssemblyValidator> subpieceValidators;
    [Header("Objeto al que se unirá este paso cuando se complete")]
    public Transform newParentOnComplete;

    private bool isCompleted = false;

    public void SetActive(bool active)
    {
        pieceRoot.SetActive(active);

        if (subpieceValidators != null)
        {
            foreach (var sub in subpieceValidators)
                sub.gameObject.SetActive(active);
        }

        if (active)
            ResetStep();
    }

    public void SetCompleted(bool completed)
    {
        isCompleted = completed;
        pieceRoot.SetActive(true);
    }

    public void ResetStep()
    {
        isCompleted = false;

        pieceRoot.transform.SetPositionAndRotation(
            startPosition.position,
            startPosition.rotation);

        if (subpieceValidators != null)
        {
            foreach (var sub in subpieceValidators)
            {
                sub.ResetValidation();
                sub.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        // Si no hay sub-piezas, salir
        if (subpieceValidators == null || subpieceValidators.Count == 0) return;

        // Cada vez que una sub-pieza se complete, hacerla hija inmediatamente
        foreach (var sub in subpieceValidators)
        {
            // Verifica si está completa y aún no está bajo el nuevo padre
            if (sub.IsStepComplete() && sub.transform.parent != newParentOnComplete)
            {
                sub.transform.SetParent(newParentOnComplete);
            }
        }

        // Revisar si todas están listas para marcar el paso completado
        if (!isCompleted)
        {
            bool allOK = true;
            foreach (var sub in subpieceValidators)
            {
                if (!sub.IsStepComplete())
                {
                    allOK = false;
                    break;
                }
            }

            if (allOK)
            {
                isCompleted = true;
                FindObjectOfType<AssemblyManager>().OnStepCompleted();
            }
        }
    }
}