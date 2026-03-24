using UnityEngine;
using System.Collections;

public class PlayerPowerUpHandler : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Controller playerController; 
    private Coroutine speedBoostCoroutine;
    private float originalSpeed;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<Controller>(); 

        if (playerController != null)
        {
            originalSpeed = playerController.forwardSpeed; 
        }
    }

    public void ApplyPowerUp(PowerUpData data)
    {
        switch (data.type)
        {
            case PowerUpData.PowerUpType.Health:
                ApplyHealthBoost(data);
                break;
            case PowerUpData.PowerUpType.SpeedBoost:
                ApplySpeedBoost(data);
                break;
        }
    }

    void ApplyHealthBoost(PowerUpData data)
    {
        if (playerHealth != null)
        {
            playerHealth.Heal(data.healthRestore);
            UnityEngine.Debug.Log($"Health restored by {data.healthRestore}");
        }
    }

    void ApplySpeedBoost(PowerUpData data)
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
            playerController.forwardSpeed = originalSpeed;
        }

        speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(data));
        UnityEngine.Debug.Log($"Speed boost activated! Duration: {data.duration}s, Multiplier: {data.speedMultiplier}");
    }

    IEnumerator SpeedBoostCoroutine(PowerUpData data)
    {
        playerController.forwardSpeed = originalSpeed * data.speedMultiplier;
        yield return new WaitForSeconds(data.duration);
        playerController.forwardSpeed = originalSpeed;
        UnityEngine.Debug.Log("Speed boost ended");
    }
}