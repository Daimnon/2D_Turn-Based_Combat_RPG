using System;
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

    public event Action OnStartGame, OnStartCombat, OnEndCombat;

    private void Awake()
    {
        _instance = this;
        
        DontDestroyOnLoad(this);
    }

    public void InvokeStartGame() // occurs when entering combat.
    {
        if (OnStartGame != null)
        {
            OnStartGame.Invoke();
        }
    }
    public void InvokeStartCombat() // occurs when entering combat.
    {
        if (OnStartCombat != null)
        {
            SceneManager.Instance.LoadCombatScene(1);
            StartCoroutine(InvokeStartCombatDelay());
        }
    }
    public void InvokeEndCombat() // occurs if player survived the combat and all enemies are dealt with.
    {
        if (OnEndCombat != null)
        {
            SceneManager.Instance.LoadCombatScene(0);
            StartCoroutine(InvokeEndCombatDelay());
        }
    }
    private IEnumerator InvokeStartCombatDelay()
    {
        OnStartCombat.Invoke();

        yield return null;
        _playerCharacter = _playerPrefab.GetComponent<Player>();
        Debug.Log($"Combat started");
    }
    private IEnumerator InvokeEndCombatDelay()
    {
        OnEndCombat.Invoke();

        yield return null;
        Debug.Log($"Combat concluded");
    }
}
