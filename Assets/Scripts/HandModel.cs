using UnityEngine;

public class HandModel : MonoBehaviour
{
    [SerializeField] private float smoothness;
    [SerializeField] private Transform phys;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, phys.position, smoothness * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, phys.rotation, smoothness * Time.deltaTime);
    }
}
