using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparentToggleSecondMaterial : MonoBehaviour
{
    public Renderer motorRenderer;
    public Material transparentMat;
    public Material normalMat;

    [Header("UI Toggle")]
    public Toggle transparencyToggle;

    void Start()
    {
        if (transparencyToggle != null)
        {
            transparencyToggle.isOn = false;
            transparencyToggle.onValueChanged.AddListener(SetTransparency);
        }
    }

    public void SetTransparency(bool state)
    {
        if (motorRenderer == null) return;

        // Obtener el array de materiales
        Material[] mats = motorRenderer.materials;

        // Verificar que haya al menos dos materiales
        if (mats.Length < 2) return;

        // Cambiar solo el segundo material
        mats[1] = state ? transparentMat : normalMat;

        // Asignar el array modificado de vuelta al renderer
        motorRenderer.materials = mats;
    }

    private void OnDestroy()
    {
        if (transparencyToggle != null)
        {
            transparencyToggle.onValueChanged.RemoveListener(SetTransparency);
        }
    }
}