using System.IO;
using UnityEngine;


public class SaveSystemJson : MonoBehaviour, ISaveSystem
{
    private string _filePath;
    private SaveData _saveData = new SaveData();

    public SaveSystemJson(string FilePath) 
    {
        _filePath = FilePath;
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
            while ((line = reader.ReadLine()) != null)
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
