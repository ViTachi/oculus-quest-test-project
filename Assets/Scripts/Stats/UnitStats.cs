using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    [SerializeField] private List<Stat> stats = new List<Stat>();

    private Dictionary<StatType, Stat> statsByType = new Dictionary<StatType, Stat>();

    public bool IsStatExist(StatType statType) => stats.Exists(x => x.StatType == statType);

    public Stat GetStat(StatType statType)
    {
        if (statsByType.ContainsKey(statType))
            return statsByType[statType];
        else
        {
            Stat stat = stats.FirstOrDefault(x => x.StatType == statType);
            if(stat != null)
            {
                statsByType[statType] = stat;
                return stat;
            }
        }
        return null;
    }

    public void SetStat(StatType statType, float value)
    {
        Stat stat = stats.FirstOrDefault(x => x.StatType == statType);
        if (stat != null)
        {
            stat.SetValue(value);
        }
    }

    public void SetStat(StatTypeData statTypeData, float value)
    {
        Stat stat = stats.FirstOrDefault(x => x.StatTypeData == statTypeData);
        if (stat != null)
        {
            stat.SetValue(value);
        }
        else
        {
            stat = new Stat(statTypeData, value);
            stats.Add(stat);
        }
        statsByType[statTypeData.StatType] = stat;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i] != null && stats[i].StatTypeData != null && !statsByType.ContainsKey(stats[i].StatType))
                statsByType[stats[i].StatType] = stats[i];
        }
        List<StatType> keysToDelete = statsByType.Where(x => !stats.Exists(y => y.StatType == x.Key))
            .Select(x => x.Key).ToList();
        foreach (var key in keysToDelete)
        {
            statsByType.Remove(key);
        }
    }
#endif
}
