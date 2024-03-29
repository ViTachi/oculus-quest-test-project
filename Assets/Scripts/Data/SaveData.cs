using Newtonsoft.Json;

public class SaveData
{
    public static SaveData Instance => GameData.Instance.SaveData;
    public static void Save() => GameData.Instance.Save();


    [JsonProperty] private PlayerSaveData accountsData = new PlayerSaveData();
    [JsonIgnore] public PlayerSaveData AccountsData => accountsData;


    [JsonProperty] private TrainSaveData trainData = new TrainSaveData();
    [JsonIgnore] public TrainSaveData TrainData => trainData;
}
