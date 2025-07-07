using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarouselUIController : MonoBehaviour
{
    public GameObject iconPrefab;
    public Transform iconContainer;
    public List<SpriteIconData> iconDataList;

    private List<GameObject> icons = new List<GameObject>();
    private int currentIndex = 0;

    public AssemblyManager assemblyManager;

    private void Start()
    {
        InitCarousel();
        UpdateCarouselUI();
    }

    void InitCarousel()
    {
        foreach (Transform child in iconContainer)
            Destroy(child.gameObject);

        icons.Clear();

        for (int i = 0; i < iconDataList.Count; i++)
        {
            GameObject icon = Instantiate(iconPrefab, iconContainer);
            icons.Add(icon);

            TMP_Text textMeshPro = icon.GetComponentInChildren<TMP_Text>();
            if (textMeshPro != null)
            {
                textMeshPro.text = iconDataList[i].instructions;
            }

            int index = i; // local para el listener
            icon.GetComponent<Button>().onClick.AddListener(() => OnIconClicked(index));
        }
    }

    void OnIconClicked(int index)
    {
        // Solo debería entrar aquí si el icono es el paso actual
        if (index == currentIndex)
        {
            assemblyManager.ResetCurrentStep();
        }
    }

    public void OnStepCompleted()
    {
        currentIndex++;
        UpdateCarouselUI();
    }

    void UpdateCarouselUI()
    {
        for (int i = 0; i < icons.Count; i++)
        {
            Image img = icons[i].GetComponent<Image>();
            Button btn = icons[i].GetComponent<Button>();
            TextMeshProUGUI textMeshPro = icons[i].GetComponentInChildren<TextMeshProUGUI>(true);

            // Actualiza el sprite
            img.sprite = iconDataList[i].icon;

            // Determina estado
            if (i < currentIndex)
            {
                // Paso finalizado
                img.color = iconDataList[i].color;
                icons[i].transform.localScale = Vector3.one * 1f;
                btn.interactable = false; // desactivado al estar ya completado
                textMeshPro.gameObject.SetActive(false);
            }
            else if (i == currentIndex)
            {
                // Paso actual
                img.color = iconDataList[i].color;
                icons[i].transform.localScale = Vector3.one * 2f;
                btn.interactable = true; // habilitado al ser el paso activo
                textMeshPro.gameObject.SetActive(true);
            }
            else
            {
                // Futuros pasos
                img.color = Color.gray;
                icons[i].transform.localScale = Vector3.one * 1f;
                btn.interactable = false; // desactivado al ser un paso futuro
                textMeshPro.gameObject.SetActive(false);
            }
        }
    }

    public void ResetCarousel()
    {
        currentIndex = 0;
        UpdateCarouselUI();
    }
}