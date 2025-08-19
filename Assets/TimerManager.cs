using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float timeRemaining;
    private bool running = true;

    void Start()
    {
        float dur = 30f; // default duration
        if (LevelManager.Instance != null)
            dur = LevelManager.Instance.GetLevelDurationSeconds();

        timeRemaining = dur;
        UpdateUI();
    }

    void Update()
    {
        if (!running) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            running = false;
            HandleWin();
        }

        UpdateUI();
    }

    // ✅ Restored so LaneController can still call this
    public void SetLaneStatus(bool anyLaneStopped)
    {
        // Old logic was to start/stop the timer when a lane is stopped
        // But now timer always counts down. We just keep this as a stub to avoid errors.
        Debug.Log("SetLaneStatus called - currently unused.");
    }

    void HandleWin()
    {
        Debug.Log("✅ HandleWin called, showing LevelCompletePopup...");

        // Stop scoring
        var score = FindObjectOfType<ScoreManager>();
        if (score) score.StopScoring();

        // Unlock next level
        if (LevelManager.Instance != null)
            LevelManager.Instance.LevelCompleted();

        // Show the new popup
        var completePopup = FindObjectOfType<LevelCompletePopup>();
        if (completePopup)
        {
            int finalScore = Mathf.FloorToInt(score ? score.score : 0f);
            completePopup.ShowLevelComplete(finalScore);
        }
        else
        {
            Debug.LogError("❌ No LevelCompletePopup found in scene!");
        }

        Time.timeScale = 0f;
    }

    void UpdateUI()
    {
        if (timerText == null) return;

        int secs = Mathf.CeilToInt(timeRemaining);
        int m = secs / 60;
        int s = secs % 60;
        timerText.text = $"{m:00}:{s:00}";
    }
}
