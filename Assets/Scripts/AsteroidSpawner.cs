using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public List<GameObject> levelOneAsteroids;
    public List<GameObject> levelTwoAsteroids;
    public List<GameObject> levelThreeAsteroids;

    public List<Transform> levelOneSpawnPoints;
    public List<Transform> levelTwoSpawnPoints;
    public List<Transform> levelThreeSpawnPoints;

    //Should be from 0 - 100, 50 being 50% chance, 100 being 100%, etc...
    public float levelOneSpawnChance;
    public float levelTwoSpawnChance;
    public float levelThreeSpawnChance;

    public void SpawnAsteroids()
    {
        foreach (Transform spawnPoint in levelOneSpawnPoints)
        {
            if (Random.Range(0, 100) > levelOneSpawnChance)
            {
                GameObject randomAsteroid = levelOneAsteroids[Random.Range(0, levelOneAsteroids.Count)];
                Instantiate(randomAsteroid, spawnPoint.transform.position, spawnPoint.transform.rotation);
            }
        }

        foreach (Transform spawnPoint in levelTwoSpawnPoints)
        {
            if (Random.Range(0, 100) > levelTwoSpawnChance)
            {
                GameObject randomAsteroid = levelTwoAsteroids[Random.Range(0, levelTwoAsteroids.Count)];
                Instantiate(randomAsteroid, spawnPoint.transform.position, spawnPoint.transform.rotation);
            }
        }

        foreach (Transform spawnPoint in levelThreeSpawnPoints)
        {
            if (Random.Range(0, 100) > levelThreeSpawnChance)
            {
                GameObject randomAsteroid = levelThreeAsteroids[Random.Range(0, levelThreeAsteroids.Count)];
                Instantiate(randomAsteroid, spawnPoint.transform.position, spawnPoint.transform.rotation);
            }
        }
    }
}
