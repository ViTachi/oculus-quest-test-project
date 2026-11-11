using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private InputActionProperty moveAction = new InputActionProperty(new InputAction("Left Hand Move", expectedControlType: "Vector2"));
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cameraTransform;

    private Vector2 moveAxis = Vector2.zero;

    protected void OnEnable()
    {
        moveAction.EnableDirectAction();
    }

    protected void OnDisable()
    {
        moveAction.DisableDirectAction();
    }

    public void Move(CallbackContext callbackContext)
    {
        moveAxis = callbackContext.action?.ReadValue<Vector2>() ?? Vector2.zero;
    }

    private void FixedUpdate()
    {
        
        Vector3 direction = cameraTransform.forward;
        direction.y = 0f;
        direction.Normalize();
        Vector3 moveVector = (moveAxis.y * direction + moveAxis.x * Vector3.Cross(Vector3.up, direction)).normalized * speed;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
    }

    private void Update()
    {
        moveAxis = moveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        Debug.Log(moveAxis);
        //moveAxis = new Vector2(0, 1f);
    }
}
