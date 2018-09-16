using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private Text playerHealthBar;
    [SerializeField]
    private SimpleHealthBar bossHealthBar;
    private float bossMaxHealth;
    private PlayerStats playerStats;
    private PlayerController playerController;
    private BossController bossController;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private GameObject optionsScreen;
    [SerializeField]
    private GameObject levelCompleteScreen;
    [SerializeField]
    private int lastLevelNumber;

    void Start () {
        Time.timeScale = 1f;
        playerStats = player.GetComponent<PlayerStats>();
        bossController = boss.GetComponent<BossController>();
        playerController = player.GetComponent<PlayerController>();
        playerHealthBar.text = playerStats.GetCurrentHealth().ToString();
        bossMaxHealth = bossController.GetMaxHP();
        InvokeRepeating("UpdateHPBar", 0.1f, 0.1f);
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
    }

    public void ToggleOptions()
    {
        optionsScreen.SetActive(!optionsScreen.active);
        pauseScreen.SetActive(!pauseScreen.active);
    }

    public void UpdateHPBar()
    {
        playerHealthBar.text = playerStats.GetCurrentHealth().ToString();
        bossHealthBar.UpdateBar(bossController.GetCurrentHealth(), bossMaxHealth);
    }

    public void GameOver(string cause)
    {
        Time.timeScale = 0f;
        UpdateHPBar();
        gameOverScreen.SetActive(true);
        GameObject.Find("GameOverDeathText").GetComponent<Text>().text = cause + " DIED";
    }

    public void LoadDifferentScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeLevel()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    public void LevelComplete()
    {
        Time.timeScale = 0f;
        levelCompleteScreen.SetActive(true);

        UnlockNextLevel();
    }

    private void UnlockNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if(currentLevel == lastLevelNumber)
        {
            return;
        }

        SaveLoad.levelsUnlocked = currentLevel + 1;
        SaveLoad.Save();
    }
}
