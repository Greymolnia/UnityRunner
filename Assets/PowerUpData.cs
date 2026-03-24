using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUp", menuName = "Runner/PowerUp")]
public class PowerUpData : ScriptableObject
{
    public string powerUpName;
    public PowerUpType type;
    public float duration = 0f; 
    public int healthRestore = 0;
    public float speedMultiplier = 1f;
    public int scoreBonus = 50;

    public enum PowerUpType
    {
        Health,
        SpeedBoost
    }
}