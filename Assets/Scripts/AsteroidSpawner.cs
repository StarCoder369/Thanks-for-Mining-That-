using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public List<GameObject> asteroids;

    public List<Transform> spawnPoints;

    // Should be from 0 - 100, 50 being 50% chance, 100 being 100%, etc...
    public float spawnChance;
    public float minSize;
    public float maxSize;
    public float spawnDelay;

    public float minAsteroidSpacing = 5f;
    public LayerMask asteroidLayer;

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
            if (Random.Range(0, 100) > spawnChance)
                continue;

            if (Vector2.Distance(spawnPoint.position, player.position) <= 20f)
                continue;

            if (Physics2D.OverlapCircle(
                    spawnPoint.position,
                    minAsteroidSpacing,
                    asteroidLayer) != null)
                continue;

            float randomSize = Random.Range(minSize, maxSize);

            GameObject randomAsteroid =
                asteroids[Random.Range(0, asteroids.Count)];

            GameObject activatedAsteroid = null;

            if (randomAsteroid.GetComponent<Asteroid>().asteroid1)
            {
                activatedAsteroid =
                    GameManager.Instance.normalAsteroidPool.GetObject();
            }
            else if (randomAsteroid.GetComponent<Asteroid>().asteroid2)
            {
                activatedAsteroid =
                    GameManager.Instance.roundAsteroidPool.GetObject();
            }

            if (activatedAsteroid == null)
                continue;

            activatedAsteroid.transform.position = spawnPoint.position;
            activatedAsteroid.transform.localScale =
                new Vector2(randomSize, randomSize);

            activatedAsteroid.GetComponent<Asteroid>().SetStats();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        if (spawnPoints == null)
            return;

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint != null)
            {
                Gizmos.DrawWireSphere(
                    spawnPoint.position,
                    minAsteroidSpacing);
            }
        }
    }
#endif
}