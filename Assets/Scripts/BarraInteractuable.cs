using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraInteractuable : MonoBehaviour
{
    [Header("Configuración visual")]
    public Material hoverMaterial;
    public float scaleMultiplier = 1.1f;
    public float smoothSpeed = 8f;

    private Vector3 originalScale;
    private Vector3 targetScale;

    private Renderer rend;
    private Material originalMaterial;
    private bool isHovered = false;

    void Awake()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;

        rend = GetComponent<Renderer>();
        if (rend)
        {
            originalMaterial = rend.material;
        }
    }

    void Update()
    {
        if (transform.localScale != targetScale)
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * smoothSpeed);
    }

    public void OnRayEnter()
    {
        if (isHovered) return;

        isHovered = true;
        targetScale = originalScale * scaleMultiplier;
        if (rend && hoverMaterial)
            rend.material = hoverMaterial;
    }

    public void OnRayExit()
    {
        if (!isHovered) return;

        isHovered = false;
        targetScale = originalScale;
        if (rend && originalMaterial)
            rend.material = originalMaterial;
    }
}