using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class TrainSaveData
{
    [JsonProperty] private List<TrainSlotData> slotsData = new List<TrainSlotData>();

    public System.Action<TrainSlotData> OnSlotUpdated;

    [JsonIgnore] public IReadOnlyList<TrainSlotData> SlotsData 
    {
        get
        {
            if (slotsData.Count == 0)
            {
                foreach (var slotData in GameSettingsData.Instance.TrainSettings.StartSlotsData)
                {
                    var trainSlotData = new TrainSlotData(slotData.SlotType, slotData.UnitType.Id);
                    trainSlotData.SetLevel(slotData.Level);
                    slotsData.Add(trainSlotData);
                }
            }
            return slotsData;
        }
    }

    public class TrainSlotData
    {
        [JsonProperty, SerializeField] private TrainSlotType slotType;
        [JsonProperty, SerializeField] private string unitTypeId;
        [JsonProperty, SerializeField] private int slotLevel;

        [JsonIgnore] public TrainSlotType SlotType => slotType;
        [JsonIgnore] public string UnitTypeId => unitTypeId;
        [JsonIgnore] public int SlotLevel => slotLevel;

        public TrainSlotData (TrainSlotType slotType, string unitTypeId)
        {
            this.slotType = slotType;
            this.unitTypeId = unitTypeId;
            slotLevel = 1;
        }

        public void Upgrade() => slotLevel++;
        public void SetLevel(int level) => slotLevel = level;
    }

    public TrainSlotData GetSlotData(TrainSlotType slotType)
    {
        foreach (var slotData in SlotsData)
        {
            if (slotData.SlotType == slotType)
                return slotData;
        }
        return null;
    }

    public void BuySlot(TrainSlotType slotType, UnitType unitType)
    {
        if (slotsData.Exists(x => x.SlotType == slotType))
        {
            Debug.LogError($"Slot {slotType} is already buyed!");
            return;
        }

        TrainSlotData trainSlotData = new TrainSlotData(slotType, unitType.Id);
        slotsData.Add(trainSlotData);

        OnSlotUpdated?.Invoke(trainSlotData);
    }

    public void UpgradeSlot(TrainSlotType slotType)
    {
        for (int i = 0; i < SlotsData.Count; i++)
        {
            if (SlotsData[i].SlotType == slotType)
            {
                SlotsData[i].Upgrade();
                return;
            }
        }
    }
}
