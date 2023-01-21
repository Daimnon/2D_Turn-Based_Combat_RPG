using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private static CombatManager _instance;
    public static CombatManager Instance => _instance;

    private Character[] _playerParty, _enemyParty;
    private List<Character> _charactersTurnOrder;

    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        foreach (Character c in _playerParty)
            Spawn(true, c.gameObject);

        foreach (Character c in _enemyParty)
            Spawn(false, c.gameObject);
        // set new player party by player + allys amount & populate it
        // set new enemy party by num of currently faced enemies & populate it
    }
    private void OnDisable()
    {
        System.Array.Clear(_playerParty, 0, _playerParty.Length);
        System.Array.Clear(_enemyParty, 0, _playerParty.Length);
        _charactersTurnOrder.Clear();

        Debug.Log($"all combat collections has been reset: player party length - {_playerParty.Length}, enemy party length {_enemyParty.Length}, turn order {_charactersTurnOrder.Count}");
        // set new player party by player + allys amount & populate it
        // set new enemy party by num of currently faced enemies & populate it
    }

    private void Spawn(bool isPlayerParty, GameObject characterPrefab)
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

        Instantiate(characterPrefab, spawnPos, Quaternion.identity);
    }
    private void SetTurnOrder()
    {
        List<Character> turnOrder = new List<Character>();

        foreach (Character c in _playerParty)
            turnOrder.Add(c);

        foreach (Character c in _enemyParty)
            turnOrder.Add(c);

        // choose order by speed value
        _charactersTurnOrder = turnOrder.OrderBy(o => o.Data.Speed).ToList();
    }



    /*
     * Pre-Execute - Occurs before the ability strikes. Usually reserved for reaction effects that modify the ability attributes.
     * On Hit - Occurs when the ability scores a hit.
     * On Miss - Occurs when the ability doesn't score a hit (i.e. a "Miss"). Likely not used by the game at all.
     * On Crit - Occurs when the ability scores a crit. The ability has to score a hit first before the game checks for critical hits.
     * On Kill - Occurs when the ability kills an enemy. The ability has to score a hit first before the game checks for killing blows.
     * Post-Execute - Occurs after the ability made it's last strike. Unlike the other phases, this one will occur exactly once per ability execution, regardless of how many times the ability strikes.
     * Pre-Execute and Post-Execute phase will always occur when executing an ability, while the rest may not due to specific circumstances. For example, if the player doesn't score a crit with the selected ability, the On Crit phase will not occur.
    */
}
