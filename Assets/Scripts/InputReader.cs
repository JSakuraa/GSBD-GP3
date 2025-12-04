using BattleDefinitions;
using MusicDefinitions;
using UnityEngine;
using TMPro; // For standard UI InputField
// using TMPro; // For TextMeshPro InputField

public class InputReader : MonoBehaviour
{
    public TMP_InputField melody1;
    public TMP_InputField melody2; // Drag your InputField here in the Inspector
    // OR
    // public TMP_InputField inputField; // If using TextMeshPro

    public TMPro.TMP_Text displayText; // Optional: To display the read text
    // OR
    // public TMPro.TMP_Text displayText; // If using TextMeshPro
    public Battlestate bs;
    public void OnButtonClick()
    {
        Action a1 = new Action(new Melody(Translations.notes_from_string(melody1.text)), bs.player1);
        Action a2 = new Action(new Melody(Translations.notes_from_string(melody2.text)), bs.player2);
        //Debug.Log("User entered: " + chord1_text);
        bs.battle(a1, a2);
        if (displayText != null)
        {
            displayText.text = $"P1 health {bs.player1.health} | P2 health {bs.player2.health}";
        }
    }
    void Start()
    {
        if (displayText != null)
        {
            displayText.text = $"P1 health {100} | P2 health {100}";
        }

    }
}
