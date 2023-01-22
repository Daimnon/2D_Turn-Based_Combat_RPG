using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Data/Enemy Data", order = 22)]
public class EnemyData : CharacterData
{
    public void Initialize(string name)
    {
        _name = name;

        _currentLevel = _maxHealth = _currentHealth = _maxMana = _currentMana = _vigor = _strength = _intelligence = _speed = 0;

        _primaryOneHanded = _secondaryOneHanded = _twoHanded = null;
        _headGear = null;
        _topGear = null;
        _bottomGear = null;
        _handsGear = null;
        _feetGear = null;

        _activeSkills = new Skill[8] { null, null, null, null, null, null, null, null };
    }
}
