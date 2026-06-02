using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = mouseWorldPos - rb.position;
        rb.linearVelocity = direction.normalized * speed;
    }

    public void Die()
    {
        Debug.Log("Player has died");
    }
}
