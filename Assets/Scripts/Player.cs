using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = mouseWorldPos - rb.position;

        if (Vector2.Distance(mouseWorldPos, transform.position) > 2f)
        {
            rb.linearVelocity = direction.normalized * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void Die()
    {
        Debug.Log("Player has died");
    }
}
