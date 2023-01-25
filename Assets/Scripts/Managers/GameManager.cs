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
    public Player PlayerCharacter => _playerCharacter;

    [SerializeField] private GameState _gameState;
    public GameState GameState { get => _gameState; set => _gameState = value; }


    public event Action OnStartGame, OnStartCombat, OnEndCombat, OnEndGame;

    private void Awake()
    {
        _instance = this;
        _playerCharacter = _playerPrefab.GetComponent<Player>();
        DontDestroyOnLoad(this);
    }

    public void InvokeStartGame() // occurs when entering combat.
    {
        OnStartGame?.Invoke();
    }
    public void InvokeStartCombat() // occurs when entering combat.
    {
        OnStartCombat?.Invoke();
    }
    public void InvokeEndCombat() // occurs if player survived the combat and all enemies are dealt with.
    {
        OnEndCombat?.Invoke();
    }
    public void InvokeEndGame() // occurs when entering combat.
    {
        OnEndGame?.Invoke();
    }
}
