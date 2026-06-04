using UnityEngine;
using UnityEngine.InputSystem;

public class WreckingBall : MonoBehaviour
{
    // =============== All commented parts are not needed anymore ====================
    // ========================== Will be removed later ==============================
    // public Transform anchor;

    // [Header("Rope")]
    // public float ropeLength = 5f;
    // public float attachDistance = 0.75f;
    // public float anchorInfluence = 2f;
    // public float reelForce = 50f;
    // public float ropeStrength = 200f;

    // [Header("Physics")]
    // public float damping = 0.1f;
    // public float maxSpeed = 50f;

    [Header("Gameplay Stuff")]
    public float dmg;

    Rigidbody2D rb;

    // bool attached = true;
    // public bool ropeEnabled = true;

    // Vector2 lastAnchorPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // if (anchor != null)
        //     lastAnchorPos = anchor.position;
    }

    // void Update()
    // {
    //     bool pressed = (Keyboard.current != null && Keyboard.current.spaceKey.wasReleasedThisFrame) || (Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame);

    //     if (pressed)
    //     {
    //         ropeEnabled = !ropeEnabled;

    //         if (attached)
    //         {
    //             attached = false;
    //             Boost();
    //             Boost();
    //         }
    //     }
    // }

    // public void Boost()
    // {
    //     rb.AddForce(rb.linearVelocity / 2f, ForceMode2D.Impulse);
    // }

    // void FixedUpdate()
    // {
    //     if (anchor == null)
    //         return;

    //     Vector2 anchorVelocity = ((Vector2)anchor.position - lastAnchorPos) / Time.fixedDeltaTime;
    //     lastAnchorPos = anchor.position;

    //     Vector2 offset = rb.position - (Vector2)anchor.position;
    //     float distance = offset.magnitude;

    //     if (!attached && ropeEnabled)
    //     {
    //         Vector2 ropeDir = offset.normalized;

    //         rb.AddForce(-ropeDir * reelForce, ForceMode2D.Force);

    //         float outwardVelocity = Vector2.Dot(rb.linearVelocity, ropeDir);

    //         if (outwardVelocity > 0f)
    //             rb.linearVelocity -= ropeDir * outwardVelocity * 1.75f;

    //         if (distance <= attachDistance)
    //         {
    //             rb.AddForce(-rb.linearVelocity / 2, ForceMode2D.Impulse);
    //             attached = true;
    //         }
    //         float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

    //         rb.SetRotation(angle + 180f);
    //     }

    //     if (attached && ropeEnabled)
    //     {
    //         Vector2 ropeDir = offset.normalized;
    //         Vector2 tangent = new Vector2(-ropeDir.y, ropeDir.x);

    //         float radialSpeed = Vector2.Dot(anchorVelocity, ropeDir);
    //         float tangentialSpeed = Vector2.Dot(anchorVelocity, tangent);

    //         float radialInfluence = 0.1f;
    //         float tangentialInfluence = 1f;

    //         float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

    //         rb.SetRotation(angle + 180f);

    //         Vector2 anchorForce =
    //             ropeDir * radialSpeed * radialInfluence +
    //             tangent * tangentialSpeed * tangentialInfluence;

    //         rb.AddForce(anchorForce * anchorInfluence, ForceMode2D.Force);

    //         float outwardVelocity = Vector2.Dot(rb.linearVelocity, ropeDir);

    //         if (distance > ropeLength)
    //         {
    //             float stretch = distance - ropeLength;

    //             rb.AddForce(
    //                 -ropeDir * stretch * ropeStrength,
    //                 ForceMode2D.Force);

    //             float positionCorrection = 0.15f;
    //             rb.position -= ropeDir * stretch * positionCorrection;

    //             if (outwardVelocity > 0f)
    //                 rb.linearVelocity -= ropeDir * outwardVelocity;
    //         }
    //         else
    //         {
    //             float t = 1f - distance;
    //             float pushStrength = ropeStrength * t * t / 3; // stronger the closer it gets

    //             rb.AddForce(ropeDir * pushStrength, ForceMode2D.Force);
    //         }
    //     }
    //     rb.AddForce(-rb.linearVelocity * damping, ForceMode2D.Force);
    //     rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    // }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Meteor"))
        {
            Vector2 normal = collision.contacts[0].normal;
            rb.linearVelocity = Vector2.Reflect(rb.linearVelocity * 2, normal);

            Asteroid asteroid = collision.collider.GetComponent<Asteroid>();

            GameObject hitEffect = Instantiate(asteroid.hitEffect, collision.contacts[0].point, Quaternion.identity);
            Destroy(hitEffect, 4f);
            if (asteroid != null)
                asteroid.TakeDamage(dmg);
        }

        if (collision.collider.CompareTag("Player"))
            collision.collider.GetComponent<Player>().Die();

        if (collision.collider.CompareTag("Enemy"))
            collision.collider.GetComponent<Enemy>().Die();
    }

    // #if UNITY_EDITOR
    //     void OnDrawGizmosSelected()
    //     {
    //         if (anchor == null)
    //             return;

    //         Gizmos.color = attached ? Color.green : Color.red;
    //         Gizmos.DrawWireSphere(anchor.position, ropeLength);

    //         Gizmos.color = Color.yellow;
    //         Gizmos.DrawWireSphere(anchor.position, attachDistance);

    //         Gizmos.color = Color.white;
    //         Gizmos.DrawLine(anchor.position, transform.position);
    //     }
    // #endif
}