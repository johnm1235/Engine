using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartupMessageSystem : MonoBehaviour
{
    [Header("Lista de mensajes")]
    [TextArea(3, 10)]
    public List<string> messages;

    [Header("Referencia al texto UI")]
    public TMP_Text messageText;

    [Header("Tiempo mínimo entre mensajes (evita clics rápidos)")]
    public float messageCooldown = 0.5f;

    [Header("Referencia al menu principal")]
    public GameObject menu;

    private int currentMessage = 0;
    private float lastClickTime = 0f;

    void Start()
    {
        if (messages != null && messages.Count > 0)
        {
            currentMessage = 0;
            messageText.text = messages[currentMessage];
        }
    }

    public void ShowNextMessage()
    {
        currentMessage++;

        if (currentMessage < messages.Count)
        {
            messageText.text = messages[currentMessage];
        }
        else
        {
            // Oculta el canvas o desactiva el objeto
          gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
        }
    }
}
