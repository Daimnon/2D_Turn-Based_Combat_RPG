using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private EnemyWarriorCreator enemyWarriorCreator;
    private EnemyDefenderCreator enemyDefenderCreator;
    private EnemyEnchanterCreator enemyEnchanterCreator;
    private EnemyRogueCreator enemyRogueCreator;

    [SerializeField] protected List<EnemyData> _enemyWarriorDataFiles, _enemyDefenderDataFiles, _enemyEnchanterDataFiles, _enemyRogueDataFiles;
    public List<EnemyData> EnemyWarriorDataFiles { get => _enemyWarriorDataFiles; set => _enemyWarriorDataFiles = value; }
    public List<EnemyData> EnemyDefenderDataFiles { get => _enemyDefenderDataFiles; set => _enemyDefenderDataFiles = value; }
    public List<EnemyData> EnemyEnchanterDataFiles { get => _enemyEnchanterDataFiles; set => _enemyEnchanterDataFiles = value; }
    public List<EnemyData> EnemyRogueDataFiles { get => _enemyRogueDataFiles; set => _enemyRogueDataFiles = value; }

    private Enemy[] _currentTrainingEnemyList;
    public Enemy[] CurrentTrainingEnemyList { get => _currentTrainingEnemyList; set => _currentTrainingEnemyList = value; }

    private List<Enemy> _generalTrainingEnemyList;
    public List<Enemy> GeneralTrainingEnemyList { get => _generalTrainingEnemyList; set => _generalTrainingEnemyList = value; }

    private void Awake()
    {
        enemyWarriorCreator = gameObject.AddComponent<EnemyWarriorCreator>();
        enemyDefenderCreator = gameObject.AddComponent<EnemyDefenderCreator>();
        enemyEnchanterCreator = gameObject.AddComponent<EnemyEnchanterCreator>();
        enemyRogueCreator = gameObject.AddComponent<EnemyRogueCreator>();
    }

    private void Start()
    {

    }

    public void GoToStoryline()
    {

    }
    public void GenerateRandomEnemies()
    {

    }
    public void GoToTraining()
    {

    }
    public void GoToMerchant()
    {

    }

    private Enemy[] RandomPremadeEnemyGenerator()
    {
        int randEnemyAmount = Random.Range(1, 3);

        _currentTrainingEnemyList = new Enemy[3];
        Enemy[] enemies = new Enemy[3];

        for (int i = 0; i < randEnemyAmount; i++)
        {
            int randEnemyType = Random.Range(0, 4);
            int randEnemyWarriorData = Random.Range(0, _enemyWarriorDataFiles.Count);
            int randEnemyDefenderData = Random.Range(0, _enemyDefenderDataFiles.Count);
            int randEnemyEnchanterData = Random.Range(0, _enemyEnchanterDataFiles.Count);
            int randEnemyRogueData = Random.Range(0, _enemyRogueDataFiles.Count);

            switch (randEnemyType)
            {
                case 0:
                    enemies[i] = enemyWarriorCreator.CreateEnemyByData(_enemyWarriorDataFiles[randEnemyWarriorData]);
                    _currentTrainingEnemyList[i] = enemies[i];
                    break;
                case 1:
                    enemies[i] = enemyDefenderCreator.CreateEnemyByData(_enemyDefenderDataFiles[randEnemyDefenderData]);
                    _currentTrainingEnemyList[i] = enemies[i];
                    break;
                case 2:
                    enemies[i] = enemyEnchanterCreator.CreateEnemyByData(_enemyEnchanterDataFiles[randEnemyEnchanterData]);
                    _currentTrainingEnemyList[i] = enemies[i];
                    break;
                case 3:
                    enemies[i] = enemyRogueCreator.CreateEnemyByData(_enemyRogueDataFiles[randEnemyRogueData]);
                    _currentTrainingEnemyList[i] = enemies[i];
                    break;
            }
        }
        return enemies;
    }
}
