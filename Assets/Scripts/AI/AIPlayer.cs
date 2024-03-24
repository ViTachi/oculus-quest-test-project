using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    [Space(10)]
    [LabelText(SdfIconType.PersonFill), LabelWidth(85)]
    [GUIColor(0.8f, 1f, 0.8f, 1f)]
    [SerializeField] private Unit unit;

    [Title("Senses")]
    [LabelText("Move", SdfIconType.ArrowsMove), LabelWidth(85)]
    [GUIColor(1f, 0.8f, 0.6f, 1f)]
    [SerializeField] private Movement movement;

    [LabelText("Vision", SdfIconType.EyeFill), LabelWidth(85)]
    [GUIColor(0.7f, 0.88f, 1f, 1f)]
    [SerializeField] private Vision vision;

    [Title("Intentions")]
    [GUIColor(0.8f, 1f, 1f, 1f)]
    [SerializeField] private List<Intention> intentions;

    private Intention activeIntention = null;

    public Movement Movement => movement;
    public Vision Vision => vision;
    public Unit Unit => unit;

    public Intention ActiveIntention => activeIntention;

    public void SetActive(bool active)
    {
        enabled = active;
    }

    private void Awake()
    {
        movement?.Initialize(this);
        vision?.Initialize(this);

        intentions = intentions.OrderByDescending(x => x.Priority).ToList();

        foreach (var intention in intentions)
        {
            intention.Initialize(this);
        }
    }

    private void OnDisable()
    {
        activeIntention?.Stop();
    }

    private void Update()
    {
        Intention nextIntention = intentions
            .Where(x => x.isActiveAndEnabled
            && x.Priority > (activeIntention?.Priority ?? -1)
            && x.IsNeedToBeActivated())
            .OrderByDescending(x => x.Priority).FirstOrDefault();

        if (nextIntention != null)
            ActivateIntention(nextIntention);

        if (activeIntention != null)
        {
            if (activeIntention.IsCompleted)
                activeIntention = null;
            else
                activeIntention.Run();
        }
    }

    public void ActivateIntention(Intention intention)
    {
        activeIntention?.Stop();
        activeIntention = intention;
        activeIntention.OnStop += OnActiveIntentionStopped;
        activeIntention.Activate();
    }

    private void OnActiveIntentionStopped(Intention intention)
    {
        activeIntention.OnStop -= OnActiveIntentionStopped;
        activeIntention = null;
    }
}
