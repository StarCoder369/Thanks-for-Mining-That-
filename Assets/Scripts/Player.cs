using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 10f;
    public float acceleration = 25f;
    public float deadZone = 0.25f;
    public float maxDistance = 5f;
    public float turnSpeed = 180f;

    [Header("Input Switching")]
    public float mouseMoveThreshold = 1f;

    private Rigidbody2D rb;
    private Vector2 lastMousePos;

    private enum InputMode
    {
        Mouse,
        Keyboard
    }

    private InputMode currentMode = InputMode.Mouse;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 4f;

        if (Mouse.current != null) lastMousePos = Mouse.current.position.ReadValue();
    }

    private void FixedUpdate()
    {
        UpdateInputMode();

        if (currentMode == InputMode.Mouse) MouseMovement();
        else KeyboardMovement();
    }

    private void UpdateInputMode()
    {
        bool keyboardActive =
            Keyboard.current != null &&
            (
                Keyboard.current.wKey.isPressed ||
                Keyboard.current.aKey.isPressed ||
                Keyboard.current.sKey.isPressed ||
                Keyboard.current.dKey.isPressed ||
                Keyboard.current.upArrowKey.isPressed ||
                Keyboard.current.downArrowKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed
            );

        if (keyboardActive)
        {
            currentMode = InputMode.Keyboard;
            return;
        }

        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        if ((mousePos - lastMousePos).sqrMagnitude > mouseMoveThreshold * mouseMoveThreshold)
        {
            currentMode = InputMode.Mouse;
        }

        lastMousePos = mousePos;
    }

    private void MouseMovement()
    {
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 toMouse = mouseWorldPos - rb.position;
        float distance = toMouse.magnitude;

        if (distance > 0.001f)
        {
            float angle = Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg;
            float newAngle = Mathf.MoveTowardsAngle(
                rb.rotation,
                angle,
                turnSpeed * Time.fixedDeltaTime);

            rb.MoveRotation(newAngle);
        }

        if (distance > deadZone)
        {
            Vector2 direction = toMouse.normalized;
            float strength = Mathf.InverseLerp(deadZone, maxDistance, distance);

            rb.AddForce(direction * (acceleration * strength), ForceMode2D.Force);

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }

    private void KeyboardMovement()
    {
        if (Keyboard.current == null) return;

        float turnInput = 0f;
        float moveInput = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) turnInput += 1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) turnInput -= 1f;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveInput += 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveInput -= 1f;

        if (Mathf.Abs(turnInput) > 0.01f)
        {
            rb.MoveRotation(rb.rotation + turnInput * turnSpeed * Time.fixedDeltaTime);
        }

        if (Mathf.Abs(moveInput) > 0.01f)
        {
            rb.AddForce((Vector2)transform.right * moveInput * acceleration, ForceMode2D.Force);

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }

    public void Die()
    {
        Debug.Log("Player has died");
    }
}