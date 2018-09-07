using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetLevelProgress()
    {
        SaveLoad.levelsUnlocked = 1;
        SaveLoad.Save();
        FindObjectOfType<SaveLoadManager>().RefreshUnlockedLevels();
    }
}
