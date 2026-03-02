using UnityEngine;
using System.Collections.Generic;



public class ObstacleGenerator : MonoBehaviour
{
    [Header("Generation Settings")]
    public List<ObstacleData> obstacleTypes;
    public float minSpawnDistance = 10f;
    public float maxSpawnDistance = 20f;
    public float spawnOffsetZ = 30f;

    [Header("Lane Settings")]
    public float[] lanes = { -2f, 0f, 2f };

    private Transform player;
    private float nextSpawnZ;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            nextSpawnZ = player.position.z + spawnOffsetZ;
        }
    }

    void Update()
    {
        if (player == null) return;

        while (nextSpawnZ < player.position.z + spawnOffsetZ)
        {
            SpawnObstacle();
            nextSpawnZ += Random.Range(minSpawnDistance, maxSpawnDistance);
        }
    }

    void SpawnObstacle()
    {
        if (obstacleTypes == null || obstacleTypes.Count == 0) return;

        ObstacleData selectedData = obstacleTypes[Random.Range(0, obstacleTypes.Count)];
        float laneX = lanes[Random.Range(0, lanes.Length)];

        GameObject obstacleObj = new GameObject($"Obstacle_{selectedData.name}");
        obstacleObj.transform.position = new Vector3(laneX, 0.5f, nextSpawnZ);
        Obstacle obstacle = obstacleObj.AddComponent<Obstacle>();
        obstacle.SetData(selectedData);

        BoxCollider collider = obstacleObj.AddComponent<BoxCollider>();
        collider.isTrigger = true;
    }
}