using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    Vector2 mouseWorldPos;

    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
    }

    void FixedUpdate()
    {
        transform.position = mouseWorldPos;
    }
}
