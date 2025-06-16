using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para usar Toggles

public class TransparentToggle : MonoBehaviour
{
    public Renderer motorRenderer;
    public Material transparentMat;
    public Material normalMat;

    [Header("UI Toggle")]
    public Toggle transparencyToggle; // Referencia al Toggle de la UI

    void Start()
    {
        // Asegurarse de que el toggle inicie desactivado
        if (transparencyToggle != null)
        {
            transparencyToggle.isOn = false; // Establecer el estado inicial como desactivado
            transparencyToggle.onValueChanged.AddListener(SetTransparency);
        }
    }

    public void SetTransparency(bool state)
    {
        motorRenderer.material = state ? transparentMat : normalMat;
    }

    private void OnDestroy()
    {
        // Desvincular el evento para evitar errores si el objeto se destruye
        if (transparencyToggle != null)
        {
            transparencyToggle.onValueChanged.RemoveListener(SetTransparency);
        }
    }
}
