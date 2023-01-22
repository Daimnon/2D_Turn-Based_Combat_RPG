using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    private static PartyManager _instance;
    public static PartyManager Instance => _instance;

    private List<Character> _playerParty, _enemyParty, _potentialAllies;
    private Ally[] _chosenAllies;

    public List<Character> PlayerParty { get => _playerParty; set => _ = value; }
    public List<Character> EnemyParty { get => _enemyParty; set => _ = value; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void Initialize()
    {
        _instance = this;
        _playerParty = new List<Character>(3) { GameManager.Instance.PlayerCharacter, null, null };
        _enemyParty = new List<Character>(3) { null, null, null };
        _potentialAllies = new List<Character>();
        _chosenAllies = new Ally[2];
    }
    public void AddNewAlly(Character newAlly)
    {
        _potentialAllies.Add(newAlly);
    }
    public void ChooseAlly(Ally ally)
    {
        if (_potentialAllies.Contains(ally) && !_chosenAllies[0])
            _chosenAllies[0] = ally;
        else if (_potentialAllies.Contains(ally) && !_chosenAllies[1])
            _chosenAllies[1] = ally;
        else
            Debug.Log("Chosen allies are full, remove ally to proceed");
    }
    public void SetEnemyParty(Stage stage)
    {

    }
}
