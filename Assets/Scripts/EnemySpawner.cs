using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyBunches;

    public List<Transform> spawnPoints;

    public float minSpawnTime;
    public float maxSpawnTime;

    bool canSpawn;
    float timeToSpawn;
    void Update()
    {
        if (Time.time >= timeToSpawn)
        {
            canSpawn = true;
        }

        if (GameManager.Instance.gameRunning && canSpawn)
        {
            canSpawn = false;
            SpawnEnemy();
        }

        if (!GameManager.Instance.gameRunning && timeToSpawn > 0)
        {
            canSpawn = false;
            timeToSpawn = 0f;
        }
    }

    private void SpawnEnemy()
    {
        GameObject randomEnemy = enemyBunches[Random.Range(0, enemyBunches.Count)];
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Instantiate(randomEnemy, randomSpawnPoint.position, Quaternion.identity);
        timeToSpawn = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }
}
