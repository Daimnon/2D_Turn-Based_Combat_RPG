using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private static CombatManager _instance;
    public static CombatManager Instance => _instance;

    private Character[] _playerParty, _enemyParty;
    private List<Character> _combatParticipantsSortedByTurn;

    public event Action OnAttackCompleted;

    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        // set new player party by player + allys amount & populate it
        foreach (Character c in _playerParty)
            Spawn(true, c.gameObject);

        // set new enemy party by num of currently faced enemies & populate it
        foreach (Character c in _enemyParty)
            Spawn(false, c.gameObject);
    }
    private void OnDisable()
    {
        System.Array.Clear(_playerParty, 0, _playerParty.Length);
        System.Array.Clear(_enemyParty, 0, _playerParty.Length);
        _combatParticipantsSortedByTurn.Clear();

        Debug.Log($"all combat collections has been reset: player party length - {_playerParty.Length}, enemy party length {_enemyParty.Length}, turn order {_combatParticipantsSortedByTurn.Count}");
        // set new player party by player + allys amount & populate it
        // set new enemy party by num of currently faced enemies & populate it
    }

    #region Events
    public void OnAttackComplete()
    {
        if (OnAttackCompleted != null)
            OnAttackCompleted.Invoke();
    }
    #endregion

    // if not using return can be simplified
    private Character Spawn(bool isPlayerParty, GameObject characterPrefab)
    {
        Vector2 spawnPos;

        switch (isPlayerParty)
        {
            case true:
                spawnPos = new(-50f, 0f);
                break;
            case false:
                spawnPos = new(50f, 0f);
                break;
        }

        /* if does not need the return value use this:
         * Instantiate(characterPrefab, spawnPos, Quaternion.identity);
           instead of the code below */

        GameObject spawnC = Instantiate(characterPrefab, spawnPos, Quaternion.identity);
        return spawnC.GetComponent<Character>();
    }
    private void SetTurnOrder()
    {
        List<Character> turnOrder = new List<Character>();

        foreach (Character c in _playerParty)
            turnOrder.Add(c);

        foreach (Character c in _enemyParty)
            turnOrder.Add(c);

        // choose order by speed value
        _combatParticipantsSortedByTurn = turnOrder.OrderBy(o => o.Data.Speed).ToList();
    }
    private Vector2 GetAttackDirection(Character attackerC, Character recieverC)
    {
        return (attackerC.transform.position - recieverC.transform.position).normalized;
    }
}
