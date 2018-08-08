using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] slides;
    private int activeSlideIndex = 0;


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
    }

    public void ExitTutorial()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
