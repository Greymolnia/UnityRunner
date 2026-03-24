using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("PowerUp Data")]
    public PowerUpData data;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUp(other.gameObject);
            Destroy(gameObject);
        }
    }

    void ApplyPowerUp(GameObject player)
    {
        PlayerPowerUpHandler handler = player.GetComponent<PlayerPowerUpHandler>();
        if (handler != null)
        {
            handler.ApplyPowerUp(data);
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(data.scoreBonus);
            }
        }
        
    }
}