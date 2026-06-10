using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public GameObject Explosion;

    [Header("Stats")]
    public float maxHealth = 100f;
    public float dmg = 2f;

    [Header("Movement")]
    public float thrust = 5f;
    public float maxSpeed = 10f;
    public float turnSpeed = 90f; // degrees per second

    float currentHealth;

    public bool normalEnemy = true;

    Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
            target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        Vector2 toTarget = (Vector2)target.position - rb.position;

        float targetAngle =
            Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;

        float newAngle = Mathf.MoveTowardsAngle(
            rb.rotation,
            targetAngle,
            turnSpeed * Time.fixedDeltaTime);

        rb.MoveRotation(newAngle);

        Vector2 desiredVelocity =
            toTarget.normalized * maxSpeed;

        rb.linearVelocity = Vector2.MoveTowards(
            rb.linearVelocity,
            desiredVelocity,
            thrust * Time.fixedDeltaTime);

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity =
                rb.linearVelocity.normalized * maxSpeed;
        }
    }

    public void Die()
    {
        GameObject instantiatedExplosion = Instantiate(Explosion, transform.position, Quaternion.identity);

        Destroy(instantiatedExplosion, 2f);
        if (normalEnemy)
        {
            GameManager.Instance.normalEnemyPool.ReturnObject(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Meteor"))
        {
            currentHealth -=
                collision.gameObject.GetComponent<Asteroid>().oreData.oreDurability / 2 * collision.gameObject.GetComponent<Rigidbody2D>().mass / 2;

            collision.gameObject.GetComponent<Asteroid>().TakeDamage(dmg);
            if (currentHealth <= 0)
                Die();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            player.TakeDamage(dmg);
            StartCoroutine(player.Knockback(collision.relativeVelocity));
        }
    }
}