using Newtonsoft.Json;

public class SaveData
{
    public static SaveData Instance => GameData.Instance.SaveData;
    public static void Save() => GameData.Instance.Save();


    [JsonProperty] private PlayerSaveData playerSaveData = new PlayerSaveData();
    [JsonIgnore] public PlayerSaveData PlayerSaveData => playerSaveData;


    [JsonProperty] private TrainSaveData trainData = new TrainSaveData();
    [JsonIgnore] public TrainSaveData TrainData => trainData;
}
