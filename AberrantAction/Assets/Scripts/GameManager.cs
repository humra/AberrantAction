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
    private GameObject[] enemyTypes;
    [SerializeField]
    private float spawnRate = 1f;
    [SerializeField]
    private GameObject gameOverScreen;

    void Start () {
        playerStats = player.GetComponent<PlayerStats>();
        bossController = boss.GetComponent<BossController>();
        playerController = player.GetComponent<PlayerController>();
        playerHealthBar.text = playerStats.GetCurrentHealth().ToString();
        bossHealthBar.text = bossController.GetCurrentHealth().ToString();
        InvokeRepeating("UpdateHPBar", 0.1f, 0.1f);
        InvokeRepeating("SpawnEnemy", 1f, spawnRate);
    }
	
	void Update () {

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

    private void SpawnEnemy()
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

        GameObject instance = Instantiate(enemyTypes[0], enemySpawnPoints[position].transform);

        Vector3 aimTarget = bossController.transform.position;
        aimTarget.z = 0f;
        Vector3 objPos = enemySpawnPoints[position].transform.position;
        aimTarget.x = aimTarget.x - objPos.x;
        aimTarget.y = aimTarget.y - objPos.y;
        float angle = Mathf.Atan2(aimTarget.y, aimTarget.x) * Mathf.Rad2Deg;

        instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        bossController.AddNewEnemy(instance);
    }

    public void GameOver(string cause)
    {
        UpdateHPBar();
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach(Enemy enemy in allEnemies)
        {
            enemy.StopAllActions();
        }
        playerController.SetDisabled(true);
        bossController.StopAllActions();
        CancelInvoke();
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
}
