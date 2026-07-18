using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    public int completedRuns;

    public float timeInGame;

    public int enemiesKilled;
    public int asteroidsDestroyed;
    public int timesDied;
    public int totalCoins;
    public int totalCopperCount;
    public int totalIronCount;
    public int totalGoldCount;
    public int totalToolUsage;

    public float allTimeInGame;

    public int allEnemiesKilled;
    public int allAsteroidsDestroyed;
    public int allTimesDied;
    public int allTotalCoins;
    public int allTotalCopperCount;
    public int allTotalIronCount;
    public int allTotalGoldCount;
    public int allTotalToolUsage;

    public int coins;

    public const int ToolCount = 8;

    public bool[] toolUnlocked = new bool[ToolCount];
    public bool[] toolEquipped = new bool[ToolCount];

    public bool statsScreenAppeared;

    public TMP_Text completedRunsTxt;

    [Header("Stats Screen Text Fields for This Run")]
    public TMP_Text hoursPlayedThisRun;
    public TMP_Text coinsCollectedThisRun;
    public TMP_Text enemiesKilledThisRun;
    public TMP_Text toolsUsedThisRun;
    public TMP_Text asteroidsDestroyedThisRun;
    public TMP_Text oresCollectedThisRun;
    public TMP_Text timesDiedThisRun;

    [Header("Stats Screen Text Fields for All Runs")]
    public TMP_Text hoursPlayedAllRuns;
    public TMP_Text coinsCollectedAllRuns;
    public TMP_Text enemiesKilledAllRuns;
    public TMP_Text toolsUsedAllRuns;
    public TMP_Text asteroidsDestroyedAllRuns;
    public TMP_Text oresCollectedAllRuns;
    public TMP_Text timesDiedAllRuns;

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
        int run = GameManager.Instance.runsCompleted + 1;
        string runId = $"_Run{run}";

        PlayerPrefs.SetInt(nameof(completedRuns), completedRuns);

        PlayerPrefs.SetFloat(nameof(timeInGame) + runId, timeInGame);

        PlayerPrefs.SetInt(nameof(enemiesKilled) + runId, enemiesKilled);
        PlayerPrefs.SetInt(nameof(asteroidsDestroyed) + runId, asteroidsDestroyed);
        PlayerPrefs.SetInt(nameof(timesDied) + runId, timesDied);
        PlayerPrefs.SetInt(nameof(totalCoins) + runId, totalCoins);
        PlayerPrefs.SetInt(nameof(totalCopperCount) + runId, totalCopperCount);
        PlayerPrefs.SetInt(nameof(totalIronCount) + runId, totalIronCount);
        PlayerPrefs.SetInt(nameof(totalGoldCount) + runId, totalGoldCount);
        PlayerPrefs.SetInt(nameof(totalToolUsage) + runId, totalToolUsage);

        PlayerPrefs.SetFloat(nameof(allTimeInGame), allTimeInGame);

        PlayerPrefs.SetInt(nameof(allEnemiesKilled), allEnemiesKilled);
        PlayerPrefs.SetInt(nameof(allAsteroidsDestroyed), allAsteroidsDestroyed);
        PlayerPrefs.SetInt(nameof(allTimesDied), allTimesDied);
        PlayerPrefs.SetInt(nameof(allTotalCoins), allTotalCoins);
        PlayerPrefs.SetInt(nameof(allTotalCopperCount), allTotalCopperCount);
        PlayerPrefs.SetInt(nameof(allTotalIronCount), allTotalIronCount);
        PlayerPrefs.SetInt(nameof(allTotalGoldCount), allTotalGoldCount);
        PlayerPrefs.SetInt(nameof(allTotalToolUsage), allTotalToolUsage);

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
        if (!PlayerPrefs.HasKey("Initialized"))
        {
            InitializeNewRun();

            PlayerPrefs.SetInt("Initialized", 1);
            Save();
            return;
        }
        completedRuns = PlayerPrefs.GetInt(nameof(completedRuns), 0);

        allTimeInGame = PlayerPrefs.GetFloat(nameof(allTimeInGame), 0f);

        allEnemiesKilled = PlayerPrefs.GetInt(nameof(allEnemiesKilled), 0);
        allAsteroidsDestroyed = PlayerPrefs.GetInt(nameof(allAsteroidsDestroyed), 0);
        allTimesDied = PlayerPrefs.GetInt(nameof(allTimesDied), 0);
        allTotalCoins = PlayerPrefs.GetInt(nameof(allTotalCoins), 0);
        allTotalCopperCount = PlayerPrefs.GetInt(nameof(allTotalCopperCount), 0);
        allTotalIronCount = PlayerPrefs.GetInt(nameof(allTotalIronCount), 0);
        allTotalGoldCount = PlayerPrefs.GetInt(nameof(allTotalGoldCount), 0);
        allTotalToolUsage = PlayerPrefs.GetInt(nameof(allTotalToolUsage), 0);

        coins = PlayerPrefs.GetInt(nameof(coins), 0);

        for (int i = 0; i < ToolCount; i++)
        {
            toolUnlocked[i] = PlayerPrefs.GetInt($"ToolUnlocked{i}", 0) == 1;
            toolEquipped[i] = PlayerPrefs.GetInt($"ToolEquipped{i}", 0) == 1;
        }

        statsScreenAppeared = PlayerPrefs.GetInt(nameof(statsScreenAppeared), 0) == 1;

        LoadShopPanels();

        if (statsScreenAppeared == true)
        {
            GameManager.Instance.ShowEndScreen();
        }
    }

    public void UpdateThisRunFields()
    {
        hoursPlayedThisRun.text = FormatHours(timeInGame);
        coinsCollectedThisRun.text = totalCoins.ToString();
        enemiesKilledThisRun.text = enemiesKilled.ToString();
        toolsUsedThisRun.text = totalToolUsage.ToString();
        asteroidsDestroyedThisRun.text = asteroidsDestroyed.ToString();
        oresCollectedThisRun.text = (totalCopperCount + totalIronCount + totalGoldCount).ToString();
        timesDiedThisRun.text = timesDied.ToString();
    }

    public void UpdateAllRunsFields()
    {
        hoursPlayedAllRuns.text = FormatHours(allTimeInGame);
        coinsCollectedAllRuns.text = allTotalCoins.ToString();
        enemiesKilledAllRuns.text = allEnemiesKilled.ToString();
        toolsUsedAllRuns.text = allTotalToolUsage.ToString();
        asteroidsDestroyedAllRuns.text = allAsteroidsDestroyed.ToString();
        oresCollectedAllRuns.text = (allTotalCopperCount + allTotalIronCount + allTotalGoldCount).ToString();
        timesDiedAllRuns.text = allTimesDied.ToString();
        completedRunsTxt.text = completedRuns.ToString();
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

    public void ResetThisRunFields()
    {
        timeInGame = 0f;

        enemiesKilled = 0;
        asteroidsDestroyed = 0;
        timesDied = 0;
        totalCoins = 0;
        totalCopperCount = 0;
        totalIronCount = 0;
        totalGoldCount = 0;
        totalToolUsage = 0;

        statsScreenAppeared = false;

        PlayerPrefs.DeleteKey("TimeInGame");
        PlayerPrefs.DeleteKey("EnemiesKilled");
        PlayerPrefs.DeleteKey("AsteroidsDestroyed");
        PlayerPrefs.DeleteKey("TimesDied");
        PlayerPrefs.DeleteKey("TotalCoins");
        PlayerPrefs.DeleteKey("TotalCopperCount");
        PlayerPrefs.DeleteKey("TotalIronCount");
        PlayerPrefs.DeleteKey("TotalGoldCount");
        PlayerPrefs.DeleteKey("TotalToolUsage");
        PlayerPrefs.Save();
    }

    public void ResetEverything()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        timeInGame = 0f;

        enemiesKilled = 0;
        asteroidsDestroyed = 0;
        timesDied = 0;
        totalCoins = 0;
        totalCopperCount = 0;
        totalIronCount = 0;
        totalGoldCount = 0;
        totalToolUsage = 0;

        allTimeInGame = 0f;

        allEnemiesKilled = 0;
        allAsteroidsDestroyed = 0;
        allTimesDied = 0;
        allTotalCoins = 0;
        allTotalCopperCount = 0;
        allTotalIronCount = 0;
        allTotalGoldCount = 0;
        allTotalToolUsage = 0;

        coins = 0;

        for (int i = 0; i < ToolCount; i++)
        {
            toolUnlocked[i] = false;
            toolEquipped[i] = false;
        }

        statsScreenAppeared = false;
    }

    public bool IsToolUnlocked(int index) => toolUnlocked[index];

    public bool IsToolEquipped(int index) => toolEquipped[index];

    public void UnlockTool(int index)
    {
        toolUnlocked[index] = true;
    }

    public void EquipTool(int index, bool equipped = true)
    {
        toolEquipped[index] = equipped;
    }

    public string FormatHours(float seconds)
    {
        int hours = Mathf.FloorToInt(seconds / 3600);
        int minutes = Mathf.FloorToInt((seconds % 3600) / 60);

        return $"{hours}h {minutes}m";
    }

    public void InitializeNewRun()
    {
        for (int i = 0; i < ToolCount; i++)
        {
            toolUnlocked[i] = false;
            toolEquipped[i] = false;
        }

        toolUnlocked[0] = true;
        toolEquipped[0] = true;
    }
}