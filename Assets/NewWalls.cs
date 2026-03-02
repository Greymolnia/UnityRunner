using UnityEngine;
using System.Collections.Generic;

public class EndlessWall : MonoBehaviour
{
    [Header("Settings")]
    public GameObject wallPrefab;
    public int wallCount = 3;
    public float wallLength = 100f;
    public float spawnAheadDistance = 30f;
    public Transform player;

    private List<GameObject> walls = new List<GameObject>();
    private float nextSpawnZ;
    private float startX;

    void Start()
    {
        if (wallPrefab == null)
        {
            Debug.LogError("Wall Prefab not assigned!");
            return;
        }

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        startX = transform.position.x;

        for (int i = 0; i < wallCount; i++)
        {
            Vector3 position = new Vector3(startX, 1f, i * wallLength);
            GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);
            wall.transform.parent = transform;
            walls.Add(wall);
            Debug.Log("Wall created at: " + position); 
        }

        nextSpawnZ = wallCount * wallLength;
        Debug.Log("Created " + walls.Count + " walls");
    }

    void Update()
    {
        if (player == null || walls.Count == 0) return;

        foreach (GameObject wall in walls)
        {
            if (wall.transform.position.z + wallLength < player.position.z - spawnAheadDistance)
            {
                wall.transform.position = new Vector3(
                    startX,
                    wall.transform.position.y,
                    nextSpawnZ
                );
                nextSpawnZ += wallLength;
                Debug.Log("Wall moved to: " + wall.transform.position.z);
            }
        }
    }
}