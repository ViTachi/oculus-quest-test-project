using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainSettings
{
    [JsonProperty, SerializeField] private List<TrainSlotData> startSlotsData;
    [JsonIgnore] public IReadOnlyList<TrainSlotData> StartSlotsData => startSlotsData;

    [System.Serializable]
    public class TrainSlotData
    {
        [JsonProperty, SerializeField] private TrainSlotType slotType;
        [JsonProperty, SerializeField] private UnitType unitType;
        [JsonProperty, SerializeField] private int level;

        [JsonIgnore] public TrainSlotType SlotType => slotType;
        [JsonIgnore] public UnitType UnitType => unitType;
        [JsonIgnore] public int Level => level;
    }
}
