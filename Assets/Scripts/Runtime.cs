using Sirenix.OdinInspector;
using UnityEngine;

public class Runtime : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;       
    }

    [Button]
    public void AddMoney(int amount)
    {
        SaveData.Instance.PlayerSaveData.ChangeCoinsCount(amount);
    }
}
