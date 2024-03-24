using System.Collections.Generic;
using UnityEngine;

public abstract class Vision : MonoBehaviour
{
    protected bool isResting = false;
    protected AIPlayer aIPlayer;

    protected List<Unit> unitsInVision = new List<Unit>();

    protected int framesFromLastUse = 0;

    public void Initialize(AIPlayer aIPlayer)
    {
        this.aIPlayer = aIPlayer;
    }

    protected bool IsAnyUnitsInVision() => GetUnitsInVision().Count > 0;
    protected int UnitsInVisionCount() => GetUnitsInVision().Count;
    public virtual IReadOnlyList<Unit> GetUnitsInVision()
    {
        if (isResting)
            SetIsResting(false);
        framesFromLastUse = 0;
        return unitsInVision;
    } 
    public abstract void UpdateUnitsInVision();

    private void Update()
    {
        if (isResting) return;

        if (Time.frameCount % Unit.GlobalUnitsGroupsCount == aIPlayer.Unit.GlobalUnitsGroup)
            UpdateUnitsInVision();

        framesFromLastUse++;
        if (framesFromLastUse > 6)
            SetIsResting(true);
    }

    public void SetIsResting(bool isResting)
    {
        if (!isResting)
        {
            UpdateUnitsInVision();
        }
        this.isResting = isResting;
    }
}
