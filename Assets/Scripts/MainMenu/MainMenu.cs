using Jsgaona;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{



    public void PlayGasolineEngine()
    {
       SceneLoadingManager.SceneInstance.LoadGameScene("GasolineEngineScene");
    }

    public void PlayDieselEngine()
    {
        SceneLoadingManager.SceneInstance.LoadGameScene("DieselEngineScene");
    }
    public void PlayElectricEngine()
    {
        SceneLoadingManager.SceneInstance.LoadGameScene("ElectricEngineScene");
    }


    public void OpenOptions()
    {

    }

    public void QuitGame()
    {
        PlayerPrefs.DeleteKey("TutorialMostrado"); 
        PlayerPrefs.Save();

        Application.Quit();
        // If we are running in the editor, stop playing the scene
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
