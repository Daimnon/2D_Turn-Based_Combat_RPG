using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Character, ICharacter, IAlly
{
    private int _partyIndex = 0;
    public int PartyIndex { get => _partyIndex; set => _partyIndex = value; }
}
