using UnityEngine;
using UnityEngine.UI;

public class LaneController : MonoBehaviour
{
    public string laneTag;
    public bool isStopped = false;
    public Image trafficLightImage;
    public Sprite redLightSprite;
    public Sprite greenLightSprite;

    private TimerManager timerManager;

    void Start()
    {
        timerManager = FindObjectOfType<TimerManager>();
        UpdateTrafficLight();
    }

    void OnMouseDown()
    {
        // Toggle stop/go
        isStopped = !isStopped;
        Debug.Log($"{gameObject.name} is now {(isStopped ? "STOPPED" : "MOVING")}");

        // Update all cars in this lane
        foreach (var v in FindObjectsOfType<Vehicle>())
        {
            if (v.laneTag == laneTag)
            {
                v.SetTrafficControlled(isStopped);
            }
        }

        // Update TimerManager
        if (timerManager)
        {
            bool anyStopped = false;
            foreach (var lane in FindObjectsOfType<LaneController>())
            {
                if (lane.isStopped) anyStopped = true;
            }
            timerManager.SetLaneStatus(anyStopped);
        }

        UpdateTrafficLight();
    }

    public void UpdateTrafficLight()
    {
        if (trafficLightImage != null)
        {
            trafficLightImage.sprite = isStopped ? redLightSprite : greenLightSprite;
        }
    }
}
