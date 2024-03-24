using System.Collections.Generic;
using UnityEngine;

public class Visual : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private Animator animator;
    [SerializeField] private List<Anchor> anchors;

    private Dictionary<string, Transform> anchorsByName = new Dictionary<string, Transform>();

    private bool isInitialized = false;

    public Transform Model => model;
    public Animator Animator => animator;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (isInitialized) return;

        foreach (var anchor in anchors)
        {
            anchorsByName[anchor.Name] = anchor.AnchorTransform;
        }

        isInitialized = true;
    }

    [System.Serializable]
    public class Anchor
    {
        public string Name;
        public Transform AnchorTransform;
    }

    public Transform GetAnchor(string name)
    {
        Initialize();
        if (anchorsByName.ContainsKey(name))
            return anchorsByName[name];
        return null;
    }
}
