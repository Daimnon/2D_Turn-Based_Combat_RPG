using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnchanterCreator : EnemyCreator
{
    [SerializeField] private List<EnemyData> _warriorEnemiesDataFiles;
    [SerializeField] private List<HeadGear> _warriorEnemiesHeadGrears;
    [SerializeField] private List<TopGear> _warriorEnemiesTopGrears;
    [SerializeField] private List<BottomGear> _warriorEnemiesBottomGearGrears;
    [SerializeField] private List<HandsGear> _warriorEnemiesHandsGrears;
    [SerializeField] private List<FeetGear> _warriorEnemiesFeetGears;
    [SerializeField] private List<Weapon> _warriorEnemiesPrimaryOneHandeds, _warriorEnemiesSecondaryOneHandeds, _warriorEnemiesTwoHandeds;
    [SerializeField] private List<Skill> _allSkills;

    public override Enemy CreateRandomEnemy(string name, int minLvl, int maxLvl, int minHealth, int maxHealth, int minMana, int maxMana, int minStat, int maxStat, int minSkill, int maxSkill)
    {
        int randLevel = Random.Range(minStat, maxStat + 1);
        int randHealth = Random.Range(minStat, maxStat + 1);
        int randMana = Random.Range(minStat, maxStat + 1);
        int randHeadGear = Random.Range(0, _warriorEnemiesHeadGrears.Count);
        int randTopGear = Random.Range(0, _warriorEnemiesTopGrears.Count);
        int randBottomGear = Random.Range(0, _warriorEnemiesBottomGearGrears.Count);
        int randHandsGear = Random.Range(0, _warriorEnemiesHandsGrears.Count);
        int randFeetGear = Random.Range(0, _warriorEnemiesFeetGears.Count);
        int randPrimaryOneHanded = Random.Range(0, _warriorEnemiesPrimaryOneHandeds.Count);
        int randSecondaryOneHanded = Random.Range(0, _warriorEnemiesSecondaryOneHandeds.Count);
        int randTwoHanded = Random.Range(0, _warriorEnemiesTwoHandeds.Count);


        EnemyData data = new();
        data.name = name;

        Enemy newEnemy = new()
        {
            Data = data
        };

        newEnemy.Data.CurrentLevel = randLevel;

        newEnemy.Data.MaxHealth = randHealth;
        newEnemy.Data.CurrentHealth = newEnemy.Data.MaxHealth;

        newEnemy.Data.MaxMana = randMana;
        newEnemy.Data.CurrentMana = newEnemy.Data.MaxMana;

        int randStat = Random.Range(minStat, maxStat + 1);
        newEnemy.Data.Vigor = randStat;
        randStat = Random.Range(minStat, maxStat + 1);
        newEnemy.Data.Strength = randStat;
        randStat = Random.Range(minStat, maxStat + 1);
        newEnemy.Data.Intelligence = randStat;
        randStat = Random.Range(minStat, maxStat + 1);
        newEnemy.Data.Speed = randStat;

        newEnemy.Data.HeadGear = _warriorEnemiesHeadGrears[randHeadGear];
        newEnemy.Data.TopGear = _warriorEnemiesTopGrears[randTopGear];
        newEnemy.Data.BottomGear = _warriorEnemiesBottomGearGrears[randBottomGear];
        newEnemy.Data.HandsGear = _warriorEnemiesHandsGrears[randHandsGear];
        newEnemy.Data.FeetGear = _warriorEnemiesFeetGears[randFeetGear];
        newEnemy.Data.PrimaryOneHanded = _warriorEnemiesPrimaryOneHandeds[randPrimaryOneHanded];
        newEnemy.Data.SecondaryOneHanded = _warriorEnemiesSecondaryOneHandeds[randSecondaryOneHanded];
        newEnemy.Data.TwoHanded = _warriorEnemiesTwoHandeds[randTwoHanded];

        int randSkill = Random.Range(minSkill, _allSkills.Count + 1);

        for (int i = 0; i < newEnemy.Data.ActiveSkills.Length; i++)
        {
            newEnemy.Data.ActiveSkills[i] = _allSkills[randSkill];
            randSkill = Random.Range(minSkill, _allSkills.Count + 1);
        }

        newEnemy.DefinedRole = Roles.Enchanter;
        return newEnemy;
    }
    public override Enemy CreateEnemyByRandomData()
    {
        int rand = Random.Range(0, _warriorEnemiesDataFiles.Count);
        EnemyData data = _warriorEnemiesDataFiles[rand];

        Enemy newEnemy = new()
        {
            Data = data
        };

        newEnemy.Data.Name = data.Name;
        newEnemy.Data.CurrentLevel = data.CurrentLevel;
        newEnemy.Data.MaxHealth = data.MaxHealth;
        newEnemy.Data.CurrentHealth = data.CurrentHealth;
        newEnemy.Data.MaxMana = data.MaxMana;
        newEnemy.Data.CurrentMana = data.CurrentMana;
        newEnemy.Data.Vigor = data.Vigor;
        newEnemy.Data.Strength = data.Strength;
        newEnemy.Data.Intelligence = data.Intelligence;
        newEnemy.Data.Speed = data.Speed;

        newEnemy.Data.PrimaryOneHanded = data.PrimaryOneHanded;
        newEnemy.Data.SecondaryOneHanded = data.SecondaryOneHanded;
        newEnemy.Data.TwoHanded = data.TwoHanded;
        newEnemy.Data.HeadGear = data.HeadGear;
        newEnemy.Data.TopGear = data.TopGear;
        newEnemy.Data.BottomGear = data.BottomGear;
        newEnemy.Data.HandsGear = data.HandsGear;
        newEnemy.Data.FeetGear = data.FeetGear;

        newEnemy.Data.ActiveSkills = data.ActiveSkills;
        newEnemy.DefinedRole = Roles.Enchanter;
        return newEnemy;
    }
    public override Enemy CreateEnemyByData(EnemyData data)
    {
        Enemy newEnemy = new()
        {
            Data = data
        };

        newEnemy.Data.Name = data.Name;
        newEnemy.Data.CurrentLevel = data.CurrentLevel;
        newEnemy.Data.MaxHealth = data.MaxHealth;
        newEnemy.Data.CurrentHealth = data.CurrentHealth;
        newEnemy.Data.MaxMana = data.MaxMana;
        newEnemy.Data.CurrentMana = data.CurrentMana;
        newEnemy.Data.Vigor = data.Vigor;
        newEnemy.Data.Strength = data.Strength;
        newEnemy.Data.Intelligence = data.Intelligence;
        newEnemy.Data.Speed = data.Speed;

        newEnemy.Data.PrimaryOneHanded = data.PrimaryOneHanded;
        newEnemy.Data.SecondaryOneHanded = data.SecondaryOneHanded;
        newEnemy.Data.TwoHanded = data.TwoHanded;
        newEnemy.Data.HeadGear = data.HeadGear;
        newEnemy.Data.TopGear = data.TopGear;
        newEnemy.Data.BottomGear = data.BottomGear;
        newEnemy.Data.HandsGear = data.HandsGear;
        newEnemy.Data.FeetGear = data.FeetGear;

        newEnemy.Data.ActiveSkills = data.ActiveSkills;
        newEnemy.DefinedRole = Roles.Enchanter;
        return newEnemy;
    }
}

