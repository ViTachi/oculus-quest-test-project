using PathCreation;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private float yOffset = 0f;
    [SerializeField] private float targetSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    [SerializeField] private Transform trainBody;

    private float distance = 0f;
    private float currentSpeed = 0f;

    public float TargetSpeed => targetSpeed;
    public float CurrentSpeed => currentSpeed;

    private List<Wheel> wheels = new List<Wheel>();

    public void SetTargetSpeed(float targetSpeed) => this.targetSpeed = targetSpeed;

    public void RegisterWheel(Wheel wheel) => wheels.Add(wheel);
    public void UnregisterWheel(Wheel wheel) => wheels.Remove(wheel);

    private void OnEnable()
    {
        if (pathCreator != null)
            distance = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        UpdateTrainPosition();
    }

    [Button]
    public void SetTrainAtDistance(float distance)
    {
        this.distance = Mathf.Clamp(distance, 0f, pathCreator.path.length);
        UpdateTrainPosition();
    }

    private void UpdateTrainPosition()
    {
        if (pathCreator == null) return;
        transform.position = pathCreator.path.GetPointAtDistance(distance, endOfPathInstruction) + Vector3.up * yOffset;
        trainBody.rotation = pathCreator.path.GetRotationAtDistance(distance, endOfPathInstruction);
    }

    private void RotateWheels(float delta)
    {
        foreach (var wheel in wheels)
        {
            if (wheel.gameObject.activeInHierarchy)
                wheel.Rotate(delta);
        }
    }

    private void Update()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        float delta = currentSpeed * Time.deltaTime;
        RotateWheels(delta);
        distance += delta;
        UpdateTrainPosition();
    }
}
