using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("PowerUp Prefabs")]
    public GameObject healthPowerUpPrefab;
    public GameObject speedPowerUpPrefab;

    [Header("Spawn Settings")]
    public float spawnChance = 0.3f;
    public float maxObstacleSize = 2f; 

    public void TrySpawnPowerUp(Vector3 obstaclePosition, float obstacleSize)
    {
        if (obstacleSize > maxObstacleSize)
        {
            UnityEngine.Debug.Log($"Skipped: obstacle too big ({obstacleSize} > {maxObstacleSize})");
            return;
        }
        if (UnityEngine.Random.value > spawnChance)
            return;
        GameObject powerUpPrefab = ChoosePowerUpType();

        if (powerUpPrefab == null)
        {
            UnityEngine.Debug.LogError("PowerUp prefab is null!");
            return;
        }

        Vector3 spawnPos = obstaclePosition + Vector3.up * 1.2f;

        Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);

        UnityEngine.Debug.Log($"PowerUp spawned at {spawnPos} - Type: {powerUpPrefab.name}");
    }

    GameObject ChoosePowerUpType()
    {
        if (UnityEngine.Random.value < 0.5f)
        {
            return healthPowerUpPrefab;
        }
        else
        {
            return speedPowerUpPrefab;
        }
    }
}