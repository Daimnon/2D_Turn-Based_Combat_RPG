using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Town, Merchant, Combat, Map}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private GameObject _playerPrefab;
    private Player _playerCharacter;

    [SerializeField] private GameState _gameState;
    public GameState GameState { get => _gameState; set => _gameState = value; }

    public Player PlayerCharacter => _playerCharacter;

    private void Awake()
    {
        _instance = this;
        _playerCharacter = _playerPrefab.GetComponent<Player>();
        CombatManager.Instance.OnStartCombat += OnStartCombat;
        CombatManager.Instance.OnEndCombat += OnEndCombat;

        DontDestroyOnLoad(this);
    }
    private void OnDestroy()
    {
        CombatManager.Instance.OnStartCombat -= OnStartCombat;
        CombatManager.Instance.OnEndCombat -= OnEndCombat;
    }

    public void OnStartCombat()
    {
        //CombatManager.Instance.Initialize(0);
    }
    public void OnEndCombat()
    {
        //CombatManager.Instance.Initialize(0);
    }
}
