using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour
{
    public GameObject[] slides; 
    public Button btnSiguiente;
    public Button btnAnterior;
    public Button btnSaltar;

    private int currentSlide = 0;

    void Start()
    {

        // MostrarSlide(currentSlide);
        btnSiguiente.onClick.AddListener(SiguienteSlide);
        btnAnterior.onClick.AddListener(AnteriorSlide);
        btnSaltar.onClick.AddListener(SaltarTutorial);
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
        PlayerPrefs.SetInt("TutorialMostrado", 2);
        PlayerPrefs.Save();
        gameObject.SetActive(false);
    }

}
