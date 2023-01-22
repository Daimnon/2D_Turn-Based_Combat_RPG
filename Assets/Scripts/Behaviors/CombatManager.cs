using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class CombatManager : MonoBehaviour
{
    private static CombatManager _instance;
    public static CombatManager Instance => _instance;

    [SerializeField] private GameObject _playerPrefab, _allyPrefab, _enemyPrefab;

    private List<Character> _playerParty, _enemyParty, _combatParticipantsSortedByTurn;

    public List<Character> PlayerParty { get => _playerParty; set => _ = value; }
    public List<Character> EnemyParty { get => _enemyParty; set => _ = value; }
    public List<Character> CombatParticipantsSortedByTurn { get => _combatParticipantsSortedByTurn; set => _ = value; }

    public event Action OnStartCombat;
    public event Action<Character> OnStartCombatByCharacter, OnStartTurnByCharacter, OnAttackByCharacter, OnAttackHitByCharacter, OnAttackMissByCharacter, OnAttackHitCritByCharacter, OnAttackKillOpponent, OnAttackResolveByOpponent, OnDeathByCharacter, OnEndTurnByCharacter, OnEndCombatByCharacter;

    private int _maxPartyMembers = 3;

    private void Awake()
    {
        _instance = this;
        _playerParty = new List<Character>(3) { null, null, null };
        _enemyParty = new List<Character>(3){ null, null, null };
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        _playerParty.Clear();
        _playerParty.Clear();
        _combatParticipantsSortedByTurn.Clear();

        Debug.Log($"all combat collections has been reset");
        // set new player party by player + allys amount & populate it
        // set new enemy party by num of currently faced enemies & populate it
    }

    #region Events
    public void InvokeStartCombat() // occurs when entering combat.
    {
        if (OnStartCombat != null)
        {
            OnStartCombat.Invoke();
            Debug.Log($"Combat started");
        }
    }
    public void InvokeStartTurnByCharacter(Character invokerC) // occurs when this character's turn has started.
    {
        if (OnStartTurnByCharacter != null)
        {
            OnStartTurnByCharacter.Invoke(invokerC);
            Debug.Log($"Its {invokerC.Data.Name}'s turn");
        }
    }
    public void InvokeAttackByCharacter(Character invokerC) // occurs before the ability strikes. Usually reserved for reaction effects that modify the ability attributes.
    {
        if (OnAttackByCharacter != null)
        {
            OnAttackByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} attacked!");
        }
    }
    public void InvokeAttackHitOnOpponent(Character invokerC) // occurs when the ability scores a hit.
    {
        if (OnAttackHitByCharacter != null)
        {
            OnAttackHitByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} hit!");
        }
    }
    public void InvokeAttackMissByCharacter(Character invokerC) // occurs when the ability doesn't score a hit (i.e. a "Miss"). Likely not used by the game at all.
    {
        if (OnAttackMissByCharacter != null)
        {
            OnAttackMissByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} missed!");
        }
    }
    public void InvokeAttackHitCritOnOpponent(Character invokerC) // occurs when the ability scores a crit. The ability has to score a hit first before the game checks for critical hits.
    {
        if (OnAttackHitCritByCharacter != null)
        {
            OnAttackHitCritByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} hit critical!");
        }
    }
    public void InvokeAttackKillOpponent(Character invokerC) // occurs when the ability kills an enemy. The ability has to score a hit first before the game checks for killing blows.
    {
        if (OnAttackKillOpponent != null)
        {
            OnAttackKillOpponent.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} killed an opponent!");
        }
    }
    public void InvokeAttackResolveByOpponent(Character invokerC) // occurs after the ability made it's last strike. Unlike the other phases, this one will occur exactly once per ability execution, regardless of how many times the ability strikes.
    {
        if (OnAttackResolveByOpponent != null)
        {
            OnAttackResolveByOpponent.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} resolved apllied interaction");
        }
    }
    public void InvokeDeathByCharacter(Character invokerC) // occurs when current hp reach 0.
    {
        if (OnDeathByCharacter != null)
        {
            OnDeathByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} has died");
        }
    }
    public void InvokeEndTurnByCharacter(Character invokerC) // occurs when this character's turn has ended.
    {
        if (OnEndTurnByCharacter != null)
        {
            OnEndTurnByCharacter.Invoke(invokerC);
            Debug.Log($"{invokerC.Data.Name} turn's ended");
        }
    }
    public void InvokeEndCombatByCharacter(Character invokerC) // occurs if player survived the combat and all enemies are dealt with.
    {
        if (OnEndCombatByCharacter != null)
        {
            OnEndCombatByCharacter.Invoke(invokerC);
            Debug.Log($"Combat ended, player won!");
        }
    }
    #endregion

    // if not using return can be simplified
    private Character Spawn(bool isPlayer, bool isPlayerParty, GameObject characterPrefab)
    {
        Vector2 spawnPos;

        switch (isPlayerParty)
        {
            case true:
                spawnPos = new(-5f, 0f);
                break;
            case false:
                spawnPos = new(5f, 0f);
                break;
        }

        /* if does not need the return value use this:
         * Instantiate(characterPrefab, spawnPos, Quaternion.identity);
           instead of the code below */

        GameObject spawnC = Instantiate(characterPrefab, spawnPos, Quaternion.identity);
        return spawnC.GetComponent<Character>();
    }
    public void Initialize()
    {
        // set new player party by player + allys amount & populate it
        for (int i = 0; i < _maxPartyMembers; i++)
        {
            Spawn(true, c.gameObject);
        }
        foreach (Character c in _playerParty)


            // set new enemy party by num of currently faced enemies & populate it
            foreach (Character c in _enemyParty)
                Spawn(false, c.gameObject);
        //if (!_isMyTurn)
        //    _combatState = Waiting;
        //else
        //    _combatState = Attacking;

        if (this is Enemy && !EnemyParty[0])
        {
            Instance.EnemyParty[0] = this;
            Instance.Spawn(true, c.gameObject);
        }
        else if (this is Enemy && !Instance.EnemyParty[1])
        {

            EnemyParty[1] = this;
        }
        else if (this is Enemy && !Instance.EnemyParty[0])
        {

            EnemyParty[2] = this;
        }
        else if (this is Player)
        {

            PlayerParty[0] = this;
        }
        else if (!PlayerParty[1])
        {

            PlayerParty[1] = this;
        }
        else if (!PlayerParty[0])
        {

            PlayerParty[2] = this;
        }

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
    public Vector2 GetAttackDirection(Character attackerC, Character recieverC)
    {
        return (attackerC.transform.position - recieverC.AttackerPosTr.position);
    }
    public Vector2 GetAttackDirectionNormalized(Character attackerC, Character recieverC)
    {
        return (attackerC.transform.position - recieverC.AttackerPosTr.position).normalized;
    }
    private void BattleConclusion()
    {
        // get rewards
    }
}
