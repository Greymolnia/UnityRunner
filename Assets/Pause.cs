using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GamePause gamePause;

    void Start()
    {
        gamePause = FindObjectOfType<GamePause>();
    }

    public void ResumeGame()
    {
        if (gamePause != null)
        {
            gamePause.ResumeGame();
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.UnloadSceneAsync("Pause");
        }
    }

    public void MainMenu()
    {
        if (gamePause != null)
        {
            gamePause.BackToMenu();
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }
}