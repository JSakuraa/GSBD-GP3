using UnityEngine;
using MusicDefinitions;
using BattleFefinitions;

public class MyContextMenuScript : MonoBehaviour
{
    [ContextMenu("Run test code")]
    void PerformEditorAction()
    {
        Debug.Log("Running test code");
        Action a1 = new Action(new Chord(0, 1, 2), new Melody(0, 1, 2, 3));
        print(a1);
        // Add your editor-specific logic here
    }
}
