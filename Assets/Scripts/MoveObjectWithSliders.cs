using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObjectWithSliders : MonoBehaviour
{
    [Header("Referencia al objeto que se desea mover")]
    public Transform targetObject;

    [Header("Sliders para cada eje")]
    [SerializeField] private Slider xSlider;
    [SerializeField] private Slider ySlider;
    [SerializeField] private Slider zSlider;

    // Valores mínimos y máximos para cada eje
    public Vector3 minPosition = new Vector3(-1f, -1f, -1f);
    public Vector3 maxPosition = new Vector3(1f, 1f, 1f);

    private void Start()
    {
        // Ajustamos el valor inicial de los sliders a la mitad
        xSlider.value = 0.5f;
        ySlider.value = 0.5f;
        zSlider.value = 0.5f;

        // Opcional: forzar la ejecución de las funciones de mapeo antes de añadir los listeners
        OnXSliderChanged(xSlider.value);
        OnYSliderChanged(ySlider.value);
        OnZSliderChanged(zSlider.value);

        // Añade listeners después de configurar los valores
        xSlider.onValueChanged.AddListener(OnXSliderChanged);
        ySlider.onValueChanged.AddListener(OnYSliderChanged);
        zSlider.onValueChanged.AddListener(OnZSliderChanged);
    }

    private void OnXSliderChanged(float value)
    {
        if (targetObject == null) return;
        float mappedValue = Mathf.Lerp(minPosition.x, maxPosition.x, value);
        targetObject.localPosition = new Vector3(mappedValue, targetObject.localPosition.y, targetObject.localPosition.z);
    }

    private void OnYSliderChanged(float value)
    {
        if (targetObject == null) return;
        float mappedValue = Mathf.Lerp(minPosition.y, maxPosition.y, value);
        targetObject.localPosition = new Vector3(targetObject.localPosition.x, mappedValue, targetObject.localPosition.z);
    }

    private void OnZSliderChanged(float value)
    {
        if (targetObject == null) return;
        float mappedValue = Mathf.Lerp(minPosition.z, maxPosition.z, value);
        targetObject.localPosition = new Vector3(targetObject.localPosition.x, targetObject.localPosition.y, mappedValue);
    }
}