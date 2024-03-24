using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitDeathAction : DeathAction
{
    [SerializeField] private List<Transform> physObjects;

    private List<PhysObjectData> physObjectsData = new List<PhysObjectData>();

    private int deathAnimationHash = Animator.StringToHash("Death");

    public class PhysObjectData
    {
        public Transform physObject;
        public Transform originParent = null;
        public Vector3 originWeaponPosition;
        public Quaternion originWeaponRotation;
        public Rigidbody rb;
    }

    private void Awake()
    {
        foreach (Transform physObject in physObjects)
        {
            PhysObjectData physObjectData = new PhysObjectData()
            {
                physObject = physObject,
                originParent = physObject.parent,
                originWeaponPosition = physObject.localPosition,
                originWeaponRotation = physObject.localRotation,
            };
            physObjectsData.Add(physObjectData);
        }
    }

    public override void StartAction(Parameters parameters = null)
    {
        base.StartAction(parameters);

        parameters.Unit.Animator.SetTrigger(deathAnimationHash);
        StartCoroutine(DestroyDelay(parameters));
    }

    private IEnumerator DestroyDelay(Parameters parameters)
    {
        Movement movement = parameters.Unit.GetComponentInChildren<Movement>();
        if (movement != null)
        {
            movement.Stop();
        }

        foreach (var physObject in physObjectsData)
        {
            physObject.physObject.SetParent(parameters.Unit.transform.parent);
            physObject.rb = physObject.physObject.AddComponent<Rigidbody>();
            physObject.rb.angularDrag = 3f;
            physObject.rb.AddForce(parameters.Unit.transform.forward * 0.6f + Vector3.up * 0.5f, ForceMode.Impulse);
        }

        parameters.Unit.transform.DOMoveY(parameters.Unit.transform.position.y - 2f, 1.5f)
        .SetId(this).SetEase(Ease.Linear).SetDelay(3f);
        yield return new WaitForSeconds(4.5f);

        parameters.Unit.DestroyUnit();
        SetDefaults();
        CompleteAction();
    }

    private void SetDefaults()
    {
        foreach (var physObject in physObjectsData)
        {
            physObject.physObject.SetParent(physObject.originParent);
            physObject.physObject.localPosition = physObject.originWeaponPosition;
            physObject.physObject.localRotation = physObject.originWeaponRotation;
            Destroy(physObject.rb);
        }
    }

    public override void StopAction()
    {
        DOTween.Kill(this);
        base.StopAction();
        StopAllCoroutines();     
    }
}
