using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    Vector2 mouseWorldPos;

    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        if (GameManager.Instance.gameRunning)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        transform.position = mouseWorldPos;
    }
}
