using UnityEngine;
using MusicDefinitions;
using BattleFefinitions;

public class Runner : MonoBehaviour
{
    [ContextMenu("Run test code")]
    void quickrun()
    {
        Debug.Log("Running test code");
        Action a1 = new Action(new Chord(0, 1, 2), new Melody(0, 1, 2, 3));
        Action a2 = new Action(new Chord(3, 4, 5), new Melody(3, 4, 5, 6));
        Player p1 = new Killer("Alice");
        Player p2 = new Killer("Alex");
        p1.enemy = p2;
        p2.enemy = p1;
        a1.player = p1;
        a2.player = p2;
        Battlestate.battle(a1, a2);
        Debug.Log(p1.info());
        Debug.Log(p2.info());
    }
}
