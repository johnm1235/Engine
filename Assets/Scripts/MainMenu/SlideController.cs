using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour
{
    public GameObject[] slides; 
    public Button btnSiguiente;
    public Button btnAnterior;
    public Button btnSaltar;

    private int currentSlide = 0;

    private bool forceShow = false;

    void Start()
    {
        btnSiguiente.onClick.AddListener(SiguienteSlide);
        btnAnterior.onClick.AddListener(AnteriorSlide);
        btnSaltar.onClick.AddListener(SaltarTutorial);
    }

    void OnEnable()
    {
        if (!forceShow && PlayerPrefs.GetInt("TutorialMostrado", 0) == 1)
        {
            gameObject.SetActive(false);
            return;
        }

        forceShow = false;
        currentSlide = 0;
        MostrarSlide(currentSlide);
    }

    public void ShowTutorial()
    {
        forceShow = true;
        gameObject.SetActive(true);
    }


    void MostrarSlide(int index)
    {
        for (int i = 0; i < slides.Length; i++)
            slides[i].SetActive(i == index);

        btnAnterior.gameObject.SetActive(index > 0);
        btnSiguiente.gameObject.SetActive(index < slides.Length - 1);
    }

    void SiguienteSlide()
    {
        if (currentSlide < slides.Length - 1)
        {
            currentSlide++;
            MostrarSlide(currentSlide);
        }
        else
        {
            TerminarTutorial();
        }
    }

    void AnteriorSlide()
    {
        if (currentSlide > 0)
        {
            currentSlide--;
            MostrarSlide(currentSlide);
        }
    }

    void SaltarTutorial()
    {
        TerminarTutorial();
    }

    void TerminarTutorial()
    {
        PlayerPrefs.SetInt("TutorialMostrado", 1);
        PlayerPrefs.Save();
        gameObject.SetActive(false);
    }

}

