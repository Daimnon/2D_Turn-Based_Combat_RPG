using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //private const string _savePath = @"D:\Users\Daimnon\Documents\GitHub\2D_Turn-Based_Combat_RPG\Assets\SaveFiles\SaveData.json";
    private string _savePath;

    private SaveData _saveData;

    private void Awake()
    {
        _saveData = new();
        _savePath = $"{Application.dataPath}/SaveData.json";
    }

    public void SaveDataOne()
    {
        GetSaveData();
        using StreamWriter writer = new(_savePath);
        string json = JsonUtility.ToJson(_saveData);
        writer.Write(json);

        Debug.Log($"Saved data in {_savePath}");
        Debug.Log(json);
    }
    public void LoadDataOne()
    {
        using StreamReader reader = new(_savePath);
        string json = reader.ReadToEnd();
        _saveData = JsonUtility.FromJson<SaveData>(json);
        SetSaveData();

        Debug.Log(_saveData.ToString());
    }

    private void GetSaveData()
    {
        _saveData.PlayerParty = PartyManager.Instance.PlayerParty;
        _saveData.EnemyParty = PartyManager.Instance.EnemyParty;
        _saveData.PotentialAllies = PartyManager.Instance.PotentialAllies;
        _saveData.ChosenAllies = PartyManager.Instance.ChosenAllies;
    }
    private void SetSaveData()
    {
        PartyManager.Instance.PlayerParty = _saveData.PlayerParty;
        PartyManager.Instance.EnemyParty = _saveData.EnemyParty;
        PartyManager.Instance.PotentialAllies = _saveData.PotentialAllies;
        PartyManager.Instance.ChosenAllies = _saveData.ChosenAllies;
    }
}
