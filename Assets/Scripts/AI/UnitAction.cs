using System;
using System.Collections;
using UnityEngine;

public abstract class UnitAction<T> : MonoBehaviour
{
    private bool isInProgress = false;

    public bool IsInProgress => isInProgress;

    public event Action<UnitAction<T>> OnActionStart;
    public event Action<UnitAction<T>> OnActionStop;
    public event Action<UnitAction<T>> OnActionComplete;

    public virtual void StartAction(T paramters = default)
    {
        if (isInProgress)
            StopAction();

        isInProgress = true;
        OnActionStart?.Invoke(this);

#if UNITY_EDITOR
        gameObject.name = $"{gameObject.name}(Active)";
#endif
    }

    public virtual void StopAction()
    {
        isInProgress = false;
        OnActionStop?.Invoke(this);

#if UNITY_EDITOR
        gameObject.name = gameObject.name.Replace("(Active)", string.Empty);
#endif
    }

    public virtual void CompleteAction()
    {
        StopAction();
        OnActionComplete?.Invoke(this);
    }

    public IEnumerator WaitForComplete()
    {
        yield return new WaitWhile(() => isInProgress);
    }
}

public abstract class UnitAction : UnitAction<object>
{

}
