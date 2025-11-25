using UnityEngine;
using MusicDefinitions;
using BattleDefinitions;

public class Runner : MonoBehaviour
{
    public Battlestate bs;
    [ContextMenu("Run test code")]


    void quickrun()
    {
        bs.init();
        Debug.Log("Running test code");
        Action a1 = new Action(new Chord(0, 1, 3), new Melody(3, 3, 3, 3));
        Action a2 = new Action(new Chord(0, 1, 3), new Melody(6, 6, 6, 6));
        Player p1 = bs.player1;
        Player p2 = bs.player2;
        p1.enemy = p2;
        p2.enemy = p1;
        a1.player = p1;
        a2.player = p2;
        bs.battle(a1, a2);
        print(bs.last_update);
    }
}
