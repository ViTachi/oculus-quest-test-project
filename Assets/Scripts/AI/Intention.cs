using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Intention : MonoBehaviour
{
    [SerializeField] private int priority = 0;

    protected AIPlayer aIPlayer;

    private int currentPriority = 0;
    private bool isCompleted = false;
    private bool isRunning = false;

    public int Priority => currentPriority;
    public bool IsCompleted => isCompleted;
    public bool IsRunning => isRunning;

    public event Action<Intention> OnActivate;
    public event Action<Intention> OnComplete;
    public event Action<Intention> OnStop;

    private List<UnitAction<object>> startedActions = new List<UnitAction<object>>();

    public virtual void Initialize(AIPlayer aIPlayer)
    {
        this.aIPlayer = aIPlayer;
        currentPriority = priority;
    }
    public void SetPriority(int priority) => currentPriority = priority;
    public void ResetPriority() => currentPriority = priority;
    public abstract bool IsNeedToBeActivated();

    public virtual void Activate()
    {
        isCompleted = false;
        isRunning = true;
        OnActivate?.Invoke(this);

#if UNITY_EDITOR
        gameObject.name = $"{gameObject.name}(Active)";
#endif
    }
    public virtual void Run() { }
    public virtual void Stop()
    {
        isRunning = false;
        ResetPriority();
        OnStop?.Invoke(this);

        while (startedActions.Count > 0)
        {
            UnitAction<object> unitAction = startedActions[0];
            startedActions.RemoveAt(0);
            unitAction.StopAction();
        }

#if UNITY_EDITOR
        gameObject.name = gameObject.name.Replace("(Active)", string.Empty);
#endif
    }

    public virtual void Complete()
    {
        Stop();
        isCompleted = true;
        OnComplete?.Invoke(this);
    }

    protected virtual void OnDisable()
    {
        if (isRunning)
            Stop();
    }

    protected void StartAction(UnitAction<object> action, object parameters = null)
    {
        startedActions.Add(action);
        action.StartAction(parameters);
        action.OnActionStop += OnActionStoped;
    }

    protected void OnActionStoped(UnitAction<object> action)
    {
        startedActions.Remove(action);
        action.OnActionStop -= OnActionStoped;
    }
}
