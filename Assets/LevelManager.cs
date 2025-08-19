using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int currentLevel = 1;   // made public so other scripts can read
    public const int totalLevels = 8;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Duration per level
    public float GetLevelDurationSeconds()
    {
        switch (currentLevel)
        {
            case 1: return 30f;
            case 2: return 60f;
            case 3: return 90f;
            default: return 120f;
        }
    }

    // Called when player completes a level
    public void LevelCompleted()
    {
        int unlocked = PlayerPrefs.GetInt("LevelUnlocked", 1);
        if (currentLevel + 1 > unlocked)
        {
            PlayerPrefs.SetInt("LevelUnlocked", currentLevel + 1);
            PlayerPrefs.Save();
        }
    }

    // 🔹 NEW: Check if a level is unlocked
    public bool IsLevelUnlocked(int levelNumber)
    {
        return levelNumber <= PlayerPrefs.GetInt("LevelUnlocked", 1);
    }

    // 🔹 Load the next level
    public void LoadNextLevel()
    {
        if (currentLevel < totalLevels)
        {
            currentLevel++;
            LoadLevel(currentLevel);
        }
        else
        {
            Debug.Log("🎉 All levels completed!");
            BackToMenu();
        }
    }

    // 🔹 Load a specific level
    public void LoadLevel(int levelNumber)
    {
        currentLevel = levelNumber;
        SceneManager.LoadScene("Game"); // all gameplay handled in "Game" scene
    }

    // 🔹 Back to Main Menu
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // 🔹 Helpers
    public int GetCurrentLevel() => currentLevel;
    public int GetUnlockedLevel() => PlayerPrefs.GetInt("LevelUnlocked", 1);
}
