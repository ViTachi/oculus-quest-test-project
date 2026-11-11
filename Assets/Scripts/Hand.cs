using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform anchor;
    [SerializeField] private float velocityDamping = 1f;
    [SerializeField] private Rigidbody originRB;

    //[SerializeField] private float m_VelocityDamping;
    //[SerializeField] private float m_AngularVelocityDamping;
    //[SerializeField] private float m_VelocityScale;
    //[SerializeField] private float m_AngularVelocityScale;


    private void FixedUpdate()
    {
        //rb.MovePosition(anchor.position);
        //rb.MoveRotation(anchor.rotation);

        rb.velocity *= (1f - velocityDamping);
        rb.velocity += originRB.velocity;

        var positionDelta = anchor.position - transform.position;
        var velocity = positionDelta / Time.deltaTime;

        rb.velocity += velocity;



        // Do angular velocity tracking

        // Scale initialized velocity by prediction factor
        rb.angularVelocity *= 0f;
        var rotationDelta = anchor.rotation * Quaternion.Inverse(transform.rotation);
        rotationDelta.ToAngleAxis(out var angleInDegrees, out var rotationAxis);
        if (angleInDegrees > 180f)
            angleInDegrees -= 360f;

        if (Mathf.Abs(angleInDegrees) > Mathf.Epsilon)
        {
            var angularVelocity = (rotationAxis * (angleInDegrees * Mathf.Deg2Rad)) / Time.deltaTime;
            rb.angularVelocity += angularVelocity;
        }

    }
}
