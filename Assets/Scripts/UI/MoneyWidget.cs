using DG.Tweening;
using UnityEngine;

public class MoneyWidget : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text moneyCountLabel;

    private PlayerSaveData playerSaveData = null;

    private Tweener tweener = null;

    private void OnEnable()
    {
        playerSaveData = SaveData.Instance.PlayerSaveData;
        UpdateCoinsCount();
        if (playerSaveData != null)
            playerSaveData.OnCoinsCountChanged += UpdateCoinsCount;
    }

    private void OnDisable()
    {
        if (playerSaveData != null)
            playerSaveData.OnCoinsCountChanged -= UpdateCoinsCount;

        DOTween.Kill(this);
    }

    public void UpdateCoinsCount()
    {
        if (playerSaveData == null) return;
        moneyCountLabel.text = $"{playerSaveData.CoinsCount}";

        tweener?.Kill();
        transform.localScale = Vector3.one;
        tweener = transform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 1, 1)
            .SetUpdate(true)
            .SetId(this);
    }
}
