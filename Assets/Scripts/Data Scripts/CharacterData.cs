using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObject/Data/Character Data", order = 20)]
public class CharacterData : ScriptableObject
{
    #region back fields
    [SerializeField] protected string _name;
    [SerializeField] protected int _currentLevel;
    [SerializeField] protected int _maxHealth, _currentHealth, _maxMana, _currentMana;
    [SerializeField] protected int _vigor, _strength, _intelligence, _speed;
    //[SerializeField] private Texture2D _spriteSheet;

    [Header("Character Equipment")]
    [SerializeField] protected HeadGear _headGear;
    [SerializeField] protected TopGear _topGear;
    [SerializeField] protected BottomGear _bottomGear;
    [SerializeField] protected HandsGear _handsGear;
    [SerializeField] protected FeetGear _feetGear;
    [SerializeField] protected Weapon _primaryOneHanded, _secondaryOneHanded, _twoHanded;

    [Header("Character Skills")]
    [SerializeField] protected Skill[] _activeSkills;
    #endregion

    #region properties
    public string Name { get => _name; set => _name = value; }
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public int MaxMana { get => _maxMana; set => _maxMana = value; }
    public int CurrentMana { get => _currentMana; set => _currentMana = value; }
    public int Vigor { get => _vigor; set => _vigor = value; }
    public int Strength { get => _strength; set => _strength = value; }
    public int Intelligence { get => _intelligence; set => _intelligence = value; }
    public int Speed { get => _speed; set => _speed = value; }
    //public Texture2D SpriteSheet => _spriteSheet;
    public Weapon PrimaryOneHanded { get => _primaryOneHanded; set => _primaryOneHanded = value; }
    public Weapon SecondaryOneHanded { get => _secondaryOneHanded; set => _secondaryOneHanded = value; }
    public Weapon TwoHanded { get => _twoHanded; set => _twoHanded = value; }
    public HeadGear @HeadGear { get => _headGear; set => _headGear = value; }
    public TopGear @TopGear { get => _topGear; set => _topGear = value; }
    public BottomGear @BottomGear { get => _bottomGear; set => _bottomGear = value; }
    public HandsGear @HandsGear { get => _handsGear; set => _handsGear = value; }
    public FeetGear @FeetGear { get => _feetGear; set => _feetGear = value; }
    public Skill[] ActiveSkills { get => _activeSkills; set => _activeSkills = value; }
    #endregion
}
