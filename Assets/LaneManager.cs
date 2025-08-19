using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public static LaneManager Instance;

    private Dictionary<string, bool> laneStates = new Dictionary<string, bool>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SetLaneState(string laneTag, bool isStopped)
    {
        laneStates[laneTag] = isStopped;
    }

    public bool IsLaneStopped(string laneTag)
    {
        return laneStates.ContainsKey(laneTag) && laneStates[laneTag];
    }
}
