using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrollingIntention : Intention
{
    [SerializeField] private List<Transform> patrolPoints;
    [SerializeField] private float waitDuration;

    private List<Vector3> patrolPointsWorldPositions;
    private Coroutine patrolCoroutine = null;

    public override bool IsNeedToBeActivated()
    {
        return true;
    }

    public override void Activate()
    {
        base.Activate();

        if (patrolPointsWorldPositions == null)
            SetPointsWorldPositions();

        if (patrolCoroutine != null) StopCoroutine(patrolCoroutine);
        patrolCoroutine = StartCoroutine(Patrol());
    }

    public override void Stop()
    {
        if (patrolCoroutine != null) StopCoroutine(patrolCoroutine);
        base.Stop();
    }

    private void OnEnable()
    {
        if (patrolPointsWorldPositions == null)
            SetPointsWorldPositions();
    }

    private void SetPointsWorldPositions()
    {
        patrolPointsWorldPositions = patrolPoints.Select(x => x.position).ToList();
    }

    private IEnumerator Patrol()
    {
        if (patrolPoints.Count < 1) yield break;
        int i = 0;
        while (true)
        {
            aIPlayer.Movement.MoveToPoint(patrolPointsWorldPositions[i]);

            yield return new WaitUntil(() => aIPlayer.Movement.IsTargetReached);

            yield return new WaitForSeconds(waitDuration);

            i++;
            if (i >= patrolPointsWorldPositions.Count)
            {
                i = 0;
            }
        }
    }
}
