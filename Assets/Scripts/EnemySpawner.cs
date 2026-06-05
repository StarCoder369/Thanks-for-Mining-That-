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
            if (Vector2.Distance(spawnPoint.position, player.transform.position) > 100)
            {
                GameObject randomEnemy = enemyBunches[Random.Range(0, enemyBunches.Count)];
                Instantiate(randomEnemy, spawnPoint.position, Quaternion.identity);
                break;
            }
        }

    }
}
