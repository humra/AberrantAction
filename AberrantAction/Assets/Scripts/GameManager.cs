﻿using System.Collections;
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
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }

        timerText.text = ((int)Time.time).ToString();
    }

    public void UpdateHPBar()
    {
        playerHealthBar.text = playerStats.GetCurrentHealth().ToString();
        bossHealthBar.text = bossController.GetCurrentHealth().ToString();
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
    }
}
