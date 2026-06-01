using UnityEngine;
using UnityEngine.InputSystem;

public class CinematicFreeCamera : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float boostSpeed = 40f;

    [Header("Smoothing")]
    [SerializeField] private float positionSmoothTime = 0.15f;
    [SerializeField] private float rotationSmoothTime = 0.05f;

    [Header("Look")]
    [SerializeField] private float mouseSensitivity = 0.15f;

    private Vector3 targetPosition;
    private Vector3 positionVelocity;
    private Vector3 direction;

    private float targetYaw;
    private float targetPitch;
    private float currentYaw;
    private float currentPitch;
    private float yawVelocity;
    private float pitchVelocity;

    private void Start()
    {
        targetPosition = transform.position;
        targetYaw = currentYaw = transform.eulerAngles.y;
        targetPitch = currentPitch = transform.eulerAngles.x;
    }

    private void Update()
    {
        HandleLook();
        HandleMovement();
        ApplySmoothing();
    }

    private void HandleLook()
    {
        if (!Mouse.current.rightButton.isPressed)
            return;

        Vector2 delta = Mouse.current.delta.ReadValue();

        targetYaw += delta.x * mouseSensitivity;
        targetPitch -= delta.y * mouseSensitivity;
        targetPitch = Mathf.Clamp(targetPitch, -89f, 89f);
    }

    private void HandleMovement()
    {
        float speed = Keyboard.current.leftShiftKey.isPressed ? boostSpeed : moveSpeed;

        targetPosition += Quaternion.LookRotation(transform.forward) * direction.normalized * speed * Time.deltaTime;
    }

    private void ApplySmoothing()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref positionVelocity,
            positionSmoothTime);

        currentYaw = Mathf.SmoothDampAngle(currentYaw, targetYaw, ref yawVelocity, rotationSmoothTime);
        currentPitch = Mathf.SmoothDampAngle(currentPitch, targetPitch, ref pitchVelocity, rotationSmoothTime);

        transform.rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
    }
    
    public void SetInputMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
}