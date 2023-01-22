using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "ScriptableObject/Data/Player Data", order = 21)]
public class PlayerData : CharacterData
{
    #region back fields
    [Header("Player Info")]
    [SerializeField] private float _maxExp, _currentExp;
    [SerializeField] private int _maxLevel;
    [SerializeField] private int _totalSkillPoints, _spentSkillPoints, _availableSkillPoints;
    [SerializeField] private int _totalStatPoints, _spentStatPoints, _availableStatPoints;
    //[SerializeField] private Texture2D _spriteSheet;

    [Header("Ally Skills")]
    [SerializeField] private List<Skill> _allAquiredSkills;
    #endregion

    #region properties
    public float MaxExp { get => _maxExp; set => _ = value; }
    public float CurrentExp { get => _currentExp; set => _ = value; }
    public int MaxLevel { get => _maxLevel; set => _ = value; }
    public int TotalSkillPoints { get => _totalSkillPoints; set => _ = value; }
    public int SpentSkillPoints { get => _spentSkillPoints; set => _ = value; }
    public int AvailableSkillPoints { get => _availableSkillPoints; set => _ = value; }
    public int TotalStatPoints { get => _totalStatPoints; set => _ = value; }
    public int SpentStatPoints { get => _spentStatPoints; set => _ = value; }
    public int AvailableStatPoints { get => _availableStatPoints; set => _ = value; }
    public List<Skill> AllAquiredSkills { get => _allAquiredSkills; set => _ = value; }
    //public Texture2D SpriteSheet => _spriteSheet;
    #endregion

    public void Initialize(string name)
    {
        _name = name;

        _maxLevel = _currentLevel = _maxHealth = _currentHealth = _maxMana = _currentMana = _vigor = _strength = _intelligence = _speed =
        _totalSkillPoints = _spentSkillPoints = _availableSkillPoints = _totalStatPoints = _spentStatPoints = _availableStatPoints = 0;

        _primaryOneHanded = _secondaryOneHanded = _twoHanded = null;
        _headGear = null;
        _topGear = null;
        _bottomGear = null;
        _handsGear = null;
        _feetGear = null;

        _allAquiredSkills.Clear();
        _activeSkills = new Skill[8] { null, null, null, null, null, null, null, null};
    }
    public void ReSpec()
    {
        // reset skill points
        _availableSkillPoints = _totalSkillPoints;
        _allAquiredSkills.Clear();
        _spentSkillPoints = 0;

        // reset stat points
        _availableStatPoints = _totalStatPoints;
        _vigor = _strength = _intelligence = _speed = 0;
        _spentStatPoints = 0;
    }
}
