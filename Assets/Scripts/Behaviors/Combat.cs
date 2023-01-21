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

    public event Action<Character> OnStartCombatByCharacter, OnStartTurnByCharacter, OnAttackByCharacter, OnAttackHitByCharacter, OnAttackMissByCharacter, OnAttackHitCritByCharacter, OnAttackKillByCharacter, OnAttackResolveByCharacter, OnDeathByCharacter, OnEndTurnByCharacter, OnEndCombatByCharacter;

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
    public void OnStartCombat(Character invokerC) // occurs when entering combat.
    {
        if (OnStartCombatByCharacter != null)
        {
            OnStartCombatByCharacter.Invoke(invokerC);
            Debug.Log($"Combat started");
        }
    }
    public void OnStartTurn(Character invokerC) // occurs when this character's turn has started.
    {
        if (OnStartTurnByCharacter != null)
        {
            OnStartTurnByCharacter.Invoke(invokerC);
            Debug.Log($"Its {invokerC.Data.Name}'s turn");
        }
    }
    public void OnAttack(Character invokerC) // occurs before the ability strikes. Usually reserved for reaction effects that modify the ability attributes.
    {
        if (OnAttackByCharacter != null)
        {
            OnAttackByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} attacked!");
        }
    }
    public void OnAttackHit(Character invokerC) // occurs when the ability scores a hit.
    {
        if (OnAttackHitByCharacter != null)
        {
            OnAttackHitByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} hit!");
        }
    }
    public void OnAttackMiss(Character invokerC) // occurs when the ability doesn't score a hit (i.e. a "Miss"). Likely not used by the game at all.
    {
        if (OnAttackMissByCharacter != null)
        {
            OnAttackMissByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} missed!");
        }
    }
    public void OnAttackHitCrit(Character invokerC) // occurs when the ability scores a crit. The ability has to score a hit first before the game checks for critical hits.
    {
        if (OnAttackHitCritByCharacter != null)
        {
            OnAttackHitCritByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} hit critical!");
        }
    }
    public void OnAttackKill(Character invokerC) // occurs when the ability kills an enemy. The ability has to score a hit first before the game checks for killing blows.
    {
        if (OnAttackKillByCharacter != null)
        {
            OnAttackKillByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} killed an opponent!");
        }
    }
    public void OnAttackResolve(Character invokerC) // occurs after the ability made it's last strike. Unlike the other phases, this one will occur exactly once per ability execution, regardless of how many times the ability strikes.
    {
        if (OnAttackResolveByCharacter != null)
        {
            OnAttackResolveByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} resolved apllied interaction");
        }
    }
    public void OnDeath(Character invokerC) // occurs when current hp reach 0.
    {
        if (OnDeathByCharacter != null)
        {
            OnDeathByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} has died");
        }
    }
    public void OnEndTurn(Character invokerC) // occurs when this character's turn has ended.
    {
        if (OnEndTurnByCharacter != null)
        {
            OnEndTurnByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} turn's ended");
        }
    }
    public void OnEndCombat(Character invokerC) // occurs if player survived the combat and all enemies are dealt with.
    {
        if (OnEndCombatByCharacter != null)
        {
            OnEndCombatByCharacter.Invoke(invokerC);
            Debug.Log($"Combat ended, player won!");
        }
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
    private void BattleConclusion()
    {
        // get rewards
    }
}
