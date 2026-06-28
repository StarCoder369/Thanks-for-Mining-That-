using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public TMP_Text coinsAmountTxt;

    public Player player;

    public AsteroidSpawner asteroidSpawner;

    public List<OreStored> oresStorage = new();
    public List<ResourcePanel> resourcePanels = new();

    public ObjectPool normalAsteroidPool;
    public ObjectPool roundAsteroidPool;
    public ObjectPool normalEnemyPool;

    public GameObject craftingPanel;

    public bool gameRunning = true;

    public int coins;

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

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            DisableEnableCrafting();
        }

        coinsAmountTxt.text = coins.ToString();
    }

    public void DisableEnableCrafting()
    {
        craftingPanel.SetActive(!craftingPanel.activeSelf);

        Time.timeScale = craftingPanel.activeSelf ? 0 : 1;
    }

    public void Play()
    {
        player.currentHealth = player.maxHealth;
        player.transform.position = Vector3.zero;
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

        GameObject[] abilities = GameObject.FindGameObjectsWithTag("Ability");
        foreach (GameObject ability in abilities)
        {
            Destroy(ability);
        }

        GameObject[] abilities1 = GameObject.FindGameObjectsWithTag("AsteroidLock");
        foreach (GameObject ability in abilities1)
        {
            Destroy(ability);
        }

        GameObject[] abilities2 = GameObject.FindGameObjectsWithTag("GrowTool");
        foreach (GameObject ability in abilities2)
        {
            Destroy(ability);
        }

        player.GetComponent<PlayerGameInventory>().cooldown = 1.5f;
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
        messageSystem.DisablePanels();
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

    public void RemoveItem(OreData oreToRemove, int amount)
    {
        foreach (OreStored ores in oresStorage)
        {
            if (ores.ore == oreToRemove)
            {
                if ((ores.amountStored -= amount) <= 0)
                {
                    oresStorage.Remove(ores);
                }
                else
                {
                    ores.amountStored -= amount;
                }
                UpdateResources();
                return;
            }
        }
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

        foreach (ResourcePanel panel in resourcePanels)
        {
            bool foundMatch = false;

            foreach (OreStored oreStored in oresStorage)
            {
                if (oreStored.ore == panel.ore)
                {
                    foundMatch = true;
                    break;
                }
            }

            if (!foundMatch)
            {
                panel.gameObject.SetActive(false);
            }
        }
    }

    public int ContainsResource(OreData ore)
    {
        int itemCount = 0;
        for (int i = 0; i < oresStorage.Count; i++)
        {
            if (oresStorage[i].ore == ore)
            {
                itemCount += oresStorage[i].amountStored;
            }
        }
        return itemCount;
    }
}
