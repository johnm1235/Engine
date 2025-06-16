using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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

        // Activar / desactivar TODAS las sub-piezas
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

        // Reposiciona la pieza raíz
        pieceRoot.transform.SetPositionAndRotation(
            startPosition.position,
            startPosition.rotation);

        // Resetea todas las sub-piezas y las mantiene activas
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
        if (isCompleted) return;
        if (subpieceValidators == null || subpieceValidators.Count == 0) return;

        // ¿Están TODAS completas?
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
            // HACER HIJO DEL NUEVO PADRE
            if (newParentOnComplete != null)
            {
                this.transform.SetParent(newParentOnComplete);
            }
            FindObjectOfType<AssemblyManager>().OnStepCompleted();
        }
    }

}
