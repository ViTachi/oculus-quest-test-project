using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private TextAsset gameSettingsJson;

    [Space(20)]
    [SerializeField] private GameSettingsData gameSettingsData;
    public GameSettingsData GameSettingsData => gameSettingsData;

#if UNITY_EDITOR
    [Button]
    public void LoadDataFromJson()
    {
        RemoteConfigUtils.ApplyRemoteConfig(gameSettingsData, gameSettingsJson.text);
    }

    [Button]
    public void SaveToJson()
    {
        FilesManager.ConvertStringToTextAsset(JsonConvert.SerializeObject(gameSettingsData, Formatting.Indented), "/Data/GameSettings");
    }
#endif
}