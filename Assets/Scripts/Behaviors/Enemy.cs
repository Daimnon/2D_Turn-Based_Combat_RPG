using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, ICharacter, IEnemy
{
    private int _partyIndex = 0;
    public int PartyIndex { get => _partyIndex; set => _partyIndex = value; }

    public override string ToString()
    {
        return Data.Name;
    }
}
