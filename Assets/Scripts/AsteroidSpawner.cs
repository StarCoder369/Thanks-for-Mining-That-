using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public List<GameObject> asteroids;

    public List<Transform> spawnPoints;
    //Should be from 0 - 100, 50 being 50% chance, 100 being 100%, etc...
    public float spawnChance;
    public float minSize;
    public float maxSize;
    public float spawnDelay;

    float spawnTime;

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Time.time > spawnTime && GameManager.Instance.gameRunning)
        {
            spawnTime = Time.time + spawnDelay;
            SpawnAsteroids();
        }

        if (!GameManager.Instance.gameRunning)
        {
            spawnTime = 0f;
        }
    }

    public void SpawnAsteroids()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (Random.Range(0, 100) > spawnChance && Vector2.Distance(spawnPoint.position, player.position) > 20)
            {
                float randomSize = Random.Range(minSize, maxSize);
                GameObject randomAsteroid = asteroids[Random.Range(0, asteroids.Count)];
                GameObject instantiatedAsteroid = Instantiate(randomAsteroid, spawnPoint.transform.position, spawnPoint.transform.rotation);
                instantiatedAsteroid.transform.localScale = new Vector2(randomSize, randomSize);
                instantiatedAsteroid.GetComponent<Asteroid>().SetStats();
            }
        }
    }
}
