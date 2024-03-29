using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class SleepersGenerator : PathSceneTool
{
    [SerializeField] private GameObject[] sleeperPrefabs;
    [SerializeField] private GameObject holder;
    [SerializeField] private float spacing = 3;
    [SerializeField] private float sleepersYOffset = 0.25f;
    [SerializeField] private float randomizeSpacing = 0.1f;
    [SerializeField] private float randomizeRotation = 0.1f;

    private const float minSpacing = .1f;

    private void Generate()
    {
        if (pathCreator != null && sleeperPrefabs != null && sleeperPrefabs.Length > 0 && holder != null)
        {
            DestroyObjects();

            VertexPath path = pathCreator.path;

            spacing = Mathf.Max(minSpacing, spacing);
            float dst = 0;

            while (dst < path.length)
            {
                Vector3 point = path.GetPointAtDistance(dst) - path.GetNormalAtDistance(dst) * sleepersYOffset;
                Quaternion rot = path.GetRotationAtDistance(dst);
                rot = Quaternion.Lerp(rot, Random.rotation, randomizeRotation);
                Instantiate(sleeperPrefabs[Random.Range(0, sleeperPrefabs.Length)], point, rot, holder.transform);
                dst += spacing + Random.Range(-randomizeSpacing, randomizeSpacing);
            }
        }
    }

    private void DestroyObjects()
    {
        int numChildren = holder.transform.childCount;
        for (int i = numChildren - 1; i >= 0; i--)
        {
            DestroyImmediate(holder.transform.GetChild(i).gameObject, false);
        }
    }

    protected override void PathUpdated()
    {
        if (pathCreator != null)
        {
            Generate();
        }
    }
}
