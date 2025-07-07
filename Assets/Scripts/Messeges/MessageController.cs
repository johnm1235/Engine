using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    public Text messageUIText; // UI Text donde se mostrará el mensaje
    public List<MessageStep> steps;

    private int currentIndex = 0;
    private bool waitingForCompletion = false;

    void Start()
    {
        ShowCurrentStep();
    }

    public void ShowCurrentStep()
    {
        if (currentIndex >= steps.Count)
        {
            Debug.Log("Todos los mensajes han sido mostrados.");
            return;
        }

        MessageStep step = steps[currentIndex];
        messageUIText.text = step.messageText;
        step.onMessageShown.Invoke();

        waitingForCompletion = true;
        step.onActionCompleted.AddListener(OnStepCompleted);
    }

    public void OnStepCompleted()
    {
        if (!waitingForCompletion) return;

        // Limpieza del listener actual
        steps[currentIndex].onActionCompleted.RemoveListener(OnStepCompleted);

        currentIndex++;
        waitingForCompletion = false;

        ShowCurrentStep();
    }
}
