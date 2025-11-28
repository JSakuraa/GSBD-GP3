using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPopup : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject howToPlayButton;
    public GameObject noteDetailsButton;

    [Header("Popup Elements")]
    public GameObject whiteBG;
    public GameObject howToPlayText;
    public GameObject noteDetailsText;

    void Start()
    {
        // Hide all popup elements initially
        if (whiteBG != null)
            whiteBG.SetActive(false);
        if (howToPlayText != null)
            howToPlayText.SetActive(false);
        if (noteDetailsText != null)
            noteDetailsText.SetActive(false);

        // Add event triggers to buttons
        SetupButtonHoverEvents(howToPlayButton, ShowHowToPlayPopup, HideAllPopups);
        SetupButtonHoverEvents(noteDetailsButton, ShowNoteDetailsPopup, HideAllPopups);
    }

    void SetupButtonHoverEvents(GameObject button, UnityEngine.Events.UnityAction onEnter, UnityEngine.Events.UnityAction onExit)
    {
        if (button == null) return;

        EventTrigger trigger = button.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.AddComponent<EventTrigger>();

        // Clear existing entries to avoid duplicates
        trigger.triggers.Clear();

        // Add PointerEnter event
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { onEnter(); });
        trigger.triggers.Add(entryEnter);

        // Add PointerExit event
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { onExit(); });
        trigger.triggers.Add(entryExit);
    }

    void ShowHowToPlayPopup()
    {
        if (whiteBG != null)
            whiteBG.SetActive(true);
        if (howToPlayText != null)
            howToPlayText.SetActive(true);
        if (noteDetailsText != null)
            noteDetailsText.SetActive(false);
    }

    void ShowNoteDetailsPopup()
    {
        if (whiteBG != null)
            whiteBG.SetActive(true);
        if (noteDetailsText != null)
            noteDetailsText.SetActive(true);
        if (howToPlayText != null)
            howToPlayText.SetActive(false);
    }

    void HideAllPopups()
    {
        if (whiteBG != null)
            whiteBG.SetActive(false);
        if (howToPlayText != null)
            howToPlayText.SetActive(false);
        if (noteDetailsText != null)
            noteDetailsText.SetActive(false);
    }
}