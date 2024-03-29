using System;
using Newtonsoft.Json;

public class PlayerSaveData
{
    [JsonProperty] private int coinsCount = 0;

    public event Action<int> OnCoinsCountChanged;

    [JsonIgnore] public int CoinsCount => coinsCount;

    public void ChangeCoinsCount(int delta)
    {
        coinsCount += delta;
        OnCoinsCountChanged?.Invoke(coinsCount);
    }
}
