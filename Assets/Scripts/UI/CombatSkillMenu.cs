using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatSkillMenu : MonoBehaviour
{
    [field: SerializeField] public Image CenterImage { get; set; } 
    [field:SerializeField] public TextMeshProUGUI LevelTMProUGUI { get; set; }
    [field:SerializeField] public GameObject SkillsParent { get; set; }
    [field:SerializeField] public Button[] Skills { get; set; }

    private string _levelText = "Lvl.";
    public string LevelText => _levelText;
}
