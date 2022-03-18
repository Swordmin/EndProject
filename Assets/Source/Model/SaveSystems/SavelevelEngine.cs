using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveLevelEngine : MonoBehaviour, ISaveSystem
{

    public static SaveLevelEngine SaveEngine;

    [SerializeField] private SaveData _saveData = new SaveData();
    [SerializeField] private ISaveSystem _core;
    [SerializeField] private readonly string _filePath = Application.persistentDataPath + "/Save.json";

    public SaveData Load()
    {
        return _core.Load();
    }

    public void Save()
    {
        _core.Save();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (!SaveEngine)
            SaveEngine = this;
        _core = new SaveSystemJson(_filePath);
    }

    private void Start()
    {
        GlobalResources.Resources.OnStarsChange.AddListener((n) => _saveData.Stars = n);
        GlobalResources.Resources.OnCoinsChange.AddListener((n) => _saveData.Coins = n);
    }
    public Level FindLevelByID(string id)
    {
        if (id == "")
            Debug.LogWarning("SFS ERROR: Id  is enmpty");

        foreach (Level level in _saveData.Levels)
        {
            if (level.Id == id)
                return level;
        }
        Debug.LogError("SFS ERROR: Level not found");
        return new Level();
    }

    public void OpenLevelByID(string id)
    {
        if (id == "")
            Debug.LogWarning("SFS ERROR: Id  is enmpty");

        Level level = FindLevelByID(id);
        level.Can = true;
    }

    public void SetBestTimeLevelByID(string id, float time)
    {
        if (id == "")
            Debug.LogWarning("SFS ERROR: Id  is enmpty");

        Level level = FindLevelByID(id);
        level.BestTime = time;
    }
    public void SetDoneLevelByID(string id)
    {
        if (id == "")
            Debug.LogWarning("SFS ERROR: Id  is enmpty");

        Level level = FindLevelByID(id);
        level.Done = true;
    }
    public void SetStarsLevelByID(string id, bool[] stars)
    {
        if (id == "")
            Debug.LogWarning("SFS ERROR: Id  is enmpty");

        Level level = FindLevelByID(id);
        level.Stars = stars;
    }


}

[Serializable]
public class SaveData
{
    public List<Level> Levels = new List<Level>();
    public float Coins;
    public float Stars;
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
