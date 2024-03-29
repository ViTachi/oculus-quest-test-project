using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitSettings
{
    [JsonIgnore, SerializeField] private List<UnitTypeData> unitsData;

    [System.Serializable]
    public class UnitTypeData
    {
        public UnitType UnitType;
        public List<Unit> unitPrefabs;
    }


    public Unit GetUnit(string unitTypeId, int level)
    {
        var unitTypeData = unitsData.Find(x => x.UnitType.Id == unitTypeId);
        if (unitTypeData == null) 
        {
            Debug.Log($"{unitTypeId} unit type is not found!");
            return null;
        }
        return GetUnit(unitTypeData.UnitType, level);
    }

    public Unit GetUnit(UnitType unitType, int level)
    {
        var typeData = unitsData.Find(x => x.UnitType == unitType);
        if (typeData == null)
        {
            Debug.Log($"{unitType} unit type is not found!");
            return null;
        }
        if (level > typeData.unitPrefabs.Count)
        {
            Debug.Log($"{unitType} unit level {level} is not found!");
            return typeData.unitPrefabs[typeData.unitPrefabs.Count - 1];
        }
        return typeData.unitPrefabs[level - 1];
    }
}
