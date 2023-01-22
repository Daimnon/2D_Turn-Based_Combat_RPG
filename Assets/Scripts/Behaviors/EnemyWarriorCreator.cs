using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarriorCreator : EnemyCreator
{
    [SerializeField] private List<EnemyData> _warriorEnemiesData;

    public override Enemy CreateEnemy()
    {
        int rand = Random.Range(0, _warriorEnemiesData.Count);
        Enemy newEnemy = new Enemy();
        newEnemy.Data = _warriorEnemiesData[rand];
        return newEnemy;
    }
}
