using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    private static PartyManager _instance;
    public static PartyManager Instance => _instance;

    private List<Character> _playerParty;
    private List<Enemy> _enemyParty;
    private List<Ally> _potentialAllies;
    private Ally[] _chosenAllies;

    public List<Character> PlayerParty { get => _playerParty; set => _playerParty = value; }
    public List<Enemy> EnemyParty { get => _enemyParty; set => _enemyParty = value; }

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
        GameManager.Instance.OnStartGame -= OnStartCombat;
        GameManager.Instance.OnEndCombat -= OnEndCombat;
    }
    public void Initialize()
    {
        _playerParty = new List<Character>(3) { GameManager.Instance.PlayerCharacter, null, null };
        _enemyParty = new List<Enemy>(3) { null, null, null };
        _potentialAllies = new List<Ally>();
        _chosenAllies = new Ally[2];
        GameManager.Instance.OnStartCombat += OnStartCombat;
        GameManager.Instance.OnEndCombat += OnEndCombat;
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
    public void OnStartCombat()
    {
        Debug.Log($"Party Should Initialize Here");
    }
    public void OnEndCombat()
    {
        Debug.Log($"Party survived combat");
    }
}
