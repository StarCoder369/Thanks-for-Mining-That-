using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class WreckingBall : MonoBehaviour
{
    public Transform anchor;

    [Header("Attraction")]
    public float attractForce = 40f;

    [Header("Rope")]
    public float ropeLength = 5f;
    public float ropeStrength = 200f;
    public float attachDistance = 0.75f;

    [Header("Physics")]
    public float damping = 0.5f;
    public float maxSpeed = 50f;

    Rigidbody2D rb;

    bool attached = true;
    bool wasHoldingLastFrame;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        bool holding =
            (Keyboard.current != null && Keyboard.current.spaceKey.isPressed) ||
            (Mouse.current != null && Mouse.current.leftButton.isPressed);

        if (holding && !wasHoldingLastFrame && attached)
        {
            attached = false;
        }

        wasHoldingLastFrame = holding;

        Vector2 offset = (Vector2)anchor.position - rb.position;
        float distance = offset.magnitude;

        if (!attached && holding)
        {
            Vector2 dir = offset.normalized;

            Vector2 desiredVelocity = dir * maxSpeed;
            Vector2 steering = desiredVelocity - rb.linearVelocity;

            rb.AddForce(steering * attractForce);
        }

        if (distance <= attachDistance)
        {
            attached = true;
        }

        if (attached && distance > ropeLength)
        {
            float stretchAmount = distance - ropeLength;

            rb.AddForce(ropeStrength * stretchAmount * offset.normalized, ForceMode2D.Force);
        }

        rb.AddForce(-rb.linearVelocity * damping);

        rb.linearVelocity =
            Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>()?.Die();
        }

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>()?.Die();
        }

        if (collision.CompareTag("Meteor"))
        {
            rb.linearVelocity *= -1f;
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (anchor == null)
            return;

        Gizmos.color = attached ? Color.green : Color.red;
        Gizmos.DrawWireSphere(anchor.position, ropeLength);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(anchor.position, attachDistance);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(anchor.position, transform.position);
    }
#endif
}