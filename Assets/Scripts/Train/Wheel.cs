using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    private Train train = null;

    private void OnEnable()
    {
        train = GetComponentInParent<Train>();
        if (train != null)
            train.RegisterWheel(this);
    }

    private void OnDisable()
    {
        if (train != null)
            train.UnregisterWheel(this);
    }

    public void Rotate(float delta)
    {
        if (radius <= 0) return;

        float wheelLength = 2 * Mathf.PI * radius;
        float rotCount = delta / wheelLength;
        float degRot = 360f * rotCount;

        transform.Rotate(rotationAxis, degRot);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Color c = Gizmos.color;
        Gizmos.color = new Color(0.4f, 0.9f, 0.8f);
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = c;
    }
#endif
}
