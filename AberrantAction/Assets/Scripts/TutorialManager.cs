using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] slides;
    [SerializeField]
    private Button previousSlideBtn;
    [SerializeField]
    private Button nextSlideBtn;
    private int activeSlideIndex = 0;

    private void Start()
    {
        SetSlideButtonActive();
    }

    public void NextSlide()
    {
        if(activeSlideIndex == slides.Length - 1)
        {
            return;
        }

        activeSlideIndex++;
        UpdateActiveSlide();
    }

    public void PreviousSlide()
    {
        if(activeSlideIndex == 0)
        {
            return;
        }

        activeSlideIndex--;
        UpdateActiveSlide();
    }

    private void UpdateActiveSlide()
    {
        slides[activeSlideIndex].SetActive(true);

        for(int i = 0; i < slides.Length; i++)
        {
            if(i != activeSlideIndex)
            {
                slides[i].SetActive(false);
            }
        }

        SetSlideButtonActive();
    }

    private void SetSlideButtonActive()
    {
        if (activeSlideIndex == 0)
        {
            previousSlideBtn.gameObject.SetActive(false);
        }
        else
        {
            previousSlideBtn.gameObject.SetActive(true);
        }

        if(activeSlideIndex == slides.Length - 1)
        {
            nextSlideBtn.gameObject.SetActive(false);
        }
        else
        {
            nextSlideBtn.gameObject.SetActive(true);
        }
    }

    public void ExitTutorial()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
