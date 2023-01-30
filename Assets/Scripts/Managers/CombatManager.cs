using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private static CombatManager _instance;
    public static CombatManager Instance => _instance;

    [SerializeField] private GameObject _playerPrefab, _allyPrefab, _enemyPrefab;
    public GameObject PlayerPrefab => _playerPrefab;
    public GameObject AllyPrefab => _allyPrefab;
    public GameObject EnemyPrefab => _enemyPrefab;

    [SerializeField] private List<Character>_combatParticipantsSortedByTurn;
    public List<Character> CombatParticipantsSortedByTurn { get => _combatParticipantsSortedByTurn; set => _combatParticipantsSortedByTurn = value; }
    
    [SerializeField] private Transform[] _playerTr, _enemiesTr;
    
    public event Action<Character> OnStartTurnByCharacter, OnAttackByCharacter, OnAttackHitByCharacter, OnAttackMissByCharacter, OnAttackHitCritByCharacter, OnAttackKillOpponent, OnAttackResolveByOpponent, OnDeathByCharacter, OnEndTurnByCharacter, OnPlayerVictory;

    private int _maxPartyMembers = 3;

    private void Awake()
    {
        Initialize();
        GameManager.Instance.InvokeStartCombat();
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        //_enemyParty.Clear();
        _combatParticipantsSortedByTurn.Clear();

        Debug.Log($"all combat collections has been reset");
        // set new player party by player + allys amount & populate it
        // set new enemy party by num of currently faced enemies & populate it
    }
    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void Initialize()
    {
        _instance = this;
    }
    private void SpawnCharacters()
    {
        for (int i = 0; i < PartyManager.Instance.PlayerParty.Length -1; i++)
        {
            switch (i)
            {
                case 0:
                    PartyManager.Instance.PlayerParty[i] = Spawn(PartyManager.Instance.PlayerParty[0], PartyManager.Instance.PlayerParty[0].gameObject, 0);
                    break;
                case 1:
                    if (PartyManager.Instance.PlayerParty[i])
                        PartyManager.Instance.PlayerParty[i] = Spawn(PartyManager.Instance.PlayerParty[1], PartyManager.Instance.PlayerParty[1].gameObject, 1);
                    break;
                case 2:
                    if (PartyManager.Instance.PlayerParty[i])
                        PartyManager.Instance.PlayerParty[i] = Spawn(PartyManager.Instance.PlayerParty[2], PartyManager.Instance.PlayerParty[2].gameObject, 2);
                    break;
            }
        }
        for (int i = 0; i < PartyManager.Instance.EnemyParty.Length -1; i++)
        {
            switch (i)
            {
                case 0:
                    if (PartyManager.Instance.EnemyParty[i])
                        PartyManager.Instance.EnemyParty[i] = Spawn(PartyManager.Instance.EnemyParty[0], PartyManager.Instance.EnemyParty[0].gameObject, 0);
                    break;
                case 1:
                    if (PartyManager.Instance.EnemyParty[i])
                        PartyManager.Instance.EnemyParty[i] = Spawn(PartyManager.Instance.EnemyParty[1], PartyManager.Instance.EnemyParty[1].gameObject, 1);
                    break;
                case 2:
                    if (PartyManager.Instance.EnemyParty[i])
                        PartyManager.Instance.EnemyParty[i] = Spawn(PartyManager.Instance.EnemyParty[2], PartyManager.Instance.EnemyParty[2].gameObject, 2);
                    break;
            }
        }
    }


    #region Events 
    public void InvokeStartTurnByCharacter(Character invokerC) // occurs when this character's turn has started.
    {
        //OnStartTurnByCharacter?.Invoke(invokerC);
        if (OnStartTurnByCharacter != null)
        {
            OnStartTurnByCharacter(invokerC);
            Debug.Log($"Its {invokerC.Data.Name}'s turn");
        }
    }
    public void InvokeAttackByCharacter(Character invokerC) // occurs before the ability strikes. Usually reserved for reaction effects that modify the ability attributes.
    {
        if (OnAttackByCharacter != null)
        {
            OnAttackByCharacter(invokerC);
            Debug.Log($"{invokerC.Data.Name} attacked!");
        }
    }
    public void InvokeAttackHitOnOpponent(Character invokerC) // occurs when the ability scores a hit.
    {
        if (OnAttackHitByCharacter != null)
        {
            OnAttackHitByCharacter(invokerC);
            Debug.Log($"{invokerC.Data.Name} hit!");
        }
    }
    public void InvokeAttackMissByCharacter(Character invokerC) // occurs when the ability doesn't score a hit (i.e. a "Miss"). Likely not used by the game at all.
    {
        if (OnAttackMissByCharacter != null)
        {
            OnAttackMissByCharacter(invokerC);
            Debug.Log($"{invokerC.Data.Name} missed!");
        }
    }
    public void InvokeAttackHitCritOnOpponent(Character invokerC) // occurs when the ability scores a crit. The ability has to score a hit first before the game checks for critical hits.
    {
        if (OnAttackHitCritByCharacter != null)
        {
            OnAttackHitCritByCharacter(invokerC);
            Debug.Log($"{invokerC.Data.Name} hit critical!");
        }
    }
    public void InvokeAttackKillOpponent(Character invokerC) // occurs when the ability kills an enemy. The ability has to score a hit first before the game checks for killing blows.
    {
        if (OnAttackKillOpponent != null)
        {
            OnAttackKillOpponent(invokerC);
            Debug.Log($"{invokerC.Data.Name} killed an opponent!");
        }
    }
    public void InvokeAttackResolveByOpponent(Character invokerC) // occurs after the ability made it's last strike. Unlike the other phases, this one will occur exactly once per ability execution, regardless of how many times the ability strikes.
    {
        if (OnAttackResolveByOpponent != null)
        {
            OnAttackResolveByOpponent(invokerC);
            Debug.Log($"{invokerC.Data.Name} resolved apllied interaction");
        }
    }
    public void InvokeDeathByCharacter(Character invokerC) // occurs when current hp reach 0.
    {
        if (OnDeathByCharacter != null)
        {
            OnDeathByCharacter(invokerC);
            Debug.Log($"{invokerC.Data.Name} has died");
        }
    }
    public void InvokeEndTurnByCharacter(Character invokerC) // occurs when this character's turn has ended.
    {
        if (OnEndTurnByCharacter != null)
        {
            OnEndTurnByCharacter(invokerC);
            Debug.Log($"{invokerC.Data.Name} turn's ended");
        }
    }
    
    public void InvokeVictoryByCharacter(Character invokerC) // occurs if player survived the combat and all enemies are dealt with.
    {
        if (OnPlayerVictory != null)
        {
            OnPlayerVictory(invokerC);
            Debug.Log($"Combat ended, player won!");
        }
    }
    
    #endregion

    private void SubscribeToEvents()
    {
        GameManager.Instance.OnStartCombat += OnStartCombat;
    }
    private void UnsubscribeFromEvents()
    {
        GameManager.Instance.OnStartCombat -= OnStartCombat;
    }

    // if not using return can be simplified
    private Character Spawn(Character c, GameObject characterPrefab, int partyIndex)
    {
        Vector2 spawnPos = Vector2.zero;

        if (c is Ally)
        {
            switch (partyIndex)
            {
                case 1:
                    spawnPos = new(x: -6.75f, y: 1f);
                    (c as Ally).PartyIndex = partyIndex;
                    break;
                case 2:
                    spawnPos = new(x: -6.75f, y: -1f);
                    (c as Ally).PartyIndex = partyIndex;
                    break;
                default:
                    break;
            }
        }
        else
        {
            spawnPos = new(x: -5f, y: 0f);
        }

        GameObject spawnC = Instantiate(characterPrefab, (Vector3)spawnPos, Quaternion.identity);
        return spawnC.GetComponent<Character>();
    }
    private Enemy Spawn(Enemy e, GameObject characterPrefab, int partyIndex)
    {
        Vector2 spawnPos = Vector2.zero;

        switch (partyIndex)
        {
            case 0:
                spawnPos = new(x: 5f, y: 0f);
                e.PartyIndex = partyIndex;
                break;
            case 1:
                spawnPos = new(x: 6.75f, y: 1f);
                e.PartyIndex = partyIndex;
                break;
            case 2:
                spawnPos = new(x: 6.75f, y: -1f);
                e.PartyIndex = partyIndex;
                break;
            default:
                break;
        }

        GameObject spawnE = Instantiate(characterPrefab, (Vector3)spawnPos, Quaternion.identity);
        return spawnE.GetComponent<Enemy>();
    }
    private void SetTurnOrder()
    {
        List<Character> turnOrder = new();

        foreach (Character c in PartyManager.Instance.PlayerParty)
        {
            if (c)
                turnOrder.Add(c);
        }

        foreach (Enemy e in PartyManager.Instance.EnemyParty)
        {
            if (e)
                turnOrder.Add(e);
        }

        UpdateTurnOrderBySpeed();
    }
    public void UpdateTurnOrderBySpeed()
    {
        _combatParticipantsSortedByTurn.OrderBy(o => o.Data.Speed).ToList();
        InvokeStartTurnByCharacter(_combatParticipantsSortedByTurn[0]);
    }
    public Vector2 GetAttackDirection(Character attackerC, Character recieverC)
    {
        return (attackerC.transform.position - recieverC.AttackerPosTr.position);
    }
    public Vector2 GetAttackDirectionNormalized(Character attackerC, Character recieverC)
    {
        return (attackerC.transform.position - recieverC.AttackerPosTr.position).normalized;
    }
    public void OnStartCombat()
    {
        SpawnCharacters();
        SetTurnOrder();
    }
    private void BattleConclusion()
    {
        // get rewards
    }
    private void EndCombat()
    {
        GameManager.Instance.InvokeEndGame();
        Destroy(gameObject);
    }
}

/*
 * 
 * 
 */
