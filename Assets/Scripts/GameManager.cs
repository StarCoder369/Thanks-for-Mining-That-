using System.Collections.Generic;
using Mono.Cecil;
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

    public GameObject menuPanel;
    public GameObject deathPanel;
    public AcquireMessageSystem messageSystem;

    public Player player;

    public AsteroidSpawner asteroidSpawner;

    public List<OreStored> oresStorage = new();
    public List<ResourcePanel> resourcePanels = new();

    public ObjectPool normalAsteroidPool;
    public ObjectPool roundAsteroidPool;
    public ObjectPool normalEnemyPool;

    public bool gameRunning = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 0f;
        gameRunning = false;
        BackToMainMenu();
    }

    public void Play()
    {
        player.currentHealth = player.maxHealth;
        oresStorage.Clear();
        UpdateResources();
        Time.timeScale = 1f;
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (Enemy enemy in enemies)
        {
            if (enemy.normalEnemy)
            {
                normalEnemyPool.ReturnObject(enemy.gameObject);
            }
        }

        Asteroid[] asteroids = FindObjectsByType<Asteroid>(FindObjectsSortMode.None);
        foreach (Asteroid asteroid in asteroids)
        {
            if (asteroid.asteroid1)
            {
                normalAsteroidPool.ReturnObject(asteroid.gameObject);
            }
            else if (asteroid.asteroid2)
            {
                roundAsteroidPool.ReturnObject(asteroid.gameObject);
            }
        }
        player.movementLocked = false;
        gameRunning = true;
    }

    public void BackToMainMenu()
    {
        menuPanel.SetActive(true);
        gameRunning = false;
        oresStorage.Clear();
        UpdateResources();
        Time.timeScale = 0f;
    }

    public void PlayerDie()
    {
        deathPanel.SetActive(true);
        gameRunning = false;
        Time.timeScale = 0f;
    }

    public void AddItem(OreData oreToAdd, int amount)
    {
        messageSystem.ItemMessage(oreToAdd, amount);

        foreach (OreStored ores in oresStorage)
        {
            if (ores.ore == oreToAdd)
            {
                ores.amountStored += amount;
                UpdateResources();
                return;
            }
        }

        OreStored oreStorageToAdd = new()
        {
            ore = oreToAdd,
            amountStored = amount
        };

        oresStorage.Add(oreStorageToAdd);

        UpdateResources();
    }

    public void UpdateResources()
    {
        if (oresStorage.Count <= 0)
        {
            foreach (ResourcePanel panel in resourcePanels)
            {
                if (panel.gameObject.activeSelf)
                {
                    panel.gameObject.SetActive(false);
                    panel.amount = 0;
                    panel.ore = null;
                    return;
                }
            }
        }

        foreach (OreStored oreStored in oresStorage)
        {
            ResourcePanel matchingPanel = null;

            // Find existing panel already assigned to this ore
            foreach (ResourcePanel panel in resourcePanels)
            {
                if (panel.gameObject.activeSelf && panel.ore == oreStored.ore)
                {
                    matchingPanel = panel;
                    break;
                }
            }

            // If no panel exists, grab an unused one
            if (matchingPanel == null)
            {
                foreach (ResourcePanel panel in resourcePanels)
                {
                    if (!panel.gameObject.activeSelf)
                    {
                        panel.gameObject.SetActive(true);
                        panel.ore = oreStored.ore;
                        matchingPanel = panel;
                        break;
                    }
                }
            }

            // Update amount
            if (matchingPanel != null)
            {
                matchingPanel.amount = oreStored.amountStored;
            }
        }
    }
}
