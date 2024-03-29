using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>(FindObjectsInactive.Include);
                if (_instance != null)
                {
                    _instance.Setup();
                }
                else
                {
                    var go = new GameObject($"[{typeof(T)}]");
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            Setup();
        }
    }

    protected abstract void Setup();
}