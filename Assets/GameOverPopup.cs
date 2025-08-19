using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverPopup : MonoBehaviour
{
    public GameObject popupPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI reasonText;
    public Button restartButton;
    public Button menuButton;

    void Start()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false); // hidden at start

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (menuButton != null)
            menuButton.onClick.AddListener(BackToMenu);
    }

    public void ShowGameOver(int score, string reason)
    {
        if (popupPanel == null)
        {
            Debug.LogError("❌ GameOverPopup is missing popupPanel in Inspector!");
            return;
        }

        popupPanel.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = "Score: " + score;

        if (bestScoreText != null)
        {
            int best = PlayerPrefs.GetInt("BestScore", 0);
            if (score > best)
            {
                PlayerPrefs.SetInt("BestScore", score);
                best = score;
            }
            bestScoreText.text = "Best: " + best;
        }

        if (reasonText != null)
            reasonText.text = reason;

        Time.timeScale = 0f; // pause game
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        if (LevelManager.Instance != null)
            LevelManager.Instance.LoadLevel(LevelManager.Instance.GetCurrentLevel());
        else
            SceneManager.LoadScene("Game");
    }

    void BackToMenu()
    {
        Time.timeScale = 1f;
        if (LevelManager.Instance != null)
            LevelManager.Instance.BackToMenu();
        else
            SceneManager.LoadScene("MainMenu");
    }
}
