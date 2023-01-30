using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    private static PartyManager _instance;
    public static PartyManager Instance => _instance;

    private List<Ally> _potentialAllies;
    private Character[] _playerParty;
    private Enemy[] _enemyParty;
    private Ally[] _chosenAllies;

    public List<Ally> PotentialAllies { get => _potentialAllies; set => _potentialAllies = value; }
    public Character[] PlayerParty { get => _playerParty; set => _playerParty = value; }
    public Enemy[] EnemyParty { get => _enemyParty; set => _enemyParty = value; }
    public Ally[] ChosenAllies { get => _chosenAllies; set => _chosenAllies = value; }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        Initialize();
        SubscribeToEvent();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvent();
    }
    private void Initialize()
    {
        _potentialAllies = new List<Ally>();
        _playerParty = new Character[3] { GameManager.Instance.PlayerCharacter, null, null };
        _enemyParty = new Enemy[3] { null, null, null };
        _chosenAllies = new Ally[2] { null , null };
    }
    public void AddNewAlly(Ally newAlly)
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
        Debug.Log($"Enemy Party Should Initialize Here");
    }

    public void SubscribeToEvent()
    {
        GameManager.Instance.OnStartGame += OnStartGame;
        GameManager.Instance.OnEndCombat += OnEndGame;
    }
    public void UnsubscribeFromEvent()
    {
        GameManager.Instance.OnStartGame -= OnStartGame;
        GameManager.Instance.OnEndCombat -= OnEndGame;
    }
    public void OnStartGame()
    {
        Debug.Log($"Player Party Should Initialize Here");
    }
    public void OnEndGame()
    {
        Debug.Log($"Party survived combat");
    }
    public void OnStartCombat()
    {
        Debug.Log($"Party Should Initialize Here");
    }
    public void OnEndCombat()
    {
        Debug.Log($"Party survived combat");
    }
}

/*
 * populate parties
 * create item
 * fill data
 * choose model by data
 */
