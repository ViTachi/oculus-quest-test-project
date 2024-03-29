using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MainSettings
{
    [JsonProperty, SerializeField] private int testProp;
    [JsonProperty, SerializeField] private List<string> testProp2;

    [JsonIgnore] public int TestProp => testProp;
    [JsonIgnore] public IReadOnlyList<string> TestProp2 => testProp2;
}
