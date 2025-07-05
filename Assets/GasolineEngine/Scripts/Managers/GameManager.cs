using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MotorMode { Animation, Info, Assembly }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MotorMode currentMode;

    public GameObject animationMode;
    public GameObject infoMode;
    public GameObject AssemblyMode;

    public GameObject canvasMenuMode;

    public GameObject PanelMenuHandAssembly;
    public GameObject PanelMenuHandInfo;
    public GameObject PanelMenuHandAnimation;


    public GameObject menuHand;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PanelMenuHandAnimation.SetActive(false);
        PanelMenuHandInfo.SetActive(false);
        PanelMenuHandAssembly.SetActive(false);
        menuHand.SetActive(false);
    }

    public void SetMode(int modeIndex)
    {
        menuHand.SetActive(true);
        currentMode = (MotorMode)modeIndex;

        // Activar/desactivar modos principales
        animationMode.SetActive(currentMode == MotorMode.Animation);
        infoMode.SetActive(currentMode == MotorMode.Info);
        AssemblyMode.SetActive(currentMode == MotorMode.Assembly);

        // Activar/desactivar paneles secundarios
        PanelMenuHandAnimation.SetActive(currentMode == MotorMode.Animation);
        PanelMenuHandInfo.SetActive(currentMode == MotorMode.Info);
        PanelMenuHandAssembly.SetActive(currentMode == MotorMode.Assembly);

        // Ocultar el menú principal
        canvasMenuMode.SetActive(false);
    }


    public void BackModeMenu()
    {
        menuHand.SetActive(false);
        canvasMenuMode.SetActive(true);
        animationMode.SetActive(false);
        infoMode.SetActive(false);
        AssemblyMode.SetActive(false);
        PanelMenuHandAssembly.SetActive(false);
        PanelMenuHandInfo.SetActive(false);
        PanelMenuHandAnimation.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
