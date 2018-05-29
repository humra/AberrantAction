using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] spawnPoints;
    [SerializeField]
    private Enemy[] enemyTypes;
    [SerializeField]
    private TextAsset waveSettings;
    [SerializeField]
    private GameObject bossAimTarget;
    [SerializeField]
    private BossController bossController;
    [SerializeField]
    private Text enemiesLeftText;

    private string[] waves;
    private float[] delays;
    private int[] spawnPointIndexes;
    private int[] enemyTypeIndexes;
    private bool[] healthGlobeSpawned;
    private float timestamp;
    private int waveCounter;
    private bool levelComplete;

	void Start () {
        waveCounter = 0;
        LoadWaves();
        levelComplete = false;
        enemiesLeftText.text = waves.Length.ToString();
    }

    void Update () {
        if(levelComplete)
        {
            return;
        }

        if (waveCounter == waves.Length)
        {
            CheckForLevelComplete();
            return;
        }

        if (timestamp <= Time.time)
        {
            SpawnEnemy();
            enemiesLeftText.text = (waves.Length - waveCounter).ToString();
        }
    }

    private void LoadWaves()
    {
        waves = Regex.Split(waveSettings.ToString(), "\n|\r\n");

        delays = new float[waves.Length];
        spawnPointIndexes = new int[waves.Length];
        enemyTypeIndexes = new int[waves.Length];
        healthGlobeSpawned = new bool[waves.Length];

        for(int i = 0; i < waves.Length; i++)
        {
            string[] temp = Regex.Split(waves[i], ";");

            delays[i] = float.Parse(temp[0]);
            spawnPointIndexes[i] = int.Parse(temp[1]);
            enemyTypeIndexes[i] = int.Parse(temp[2]);
            healthGlobeSpawned[i] = (temp[3] == "true") ? true : false;
        }

        timestamp = Time.time + delays[0];
    }

    private void SpawnEnemy()
    {
        timestamp = Time.time + delays[waveCounter];

        Enemy temp = Instantiate(enemyTypes[enemyTypeIndexes[waveCounter]], spawnPoints[spawnPointIndexes[waveCounter]].transform, false);
        temp.SetWillDropHealthGlobe(healthGlobeSpawned[waveCounter]);

        Vector3 aimTarget = bossAimTarget.transform.position;
        aimTarget.z = 0f;
        Vector3 objPos = spawnPoints[spawnPointIndexes[waveCounter]].transform.position;
        aimTarget.x = aimTarget.x - objPos.x;
        aimTarget.y = aimTarget.y - objPos.y;
        float angle = Mathf.Atan2(aimTarget.y, aimTarget.x) * Mathf.Rad2Deg;

        temp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        FindObjectOfType<BossController>().AddNewEnemy(temp);

        waveCounter++;
    }

    private void CheckForLevelComplete()
    {
        if(bossController.GetNumberOfEnemies() == 0)
        {
            levelComplete = true;
            FindObjectOfType<GameManager>().LevelComplete();
        }
    }
}
