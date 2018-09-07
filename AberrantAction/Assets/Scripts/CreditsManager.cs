using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour {

    public void ExitCredits()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
