using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialNavigator : MonoBehaviour
{
    [Header("Tutorial Images")]
    public GameObject img1;
    public GameObject img2;
    public GameObject img3;

    [Header("Navigation Buttons")]
    public Button prevButton;
    public Button nextButton;
    public Button toGameButton;

    private int currentIndex = 0;
    private GameObject[] images;

    void Start()
    {
        images = new GameObject[] { img1, img2, img3 };

        if (prevButton != null)
            prevButton.onClick.AddListener(ShowPrevious);

        if (nextButton != null)
            nextButton.onClick.AddListener(ShowNext);

        if (toGameButton != null)
            toGameButton.onClick.AddListener(LoadGameScene);

        ShowCurrentImage();
    }

    void ShowCurrentImage()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i] != null)
            {
                images[i].SetActive(i == currentIndex);
            }
        }

        if (prevButton != null)
            prevButton.interactable = (currentIndex > 0);
    }

    void ShowPrevious()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowCurrentImage();
        }
    }

    void ShowNext()
    {
        if (currentIndex < images.Length - 1)
        {
            currentIndex++;
            ShowCurrentImage();
        }
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("GameSceneV3");
    }
}
