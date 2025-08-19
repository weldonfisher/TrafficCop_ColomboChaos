using UnityEngine;
using System.Linq;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject[] vehiclePrefabs;
    public Transform[] spawnPoints;
    public Transform[] stopPoints;
    public string[] laneTags;

    public float spawnRate = 2f;

    void Start()
    {
        InvokeRepeating("SpawnVehicle", 1f, spawnRate);
    }

    void SpawnVehicle()
    {
        int rand = Random.Range(0, spawnPoints.Length);

        // Check if lane is stopped
        LaneController lane = FindObjectsOfType<LaneController>()
            .FirstOrDefault(l => l.laneTag == laneTags[rand]);

        if (lane != null && lane.isStopped)
            return; // don't spawn if lane is stopped

        GameObject car = Instantiate(
            vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length)],
            spawnPoints[rand].position,
            spawnPoints[rand].rotation
        );

        Vehicle v = car.GetComponent<Vehicle>();
        v.laneTag = laneTags[rand];
        v.stopPoint = stopPoints[rand];

        car.tag = "Vehicle";
    }
}
