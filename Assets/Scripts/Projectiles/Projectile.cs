using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Unit target;
    protected Vector3 startPosition;
    protected Vector3 targetPosition;
    protected DamageData damage;

    public Action OnReachTarget;

    public virtual void Launch(Unit target, DamageData damage)
    {
        startPosition = transform.position;
        this.target = target;
        this.damage = damage;
    } 
}
