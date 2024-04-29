using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TrainCameraFollow : MonoBehaviour
{
    [SerializeField] private float offsetZ;
    [SerializeField] private float offsetY;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveLerpSpeed;
    [SerializeField] private Transform trainBody;

    private void LateUpdate()
    {
        if (trainBody == null) return;

        Vector3 position = trainBody.position - trainBody.forward * offsetZ;
        position.y = trainBody.position.y + offsetY;
        transform.position = Vector3.Lerp(transform.position, position, moveLerpSpeed * Time.deltaTime);


        Vector3 forward = trainBody.forward;
        forward.y = 0f;
        transform.rotation = Quaternion.Lerp(transform.rotation, 
            Quaternion.FromToRotation(Vector3.forward, forward), rotationSpeed * Time.deltaTime);
    }
}
