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
    public string Name { get => _name; set => _ = value; }
    public int CurrentLevel { get => _currentLevel; set => _ = value; }
    public int MaxHealth { get => _maxHealth; set => _ = value; }
    public int CurrentHealth { get => _currentHealth; set => _ = value; }
    public int MaxMana { get => _maxMana; set => _ = value; }
    public int CurrentMana { get => _currentMana; set => _ = value; }
    public int Vigor { get => _vigor; set => _ = value; }
    public int Strength { get => _strength; set => _ = value; }
    public int Intelligence { get => _intelligence; set => _ = value; }
    public int Speed { get => _speed; set => _ = value; }
    //public Texture2D SpriteSheet => _spriteSheet;
    public Weapon PrimaryOneHanded { get => _primaryOneHanded; set => _ = value; }
    public Weapon SecondaryOneHanded { get => _secondaryOneHanded; set => _ = value; }
    public Weapon TwoHanded { get => _twoHanded; set => _ = value; }
    public HeadGear @HeadGear { get => _headGear; set => _ = value; }
    public TopGear @TopGear { get => _topGear; set => _ = value; }
    public BottomGear @BottomGear { get => _bottomGear; set => _ = value; }
    public HandsGear @HandsGear { get => _handsGear; set => _ = value; }
    public FeetGear @FeetGear { get => _feetGear; set => _ = value; }
    public Skill[] ActiveSkills { get => _activeSkills; set => _ = value; }
    #endregion
}
