using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public CharacterData Player;
    public CharacterData[] ChosenAllies;
    public List<CharacterData> PotentialAllies;

    public SaveData()
    {

    }
}
