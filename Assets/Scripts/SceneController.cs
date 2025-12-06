using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameSceneV3");
    }

    public void LoadGameOverScreen()
    {
        SceneManager.LoadScene("GameOverScreen");
    }

    public void LoadCutscene()
    {
        SceneManager.LoadScene("Cutscene");
    }

    public void LoadP1Wins()
    {
        SceneManager.LoadScene("P1Wins");
    }

    public void LoadP2Wins()
    {
        SceneManager.LoadScene("P2Wins");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

        Debug.Log("Game is quitting...");
    }
}