using System.Collections;
using System.Collections.Generic;
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
    private Text bossHealthBar;
    private PlayerStats playerStats;
    private PlayerController playerController;
    private BossController bossController;
    [SerializeField]
    private GameObject[] enemySpawnPoints;
    [SerializeField]
    private Enemy[] enemyTypes;
    [SerializeField]
    private float spawnRate = 1f;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private GameObject levelCompleteScreen;
    [SerializeField]
    private Text timerText;

    void Start () {
        Time.timeScale = 1f;
        playerStats = player.GetComponent<PlayerStats>();
        bossController = boss.GetComponent<BossController>();
        playerController = player.GetComponent<PlayerController>();
        playerHealthBar.text = playerStats.GetCurrentHealth().ToString();
        bossHealthBar.text = bossController.GetCurrentHealth().ToString();
        InvokeRepeating("UpdateHPBar", 0.1f, 0.1f);

        if(SceneManager.GetActiveScene().name.Equals("TestingLevel"))
        {
            InvokeRepeating("SpawnEnemyInTestingLevel", 1f, spawnRate);
        }
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }

        timerText.text = ((int)Time.time).ToString();
    }

    private void DealDamageToPlayer(float damage)
    {
        playerStats.TakeDamage(damage);

        UpdateHPBar();
    }

    public void UpdateHPBar()
    {
        playerHealthBar.text = playerStats.GetCurrentHealth().ToString();
        bossHealthBar.text = bossController.GetCurrentHealth().ToString();
    }

    private void SpawnEnemyInTestingLevel()
    {
        if(bossController.GetNumberOfEnemies() >= enemySpawnPoints.Length)
        {
            return;
        }

        int position;
        int counter = 0;
        do
        {
            position = Random.Range(0, enemySpawnPoints.Length);
            counter += 1;
        } while (enemySpawnPoints[position].transform.childCount > 0 && counter < enemySpawnPoints.Length);

        Enemy instance = Instantiate(enemyTypes[0], enemySpawnPoints[position].transform);

        Vector3 aimTarget = bossController.transform.position;
        aimTarget.z = 0f;
        Vector3 objPos = enemySpawnPoints[position].transform.position;
        aimTarget.x = aimTarget.x - objPos.x;
        aimTarget.y = aimTarget.y - objPos.y;
        float angle = Mathf.Atan2(aimTarget.y, aimTarget.x) * Mathf.Rad2Deg;

        instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        if(Random.Range(0, 2) % 2 == 0)
        {
            instance.GetComponent<Enemy>().SetWillDropHealthGlobe(true);
        }

        bossController.AddNewEnemy(instance);
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
        Debug.Log("level complete!");
        Time.timeScale = 0f;
        levelCompleteScreen.SetActive(true);
    }
}
