using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    //public GameObject bienvenidaPanel;

    void Start()
    {
        if (PlayerPrefs.GetInt("TutorialMostrado", 1) == 2)
        {
            tutorialPanel.SetActive(false);
          //  bienvenidaPanel.SetActive(false);
        }
        else
        {
            tutorialPanel.SetActive(true);
          //  bienvenidaPanel.SetActive(true);
        }
    }
}


