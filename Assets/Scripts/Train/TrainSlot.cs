using UnityEngine;

public class TrainSlot : MonoBehaviour
{
    [SerializeField] private TrainSlotType slotType;
    [SerializeField] private Unit unitInSlot = null;

    public TrainSlotType SlotType => slotType;
   
    private TrainSaveData trainSaveData = null;

    private void OnEnable()
    {
        UpdateSlot();
        trainSaveData = SaveData.Instance.TrainData;
        trainSaveData.OnSlotUpdated += SetupSlot;
    }

    private void OnDisable()
    {
        if (trainSaveData != null)
            trainSaveData.OnSlotUpdated -= SetupSlot;
    }

    public void UpdateSlot()
    {
        SetupSlot(SaveData.Instance.TrainData.GetSlotData(slotType));              
    }

    private void SetupSlot(TrainSaveData.TrainSlotData slotData)
    {
        if (slotData == null) return;

        if (unitInSlot != null)
        {
            Destroy(unitInSlot.gameObject);
            unitInSlot = null;
        }

        var unitPrefab = GameSettingsData.Instance.UnitSettings.GetUnit(slotData.UnitTypeId, slotData.SlotLevel);
        if (unitPrefab == null)
        {
            Debug.Log($"Slot is not setuped! Unit {slotData.UnitTypeId} level {slotData.SlotLevel} not found!");
        }
        unitInSlot = Instantiate(unitPrefab, transform);
    }
}

public enum TrainSlotType
{
    Engine,
    Cabine,
    Pusher,
    Unit1,
    Unit2,
    Unit3,
    Unit4,
    Weapon1,
    Weapon2,
    Weapon3,
    Weapon4
}
