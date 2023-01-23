using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;
    public static SceneManager Instance => _instance;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadCombatScene(int combatScene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(combatScene);
    }

    public void OnCombatStart()
    {
        CombatManager.Instance.Initialize(0);
    }
}
