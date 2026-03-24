using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI Settings")]
    public int fontSize = 40;
    public Color textColor = Color.white;
    public Vector2 textPosition = new Vector2(20, 80);

    [Header("Score Settings")]
    public int scorePerDistance = 1;

    private int currentScore = 0;
    private int bestScore = 0;
    private float lastPositionZ;
    private bool isGameRunning = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UnityEngine.Debug.Log($"Loaded best score: {bestScore}");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            lastPositionZ = player.transform.position.z;
        }

        ResetScore();
    }

    void Update()
    {
        if (!isGameRunning) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float currentZ = player.transform.position.z;
            float distanceTraveled = currentZ - lastPositionZ;

            if (distanceTraveled > 0)
            {
                int pointsToAdd = Mathf.CeilToInt(distanceTraveled * scorePerDistance);
                if (pointsToAdd > 0)
                {
                    AddScore(pointsToAdd);
                }
            }

            lastPositionZ = currentZ;
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        UnityEngine.Debug.Log("Score reset to 0");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            lastPositionZ = player.transform.position.z;
        }
    }

    public void SetGameRunning(bool running)
    {
        isGameRunning = running;
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;
        style.normal.textColor = textColor;
        style.fontStyle = FontStyle.Bold;
        GUI.Label(new Rect(textPosition.x, textPosition.y, 300, 100), $"Score: {currentScore}", style);
    }
}