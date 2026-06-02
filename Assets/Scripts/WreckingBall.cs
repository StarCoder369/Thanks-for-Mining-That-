using UnityEngine;
using UnityEngine.InputSystem;

public class WreckingBall : MonoBehaviour
{
    public Transform anchor;

    public float springStrength;
    public float damping;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 offset = anchor.position - transform.position;

        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            rb.AddForce(offset * springStrength);
        }

        rb.AddForce(-rb.linearVelocity * damping);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Die();
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().Die();
        }
    }
}
