using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelCompletePopup : MonoBehaviour
{
    public GameObject popupPanel;
    public TextMeshProUGUI scoreText;
    public Button nextButton;
    public Button menuButton;

    void Start()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false); // hidden at start

        if (nextButton != null)
            nextButton.onClick.AddListener(NextLevel);

        if (menuButton != null)
            menuButton.onClick.AddListener(ReturnToMenu);
    }

    public void ShowLevelComplete(int score)
    {
        if (popupPanel == null)
        {
            Debug.LogError("❌ LevelCompletePopup is missing popupPanel in Inspector!");
            return;
        }

        popupPanel.SetActive(true);

        if (scoreText != null)
            scoreText.text = "Score: " + score;

        Time.timeScale = 0f; // pause game
    }

    void NextLevel()
    {
        Time.timeScale = 1f;
        if (LevelManager.Instance != null)
            LevelManager.Instance.LoadNextLevel();
        else
            SceneManager.LoadScene("Game"); // fallback
    }

    void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
