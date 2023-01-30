using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<Character> PlayerParty;
    public List<Enemy> EnemyParty;
    public List<Ally> PotentialAllies;
    public Ally[] ChosenAllies;

    public SaveData()
    {
        PlayerParty = new();
        EnemyParty = new();
        PotentialAllies = new();
        ChosenAllies = new Ally[2];
    }
}
