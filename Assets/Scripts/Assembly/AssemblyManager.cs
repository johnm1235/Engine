using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyManager : MonoBehaviour
{
    public List<AssemblyStep> steps;
    private int currentStepIndex = 0;
    public CarouselUIController carouselUI;
    public int CurrentStepIndex => currentStepIndex;

    [Header("Audio Clips")]
    public AudioClip assemblyStartClip;
    public AudioClip stepCompletedClip;
    public AudioClip assemblyCompleteClip;
    public AudioClip resetAssemblyClip;

    private void Start()
    {
        StartAssembly();
    }

    public void StartAssembly()
    {
        // Reproducir audio de inicio
        if (assemblyStartClip != null)
            AudioManager.Instance.PlaySFX(assemblyStartClip);

        for (int i = 0; i < steps.Count; i++)
        {
            steps[i].SetActive(i == 0); // solo el primero activo
        }
    }

    public void OnStepCompleted()
    {
        steps[currentStepIndex].SetCompleted(true);
        currentStepIndex++;

        // Reproducir audio de paso completado
        if (stepCompletedClip != null)
            AudioManager.Instance.PlaySFX(stepCompletedClip);

        carouselUI.OnStepCompleted();

        if (currentStepIndex < steps.Count)
        {
            steps[currentStepIndex].SetActive(true);
        }
        else
        {
            Debug.Log("Motor ensamblado completamente!");

            // Reproducir audio de final
            if (assemblyCompleteClip != null)
                AudioManager.Instance.PlaySFX(assemblyCompleteClip);
        }
    }

    public void ResetAssembly()
    {
        foreach (var step in steps)
            step.ResetStep();

        currentStepIndex = 0;

        // Reproducir audio de reseteo
        if (resetAssemblyClip != null)
            AudioManager.Instance.PlaySFX(resetAssemblyClip);

        StartAssembly();
    }

    public void ResetCurrentStep()
    {
        steps[currentStepIndex].ResetStep();
    }
}