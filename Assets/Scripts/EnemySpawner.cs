using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<ObjectPool> enemyPools;

    public List<Transform> spawnPoints;

    public float minSpawnTime;
    public float maxSpawnTime;

    public int minBunchSize = 3;
    public int maxBunchSize = 6;
    public float spawnRadius = 2f;

    bool canSpawn;
    float timeToSpawn;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Time.time >= timeToSpawn)
        {
            canSpawn = true;
        }

        if (GameManager.Instance.gameRunning && canSpawn)
        {
            timeToSpawn = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
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
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (Vector2.Distance(spawnPoint.position, player.transform.position) > 35f)
            {
                int bunchSize = Random.Range(minBunchSize, maxBunchSize + 1);
                ObjectPool randomPool = enemyPools[Random.Range(0, enemyPools.Count)];

                for (int i = 0; i < bunchSize; i++)
                {
                    GameObject enemy = randomPool.GetObject();

                    Vector2 offset = Random.insideUnitCircle * spawnRadius;
                    enemy.transform.position = (Vector2)spawnPoint.position + offset;
                }

                break;
            }
        }
    }
}