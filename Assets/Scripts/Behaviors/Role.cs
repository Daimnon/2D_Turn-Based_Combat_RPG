using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType { Player, NPC }
public enum Roles { Warrior, Defender, Enchanter, Rogue }

public class Role : MonoBehaviour
{
    [SerializeField] protected CharacterType _characterType;
    public CharacterType DefinedCharacterType { get => _characterType; set => _characterType = value; }

    [SerializeField] protected Roles _role;
    public Roles DefinedRole { get => _role; set => _role = value; }
}
