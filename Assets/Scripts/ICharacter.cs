using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public void OpenSkillMenu();
    public void DoSkill(int slotNum);
    public void Die();
}
