using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class GameSettingsData
{
    public static GameSettingsData Instance => GameData.Instance.GameSettings.GameSettingsData;


    [JsonProperty, SerializeField] private MainSettings mainSettings;
    [JsonIgnore] public MainSettings MainSettings => mainSettings;


    [JsonProperty, SerializeField] private UnitSettings unitSettings;
    [JsonIgnore] public UnitSettings UnitSettings => unitSettings;


    [JsonProperty, SerializeField] private TrainSettings trainSettings;
    [JsonIgnore] public TrainSettings TrainSettings => trainSettings;
}
