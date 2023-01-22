using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ally", menuName = "ScriptableObject/Data/Ally Data", order = 23)]
public class AllyData : CharacterData
{
    #region back fields
    [Header("Ally Info")]
    [SerializeField] private float _maxExp, _currentExp;
    [SerializeField] private int _maxLevel;
    [SerializeField] private int _totalStatPoints, _spentStatPoints, _availableStatPoints;
    //[SerializeField] private Texture2D _spriteSheet;

    [Header("Ally Skills")]
    [SerializeField] private List<Skill> _allAquiredSkills;
    #endregion

    #region properties
    public float MaxExp { get => _maxExp; set => _ = value; }
    public float CurrentExp { get => _currentExp; set => _ = value; }
    public int MaxLevel { get => _maxLevel; set => _ = value; }
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
        _totalStatPoints = _spentStatPoints = _availableStatPoints = 0;

        _primaryOneHanded = _secondaryOneHanded = _twoHanded = null;
        _headGear = null;
        _topGear = null;
        _bottomGear = null;
        _handsGear = null;
        _feetGear = null;

        _allAquiredSkills.Clear();
        _activeSkills = new Skill[8] { null, null, null, null, null, null, null, null };
    }
    public void ReSpec()
    {
        // reset stat points
        _availableStatPoints = _totalStatPoints;
        _vigor = _strength = _intelligence = _speed = 0;
        _spentStatPoints = 0;
    }
}
