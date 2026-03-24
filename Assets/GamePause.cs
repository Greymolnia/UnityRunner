using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; 
        SceneManager.LoadScene(2, LoadSceneMode.Additive); 
    }


    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync(2); 
    }


    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); 
    }
}