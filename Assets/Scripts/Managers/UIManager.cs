using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] private CombatSkillMenu _combatSkillMenu;

    public CombatSkillMenu CombatSkillMenu => _combatSkillMenu;

    private void Awake()
    {
        _instance = this;
    }

    private void PopulateSkill()
    {

    }
    public void RefreshCombatSkillMenuDisplay(Character lastCharacterClickedOn, Vector2 newPosUI, Skill[] activeSkills, int lvl)
    {
        _combatSkillMenu.transform.position = newPosUI;

        for (int i = 0; i < _combatSkillMenu.Skills.Length -1; i++)
        {
            _combatSkillMenu.Skills[i].onClick.RemoveAllListeners();
            _combatSkillMenu.Skills[i].onClick.AddListener(delegate { activeSkills[i].Activate(); });
            Debug.Log($"Populated {_combatSkillMenu.Skills[i].name} successfuly");
        }

        _combatSkillMenu.LevelTMProUGUI.text = $"{_combatSkillMenu.LevelText} {lvl}";

        Color color;
        if (lastCharacterClickedOn is Enemy)
        {
            color = new Color(255f, 0f, 0f, 0.47f);
            _combatSkillMenu.CenterImage.color = color;
        }
        else if (lastCharacterClickedOn is Ally)
        {
            color = new Color(0f, 255f, 0f, 0.47f);
            _combatSkillMenu.CenterImage.color = color;
        }
        else
        {
            color = new Color(255f, 255f, 255f, 0.47f);
            _combatSkillMenu.CenterImage.color = color;
        }
    }
}
