using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject popupPanel;

    void Start()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (popupPanel != null)
            popupPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }
}