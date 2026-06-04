using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OreStored
{
    public OreData ore;
    public int amountStored;

}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public AsteroidSpawner asteroidSpawner;

    public List<OreStored> oresStorage = new();

    public bool gameRunning;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Play()
    {
        asteroidSpawner.SpawnAsteroids();
    }

    public void AddItem(OreData oreToAdd, int amount)
    {
        foreach (OreStored ores in oresStorage)
        {
            if (ores.ore == oreToAdd)
            {
                ores.amountStored += amount;
                return;
            }
        }

        OreStored oreStorageToAdd = new()
        {
            ore = oreToAdd,
            amountStored = amount
        };

        oresStorage.Add(oreStorageToAdd);
    }
}
