using System.IO;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private string saveName = "SaveData";
    [SerializeField] private string savesFolderName = "Saves";
    public GameSettings GameSettings => gameSettings;

    private SaveData saveData = null;
    public SaveData SaveData
    {
        get
        {
            if(saveData == null) Load();
            return saveData;
        }
    }

    [Button]
    public void Load()
    {
        string json = FilesManager.LoadTextFromFile(Path.Combine(savesFolderName, $"{saveName}.js"));
        if (string.IsNullOrEmpty(json))
        {
            saveData = new SaveData();
        }
        else
        {
            saveData = JsonConvert.DeserializeObject<SaveData>(json);
        }
    }

    [Button]
    public void Save()
    {
        FilesManager.SaveTextToFile(Path.Combine(savesFolderName, $"{saveName}.js"), JsonConvert.SerializeObject(saveData, Formatting.Indented));
    }

    [Button]
    public void Delete()
    {
        FilesManager.DeleteFile(Path.Combine(savesFolderName, $"{saveName}.js"));
    }

    protected override void Setup()
    {
        
    }
}
