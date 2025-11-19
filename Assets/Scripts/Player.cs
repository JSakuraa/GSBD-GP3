using UnityEngine;
using MusicDefinitions;
using BattleFefinitions;



public class Player : MonoBehaviour
{
    public double health = 100;
    public PlayerEffect[] effects;
    public Player enemy;

    public Monster monster;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class Monster
{
    public string name;
    public SpecialMelody[] combos;
}

public class Unseen : Monster
{
    public Unseen()
    {
        name = "the Unseen";
        combos = new SpecialMelody[] { new Execute() };
    }
}
