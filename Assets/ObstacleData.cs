using UnityEngine;

[CreateAssetMenu(fileName = "NewObstacleData", menuName = "Runner/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    [Header("Obstacle Characteristics")]
    public string obstacleName = "Basic Obstacle";
    public int damage = 1;
    public float scale = 1f;
    public Color obstacleColor = Color.red;

    [Header("Behavior Settings")]
    public float destroyAfterDistance = 30f;
}