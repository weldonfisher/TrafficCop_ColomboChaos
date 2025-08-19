using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float moveSpeed = 2f;
    public string laneTag;
    public Transform stopPoint;

    private bool isStopped = false;
    private bool isControlledByLane = false;

    void Update()
    {
        if (!isStopped)
        {
            if (isControlledByLane && stopPoint)
            {
                float distance = Vector2.Distance(transform.position, stopPoint.position);
                if (distance > 0.5f)
                {
                    transform.position = Vector2.MoveTowards(
                        transform.position,
                        transform.position + transform.up,
                        moveSpeed * Time.deltaTime
                    );
                }
            }
            else
            {
                transform.position += transform.up * moveSpeed * Time.deltaTime;
            }
        }
    }

    public void SetTrafficControlled(bool stop)
    {
        isControlledByLane = stop;
        isStopped = stop;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vehicle other = collision.gameObject.GetComponent<Vehicle>();

        // Only game over if collided with another vehicle from a different lane
        if (other != null && other.laneTag != this.laneTag)
        {
            Debug.Log("GAME OVER - Cross lane collision!");

            // stop scoring
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager)
                scoreManager.StopScoring();

            // show popup with reason
            GameOverPopup popup = FindObjectOfType<GameOverPopup>();
            if (popup && scoreManager)
            {
                int finalScore = Mathf.FloorToInt(scoreManager.score);
                popup.ShowGameOver(finalScore, "Cross Lane Collision!");
            }

            // freeze game
            Time.timeScale = 0;
        }
    }
}
