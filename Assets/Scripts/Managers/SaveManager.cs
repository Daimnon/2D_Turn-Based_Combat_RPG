using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    public static SaveManager Instance => _instance;

    //private const string _savePath = @"D:\Users\Daimnon\Documents\GitHub\2D_Turn-Based_Combat_RPG\Assets\SaveFiles\SaveData.json";
    private string _savePath;
    private SaveData _saveData;

    private void Awake()
    {
        _instance = this;
        _saveData = new();
        _savePath = $"{Application.dataPath}/SaveData.json";
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
            SaveData();

        if (Input.GetKey(KeyCode.L))
            LoadData();
    }

    private void UpdateSaveData()
    {
        _saveData.Player = GameManager.Instance.PlayerCharacter.Data;

        for (int i = 0; i < PartyManager.Instance.ChosenAllies.Length -1; i++)
            _saveData.ChosenAllies[i] = PartyManager.Instance.ChosenAllies[i].Data;

        for (int i = 0; i < PartyManager.Instance.PotentialAllies.Count; i++)
            _saveData.PotentialAllies[i] = PartyManager.Instance.PotentialAllies[i].Data;
    }
    private void UpdateLoadData()
    {
        GameManager.Instance.PlayerCharacter.Data = _saveData.Player;

        for (int i = 0; i < _saveData.ChosenAllies.Length -1; i++)
            PartyManager.Instance.ChosenAllies[i].Data = _saveData.ChosenAllies[i];

        for (int i = 0; i < _saveData.PotentialAllies.Count; i++)
            PartyManager.Instance.PotentialAllies[i].Data = _saveData.PotentialAllies[i];
    }

    public void SaveData()
    {
        UpdateSaveData();
        using StreamWriter writer = new(_savePath);
        string json = JsonUtility.ToJson(_saveData);
        writer.Write(json);

        Debug.Log($"Saved data in {_savePath}");
        Debug.Log(json);
    }
    public void LoadData()
    {
        _saveData = new();
        using StreamReader reader = new(_savePath);
        string json = reader.ReadToEnd();
        _saveData = JsonUtility.FromJson<SaveData>(json);
        UpdateLoadData();

        Debug.Log($"Read data from {_savePath}");
        Debug.Log(json);
    }
}
