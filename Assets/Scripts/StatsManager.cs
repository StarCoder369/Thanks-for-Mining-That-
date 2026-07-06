using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

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

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
            Save();
    }

    public void Save()
    {
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

        PlayerPrefs.Save();
    }

    public void Load()
    {
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
    }

    public void ResetStats()
    {
        PlayerPrefs.DeleteKey(nameof(timePlayed));
        PlayerPrefs.DeleteKey(nameof(timeInGame));

        PlayerPrefs.DeleteKey(nameof(enemiesKilled));
        PlayerPrefs.DeleteKey(nameof(asteroidsDestroyed));
        PlayerPrefs.DeleteKey(nameof(timesDied));
        PlayerPrefs.DeleteKey(nameof(totalCoins));
        PlayerPrefs.DeleteKey(nameof(totalCopperCount));
        PlayerPrefs.DeleteKey(nameof(totalIronCount));
        PlayerPrefs.DeleteKey(nameof(totalGoldCount));
        PlayerPrefs.DeleteKey(nameof(totalToolUsage));

        PlayerPrefs.Save();
        Load();
    }
}