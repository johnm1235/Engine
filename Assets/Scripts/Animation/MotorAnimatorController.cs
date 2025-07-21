using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotorAnimatorController : MonoBehaviour
{
    [Header("Motor Animation")]
    public Animator motorAnimator;
    private bool isRunning = false;
    private bool isDesarmando = false;

    [Header("UI Toggles")]
    public Toggle toggleIsRunning;
    public Toggle toggleStartDesarmar;
    public Slider sliderDesarmar;

    // NUEVO: Clips de audio para cada estado
    [Header("Audio Clips")]
    public AudioClip startMotorClip;
    public AudioClip disassembleClip;

    void Awake()
    {
        StartCoroutine(PrewarmAnimation());
    }
    void Start()
    {
        toggleIsRunning.isOn = false;
        toggleStartDesarmar.isOn = false;
        sliderDesarmar.gameObject.SetActive(false);

        toggleIsRunning.onValueChanged.AddListener(OnToggleIsRunningChanged);
        toggleStartDesarmar.onValueChanged.AddListener(OnToggleStartDesarmarChanged);
        sliderDesarmar.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private IEnumerator PrewarmAnimation()
    {
        // Reproduce la animación en silencio y la pausa de inmediato
        motorAnimator.Play("IsRunning");
        yield return 2; // Espera un frame para que cargue
        motorAnimator.Play("Idle"); // Vuelve al estado inicial
    }

    private void OnToggleIsRunningChanged(bool isOn)
    {
        isRunning = isOn;
        isDesarmando = false;
        toggleStartDesarmar.isOn = false;

        motorAnimator.speed = 1;
        motorAnimator.SetBool("IsRunning", isRunning);
        motorAnimator.SetBool("StartDesarmar", isDesarmando);

        // Reproduce sonido de arranque o apagado
        if (isOn)
        {
            if (startMotorClip != null)
                AudioManager.Instance.PlaySFX(startMotorClip);
        }
        else
        {
            if (startMotorClip != null)
                AudioManager.Instance.StopSFX(startMotorClip);

            motorAnimator.Play("Idle");
        }
    }

    private void OnToggleStartDesarmarChanged(bool isOn)
    {
        isDesarmando = isOn;
        isRunning = false;
        toggleIsRunning.isOn = false;

        motorAnimator.SetBool("StartDesarmar", isDesarmando);
        motorAnimator.SetBool("IsRunning", isRunning);

        // Reproduce sonido de desarmado
        if (isOn && disassembleClip != null)
        {
            AudioManager.Instance.PlaySFX(disassembleClip);
        }

        if (isDesarmando)
        {
            sliderDesarmar.gameObject.SetActive(true);
            motorAnimator.Play("Desarmar", 0, sliderDesarmar.value);
            motorAnimator.speed = 0;
        }
        else
        {
            sliderDesarmar.gameObject.SetActive(false);
            motorAnimator.speed = 1;
            motorAnimator.Play("Idle");
        }
    }

    private void OnSliderValueChanged(float value)
    {
        if (isDesarmando)
        {
            motorAnimator.Play("Desarmar", 0, value);
            motorAnimator.speed = 0;
        }
    }

    void Update()
    {
        if (!isDesarmando)
        {
            motorAnimator.speed = 1;
        }
    }
    public void ResetTogglesAndSlider()
    {
        sliderDesarmar.value = 0; // Reinicia el slider al valor inicial
        toggleIsRunning.isOn = false;
        toggleStartDesarmar.isOn = false;

        sliderDesarmar.gameObject.SetActive(false);
        isRunning = false;
        isDesarmando = false;
        motorAnimator.SetBool("IsRunning", false);
        motorAnimator.SetBool("StartDesarmar", false);

        motorAnimator.Play("Idle", 0, 0f); // Fuerza el frame inicial
        motorAnimator.Rebind();            // Reinicia el Animator completamente
        motorAnimator.Update(0);           // Aplica el reinicio inmediatamente
    }
}