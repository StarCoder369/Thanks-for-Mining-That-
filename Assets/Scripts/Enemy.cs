using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public GameObject Explosion;

    [Header("Stats")]
    public float maxHealth = 100f;
    public float dmg = 2f;
    public Vector3 rotationOffset;

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
        if (GameManager.Instance.followDecoy)
        {
            if (target == null || !target.gameObject.CompareTag("Decoy"))
            {
                target = GameObject.FindWithTag("Decoy").transform;
            }
        }
        else
        {
            if (target == null || !target.gameObject.CompareTag("Player"))
            {
                target = GameObject.FindWithTag("Player").transform;
            }
        }

        if (target == null)
            return;

        Vector2 toTarget = (Vector2)target.position - rb.position;

        float targetAngle =
            Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg + rotationOffset.z;

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
        StatsManager.Instance.enemiesKilled++;
        StatsManager.Instance.allEnemiesKilled++;
        GameObject instantiatedExplosion = Instantiate(Explosion, transform.position, Quaternion.identity);

        Destroy(instantiatedExplosion, 2f);
        int coinsToAdd = Random.Range(1, 2);
        GameManager.Instance.coins += coinsToAdd;
        StatsManager.Instance.totalCoins += coinsToAdd;
        StatsManager.Instance.allTotalCoins += coinsToAdd;
        if (normalEnemy)
        {
            GameManager.Instance.normalEnemyPool.ReturnObject(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Meteor"))
        {
            if (collision.gameObject.GetComponent<Asteroid>().oreData != null)
            {
                currentHealth -= collision.gameObject.GetComponent<Asteroid>().oreData.oreDurability * 3 * collision.gameObject.GetComponent<Rigidbody2D>().mass / 2;
            }
            else
            {
                currentHealth -= collision.gameObject.GetComponent<Rigidbody2D>().mass * 10;
            }

            collision.gameObject.GetComponent<Asteroid>().TakeDamage(dmg);
            if (currentHealth <= 0)
                Die();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            player.TakeDamage(dmg);
            player.ApplyKnockback(collision.relativeVelocity);
            Die();
        }
    }
}