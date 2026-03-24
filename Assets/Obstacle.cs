using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Obstacle Data")]
    [SerializeField] private ObstacleData obstacleData;

    [Header("Runtime References")]
    private Transform player;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    void Start()
    {
        if (obstacleData == null)
        {
            Debug.LogError($"Obstacle {gameObject.name} has no ObstacleData assigned!");
            enabled = false;
            return;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        SetupVisuals();

        transform.localScale = Vector3.one * obstacleData.scale;
    }

    void SetupVisuals()
    {
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
            meshFilter = gameObject.AddComponent<MeshFilter>();

        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
            meshRenderer = gameObject.AddComponent<MeshRenderer>();

        if (meshFilter.sharedMesh == null)
        {
            meshFilter.mesh = CreateCubeMesh();
        }

        Material material = new Material(Shader.Find("Standard"));

        material.color = obstacleData.obstacleColor;

        material.SetColor("_Color", obstacleData.obstacleColor);
        meshRenderer.material = material;
    }

    Mesh CreateCubeMesh()
    {
        GameObject tempCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Mesh cubeMesh = tempCube.GetComponent<MeshFilter>().sharedMesh;
        DestroyImmediate(tempCube);
        return cubeMesh;
    }

    void Update()
    {
        if (player == null || obstacleData == null) return;

        if (transform.position.z < player.position.z - obstacleData.destroyAfterDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == null || obstacleData == null) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player hit by {obstacleData.obstacleName}! Damage: {obstacleData.damage}");

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(obstacleData.damage);
            }

            Destroy(gameObject);
        }
    }

    public void SetData(ObstacleData data)
    {
        obstacleData = data;
        if (gameObject.activeInHierarchy)
        {
            SetupVisuals();
            transform.localScale = Vector3.one * obstacleData.scale;
        }
    }
}