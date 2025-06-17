using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoCanvasController : MonoBehaviour
{
    public static InfoCanvasController Instance;

    public GameObject infoCanvas;
    public TextMeshProUGUI partNameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI failuresText;
    public Image partImage;

    // NUEVO: Clips de sonido
    [Header("Audio Clips")]
    public AudioClip showInfoClip;
    public AudioClip hideInfoClip;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        infoCanvas.SetActive(false);
    }

    public void ShowInfo(PartData data)
    {
        partNameText.text = data.partName;
        descriptionText.text = data.description;
        // ...

        if (data.partSprite != null)
        {
            partImage.enabled = true;
            partImage.sprite = data.partSprite;
        }
        else
        {
            partImage.enabled = false;
        }

        // Reproducir el sonido de mostrar
        if (showInfoClip != null)
            AudioManager.Instance.PlaySFX(showInfoClip);

        infoCanvas.SetActive(true);
        Debug.Log("InfoCanvasController: ShowInfo called with data: " + data.partName);
    }

    public void HideInfo()
    {
        // Reproducir el sonido de ocultar
        if (hideInfoClip != null)
            AudioManager.Instance.PlaySFX(hideInfoClip);

        infoCanvas.SetActive(false);
    }
}