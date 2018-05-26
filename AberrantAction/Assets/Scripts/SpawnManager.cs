using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

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
        }
    }

    private void LoadWaves()
    {
        waves = Regex.Split(waveSettings.ToString(), "\r\n|\n");

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
