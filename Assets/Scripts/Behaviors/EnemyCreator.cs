using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCreator : MonoBehaviour
{
    public abstract Enemy CreateRandomEnemy(string name, int minLvl, int maxLvl, int minHealth, int maxHealth, int minMana, int maxMana, int minStat, int maxStat, int minSkill, int maxSkill);
    public abstract Enemy CreateEnemyByRandomData();
    public abstract Enemy CreateEnemyByData(EnemyData enemyData);
}
