using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingLevelSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] enemySpawnPoints;
    [SerializeField]
    private Enemy[] enemyTypes;
    [SerializeField]
    private float spawnRate = 1f;
    private BossController bossController;


    private void Start()
    {
        bossController = FindObjectOfType<BossController>();
        InvokeRepeating("SpawnEnemyInTestingLevel", 1f, spawnRate);
    }

    private void SpawnEnemyInTestingLevel()
    {
        if (bossController.GetNumberOfEnemies() >= enemySpawnPoints.Length)
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

        Enemy instance = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], enemySpawnPoints[position].transform);

        Vector3 aimTarget = bossController.transform.position;
        aimTarget.z = 0f;
        Vector3 objPos = enemySpawnPoints[position].transform.position;
        aimTarget.x = aimTarget.x - objPos.x;
        aimTarget.y = aimTarget.y - objPos.y;
        float angle = Mathf.Atan2(aimTarget.y, aimTarget.x) * Mathf.Rad2Deg;

        instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        if (Random.Range(0, 2) % 2 == 0)
        {
            instance.GetComponent<Enemy>().SetWillDropHealthGlobe(true);
        }

        bossController.AddNewEnemy(instance);
    }
}
