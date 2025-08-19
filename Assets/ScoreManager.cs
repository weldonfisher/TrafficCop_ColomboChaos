using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float score;
    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime;
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }
    }

    public void StopScoring()
    {
        isGameOver = true;

        int finalScore = Mathf.FloorToInt(score);
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (finalScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", finalScore);
        }
    }
}
