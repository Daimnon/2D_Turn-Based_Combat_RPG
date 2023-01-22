using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private GameObject _playerPrefab;
    private Player _playerCharacter;

    public Player PlayerCharacter => _playerCharacter;

    private void Awake()
    {
        _instance = this;
        _playerCharacter = _playerPrefab.GetComponent<Player>();

        CombatManager.Instance.OnStartCombat += OnCombatStart;
    }

    public void OnCombatStart()
    {
        CombatManager.Instance.Initialize();
    }
}
