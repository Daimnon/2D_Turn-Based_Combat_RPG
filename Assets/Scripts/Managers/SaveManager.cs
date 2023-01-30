using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string _savePath = @"D:\Users\Daimnon\Documents\GitHub\2D_Turn-Based_Combat_RPG\Assets\SaveFiles\SaveData.json";

    private SaveData _saveData;

    private void Awake()
    {
        _saveData = new SaveData();
        Debug.Log(_savePath);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
            SaveData();
        if (Input.GetKey(KeyCode.L))
            LoadData();
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
        using StreamReader reader = new(_savePath);

        string json = reader.ReadToEnd();
        _saveData = JsonUtility.FromJson<SaveData>(json);
        Debug.Log(_saveData.ToString());
    }

    private void UpdateSaveData()
    {
        _saveData.PlayerParty = PartyManager.Instance.PlayerParty;
        _saveData.EnemyParty = PartyManager.Instance.EnemyParty;
        _saveData.PotentialAllies = PartyManager.Instance.PotentialAllies;
        _saveData.ChosenAllies = PartyManager.Instance.ChosenAllies;
    }

    public override string ToString()
    {
        string toString = $"{_saveData.PlayerParty}";
        return toString;
    }
}
