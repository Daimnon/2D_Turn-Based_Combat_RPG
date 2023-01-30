using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    private static PartyManager _instance;
    public static PartyManager Instance => _instance;

    // serialization only for debug purposes
    [SerializeField] private List<Character> _playerParty;
    [SerializeField] private List<Enemy> _enemyParty;
    [SerializeField] private List<Ally> _potentialAllies;
    [SerializeField] private Ally[] _chosenAllies;

    public List<Character> PlayerParty { get => _playerParty; set => _playerParty = value; }
    public List<Enemy> EnemyParty { get => _enemyParty; set => _enemyParty = value; }
    public List<Ally> PotentialAllies { get => _potentialAllies; set => _potentialAllies = value; }
    public Ally[] ChosenAllies { get => _chosenAllies; set => _chosenAllies = value; }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        Initialize();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStartGame -= OnStartGame;
        GameManager.Instance.OnEndCombat -= OnEndGame;
    }
    private void Initialize()
    {
        _playerParty = new List<Character>(3) { GameManager.Instance.PlayerCharacter, null, null };
        _enemyParty = new List<Enemy>(3) { null, null, null };
        _potentialAllies = new List<Ally>();
        //_chosenAllies = new Ally[2];
        GameManager.Instance.OnStartGame += OnStartGame;
        GameManager.Instance.OnEndCombat += OnEndGame;
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

    }
    public void OnStartGame()
    {
        Debug.Log($"Party Should Initialize Here");

        for (int i = 1; i < _playerParty.Count; i++)
        {
            _playerParty[i] = _chosenAllies[i - 1];
            Debug.Log($"{_playerParty[i]} should be {_chosenAllies[i - 1]}");
        }
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
