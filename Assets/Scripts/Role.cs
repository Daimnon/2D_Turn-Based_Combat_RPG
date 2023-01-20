using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType { Player, NPC }
public enum Roles { Warrior, Defender, Enchanter, Rogue }

public class Role : MonoBehaviour
{
    [SerializeField] internal CharacterType _characterType;
    public CharacterType DefinedCharacterType { get => _characterType; set => _ = value; }

    [SerializeField] internal Roles _role;
    public Roles DefinedRole { get => _role; set => _ = value; }
}
