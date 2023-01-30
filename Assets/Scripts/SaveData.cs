using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    private List<Character> _playerParty;
    private List<Enemy> _enemyParty;
    private List<Ally> _potentialAllies;
    private Ally[] _chosenAllies;

    public List<Character> PlayerParty { get => _playerParty; set => _playerParty = value; }
    public List<Enemy> EnemyParty { get => _enemyParty; set => _enemyParty = value; }
    public List<Ally> PotentialAllies { get => _potentialAllies; set => _potentialAllies = value; }
    public Ally[] ChosenAllies { get => _chosenAllies; set => _chosenAllies = value; }

    public SaveData()
    {
        _playerParty = new();
        _enemyParty = new();
        _potentialAllies = new();
        _chosenAllies = new Ally[2];
    }
    //public SaveData(PartyManager partyManager)
    //{
    //    if (partyManager)
    //    {
    //        _playerParty = partyManager.PlayerParty;
    //        _enemyParty = partyManager.EnemyParty;
    //        _potentialAllies = partyManager.PotentialAllies;
    //        _chosenAllies = partyManager.ChosenAllies;
    //    }
    //}
    //
    //public void UpdateSaveData()
    //{
    //    _playerParty = PartyManager.Instance.PlayerParty;
    //    _enemyParty = PartyManager.Instance.EnemyParty;
    //    _potentialAllies = PartyManager.Instance.PotentialAllies;
    //    _chosenAllies = PartyManager.Instance.ChosenAllies;
    //}
}
