using UnityEngine;

public class EndlessGround : MonoBehaviour
{
    [Header("Ground Settings")]
    public float groundLength = 20f; 
    public Transform player;

    private float triggerZ;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        triggerZ = groundLength * 2; 
    }

    void Update()
    {
        if (player.position.z > transform.position.z + triggerZ)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z + groundLength * 3
            );
        }
    }
}