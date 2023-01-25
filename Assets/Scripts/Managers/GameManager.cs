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


    public event Action OnStartGame, OnStartCombat, OnEndCombat;

    private void Awake()
    {
        _instance = this;
        _playerCharacter = _playerPrefab.GetComponent<Player>();
        DontDestroyOnLoad(this);
    }

    public void InvokeStartGame() // occurs when entering combat.
    {
        if (OnStartGame != null)
        {
            //Invoke(nameof(OnStartGame), 0);
            OnStartGame.Invoke();
        }
    }
    public void InvokeStartCombat() // occurs when entering combat.
    {
        if (OnStartCombat != null)
        {
            OnStartCombat.Invoke();
        }
    }
    public void InvokeEndCombat() // occurs if player survived the combat and all enemies are dealt with.
    {
        if (OnEndCombat != null)
        {
            OnEndCombat.Invoke();
        }
    }
    private IEnumerator InvokeInvokeStartGameDelay()
    {
        yield return null;

        OnStartGame.Invoke();
        Debug.Log($"Combat started");
    }
    private IEnumerator InvokeStartCombatDelay()
    {
        yield return null;

        OnStartCombat.Invoke();
        Debug.Log($"Combat started");
    }
    private IEnumerator InvokeEndCombatDelay()
    {
        yield return null;

        OnEndCombat.Invoke();
        Debug.Log($"Combat concluded");
    }
}
