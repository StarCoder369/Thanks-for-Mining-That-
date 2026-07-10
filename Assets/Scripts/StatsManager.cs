using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    // Statistics
    public float timePlayed;
    public float timeInGame;

    public int enemiesKilled;
    public int asteroidsDestroyed;
    public int timesDied;
    public int totalCoins;
    public int totalCopperCount;
    public int totalIronCount;
    public int totalGoldCount;
    public int totalToolUsage;
    public int totalCompletedRuns;

    // Save data
    public int coins;

    public const int ToolCount = 8;

    public bool[] toolUnlocked = new bool[ToolCount];
    public bool[] toolEquipped = new bool[ToolCount];

    public bool statsScreenAppeared;

    [Header("Stats Screen Text Fields for This Run")]
    public TMP_Text hoursPlayedThisRun;
    public TMP_Text coinsCollectedThisRun;
    public TMP_Text enemiesKilledThisRun;
    public TMP_Text toolsUsedThisRun;
    public TMP_Text asteroidsDestroyedThisRun;
    public TMP_Text oresCollectedThisRun;
    public TMP_Text timesDiedThisRun;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Load();
    }

    private void OnApplicationQuit() => Save();

    private void OnApplicationPause(bool paused)
    {
        if (paused)
            Save();
    }

    public void Save()
    {
        // Stats
        PlayerPrefs.SetFloat(nameof(timePlayed), timePlayed);
        PlayerPrefs.SetFloat(nameof(timeInGame), timeInGame);

        PlayerPrefs.SetInt(nameof(enemiesKilled), enemiesKilled);
        PlayerPrefs.SetInt(nameof(asteroidsDestroyed), asteroidsDestroyed);
        PlayerPrefs.SetInt(nameof(timesDied), timesDied);
        PlayerPrefs.SetInt(nameof(totalCoins), totalCoins);
        PlayerPrefs.SetInt(nameof(totalCopperCount), totalCopperCount);
        PlayerPrefs.SetInt(nameof(totalIronCount), totalIronCount);
        PlayerPrefs.SetInt(nameof(totalGoldCount), totalGoldCount);
        PlayerPrefs.SetInt(nameof(totalToolUsage), totalToolUsage);
        PlayerPrefs.SetInt(nameof(totalCompletedRuns), totalCompletedRuns);

        // Save data
        PlayerPrefs.SetInt(nameof(coins), coins);

        for (int i = 0; i < ToolCount; i++)
        {
            PlayerPrefs.SetInt($"ToolUnlocked{i}", toolUnlocked[i] ? 1 : 0);
            PlayerPrefs.SetInt($"ToolEquipped{i}", toolEquipped[i] ? 1 : 0);
        }

        PlayerPrefs.SetInt(nameof(statsScreenAppeared), statsScreenAppeared ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        // Stats
        timePlayed = PlayerPrefs.GetFloat(nameof(timePlayed), 0f);
        timeInGame = PlayerPrefs.GetFloat(nameof(timeInGame), 0f);

        enemiesKilled = PlayerPrefs.GetInt(nameof(enemiesKilled), 0);
        asteroidsDestroyed = PlayerPrefs.GetInt(nameof(asteroidsDestroyed), 0);
        timesDied = PlayerPrefs.GetInt(nameof(timesDied), 0);
        totalCoins = PlayerPrefs.GetInt(nameof(totalCoins), 0);
        totalCopperCount = PlayerPrefs.GetInt(nameof(totalCopperCount), 0);
        totalIronCount = PlayerPrefs.GetInt(nameof(totalIronCount), 0);
        totalGoldCount = PlayerPrefs.GetInt(nameof(totalGoldCount), 0);
        totalToolUsage = PlayerPrefs.GetInt(nameof(totalToolUsage), 0);
        totalCompletedRuns = PlayerPrefs.GetInt(nameof(totalCompletedRuns), 0);

        // Save data
        coins = PlayerPrefs.GetInt(nameof(coins), 0);

        for (int i = 0; i < ToolCount; i++)
        {
            toolUnlocked[i] = PlayerPrefs.GetInt($"ToolUnlocked{i}", 0) == 1;
            toolEquipped[i] = PlayerPrefs.GetInt($"ToolEquipped{i}", 0) == 1;
        }

        statsScreenAppeared = PlayerPrefs.GetInt(nameof(statsScreenAppeared), 0) == 1;
        LoadShopPanels();
    }

    public void LoadShopPanels()
    {
        ShopPanel[] shopPanels = FindObjectsByType<ShopPanel>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        for (int i = 0; i < toolEquipped.Length; i++)
        {
            foreach (ShopPanel panel in shopPanels)
            {
                if (panel.index == i)
                {
                    panel.LoadValues();
                    break;
                }
            }
        }
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        timePlayed = 0f;

        enemiesKilled = 0;
        asteroidsDestroyed = 0;
        timesDied = 0;
        totalCoins = 0;
        totalCopperCount = 0;
        totalIronCount = 0;
        totalGoldCount = 0;
        totalToolUsage = 0;

        coins = 0;

        for (int i = 0; i < ToolCount; i++)
        {
            toolUnlocked[i] = false;
            toolEquipped[i] = false;
        }

        statsScreenAppeared = false;
    }

    // Extra functions to make stuff easier
    public bool IsToolUnlocked(int index) => toolUnlocked[index];
    public bool IsToolEquipped(int index) => toolEquipped[index];

    public void UnlockTool(int index) => toolUnlocked[index] = true;

    public void EquipTool(int index, bool equipped = true)
    {
        toolEquipped[index] = equipped;
    }
}