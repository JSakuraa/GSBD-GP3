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
        SceneManager.LoadScene("NewGameScene");
    }

    public void LoadGameOverScreen()
    {
        SceneManager.LoadScene("GameOverScreen");
    }
    public void debugprint(string s)
    {
        print(s);
    }
}