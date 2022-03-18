using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GlobalResources : MonoBehaviour, ISaveSystem
{

    public static GlobalResources Resources;
    private string _filePath;
    [SerializeField] private float _coins;
    [SerializeField] private float _stars;
    [SerializeField] private SaveData _saveData = new SaveData();
    public float Coins => _coins;
    public float Stars => _stars;

    private void Awake()
    {
        _filePath = Application.persistentDataPath + "/Save.json";
        DontDestroyOnLoad(gameObject);
        if (!Resources)
            Resources = this;
        //_saveData = Load();
    }

    public void AddCoins(float count) 
    {
        if(count < 0)
        {
            Debug.LogError("SFS ERROR: Value must be positive");
            return;
        }
        _coins += count;
        _saveData.Coins = _coins;
    }
    public void AddStars(float count)
    {
        if (count < 0)
        {
            Debug.LogError("SFS ERROR: Value must be positive");
            return;
        }
        _stars += count;
    }


    public Level FindLevelByID(string id)
    {
        foreach (Level level in _saveData.Levels)
        {
            if (level.Id == id)
                return level;
        }
        return new Level();
    }

    public void OpenLevelByID(string id) 
    {
        Level level = FindLevelByID(id);
        level.Can = true;
    }

    public void SetBestTimeLevelByID(string id, float time) 
    {
        Level level = FindLevelByID(id);
        level.BestTime = time;
    }
    public void SetDoneLevelByID(string id)
    {
        Level level = FindLevelByID(id);
        level.Done = true;
    }
    public void SetStarsLevelByID(string id, bool[] stars)
    {
        Level level = FindLevelByID(id);
        level.Stars = stars;
    }

    public void Save()
    {
        var json = JsonUtility.ToJson(_saveData);
        using (var writer = new StreamWriter(_filePath)) 
        {
            writer.WriteLine(json);
        }
    }

    public SaveData Load()
    {
        string json = "";
        using (var reader = new StreamReader(_filePath)) 
        {
            string line;
            while((line = reader.ReadLine()) != null) 
            {
                json += line;
            }
        }

        if (string.IsNullOrEmpty(json)) 
        {
            return new SaveData();
        }

        return JsonUtility.FromJson<SaveData>(json);
    }
}
[Serializable]
public class SaveData
{
    public List<Level> Levels = new List<Level>();
    public float Coins;
}

[Serializable]
public class Level
{
    public string Id;
    public string SceneId;
    public float BestTime;
    public float StarsNeed;
    public bool[] Stars = new bool[3];
    public bool Can;
    public bool Done;
}

