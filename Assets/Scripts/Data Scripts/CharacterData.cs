using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObject/Character Data", order = 20)]
public class CharacterData : ScriptableObject
{
    #region back fields
    [Header("Character Info")]
    [SerializeField] private string _name;
    [SerializeField] private int _maxLevel, _currentLevel;
    [SerializeField] private float _maxExp, _currentExp;
    [SerializeField] private int _maxHealth, _currentHealth, _maxMana, _currentMana;
    [SerializeField] private int _vigor, _strength, _intelligence, _speed;
    [SerializeField] private int _totalSkillPoints, _spentSkillPoints, _availableSkillPoints;
    [SerializeField] private int _totalStatPoints, _spentStatPoints, _availableStatPoints;
    //[SerializeField] private Texture2D _spriteSheet;

    [Header("Character Equipment")]
    [SerializeField] private HeadGear _headGear;
    [SerializeField] private TopGear _topGear;
    [SerializeField] private BottomGear _bottomGear;
    [SerializeField] private HandsGear _handsGear;
    [SerializeField] private FeetGear _feetGear;
    [SerializeField] private Weapon _primaryOneHanded, _secondaryOneHanded, _twoHanded;

    [Header("Character Skills")]
    [SerializeField] private List<Skill> _allAquiredSkills;
    [SerializeField] private Skill[] _activeSkills;
    #endregion

    #region properties
    public string Name { get => _name; set => _ = value; }
    public int MaxLevel => _maxLevel;
    public int CurrentLevel { get => _currentLevel; set => _ = value; }
    public float MaxExp { get => _maxExp; set => _ = value; }
    public float CurrentExp { get => _currentExp; set => _ = value; }
    public int MaxHealth { get => _maxHealth; set => _ = value; }
    public int CurrentHealth { get => _currentHealth; set => _ = value; }
    public int MaxMana { get => _maxMana; set => _ = value; }
    public int CurrentMana { get => _currentMana; set => _ = value; }
    public int Vigor { get => _vigor; set => _ = value; }
    public int Strength { get => _strength; set => _ = value; }
    public int Intelligence { get => _intelligence; set => _ = value; }
    public int Speed { get => _speed; set => _ = value; }
    public int TotalSkillPoints { get => _totalSkillPoints; set => _ = value; }
    public int SpentSkillPoints { get => _spentSkillPoints; set => _ = value; }
    public int AvailableSkillPoints { get => _availableSkillPoints; set => _ = value; }
    public int TotalStatPoints { get => _totalStatPoints; set => _ = value; }
    public int SpentStatPoints { get => _spentStatPoints; set => _ = value; }
    public int AvailableStatPoints { get => _availableStatPoints; set => _ = value; }
    //public Texture2D SpriteSheet => _spriteSheet;
    public Weapon PrimaryOneHanded { get => _primaryOneHanded; set => _ = value; }
    public Weapon SecondaryOneHanded { get => _secondaryOneHanded; set => _ = value; }
    public Weapon TwoHanded { get => _twoHanded; set => _ = value; }
    public HeadGear @HeadGear { get => _headGear; set => _ = value; }
    public TopGear @TopGear { get => _topGear; set => _ = value; }
    public BottomGear @BottomGear { get => _bottomGear; set => _ = value; }
    public HandsGear @HandsGear { get => _handsGear; set => _ = value; }
    public FeetGear @FeetGear { get => _feetGear; set => _ = value; }
    public List<Skill> AllAquiredSkills { get => _allAquiredSkills; set => _ = value; }
    public Skill[] ActiveSkills { get => _activeSkills; set => _ = value; }
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
