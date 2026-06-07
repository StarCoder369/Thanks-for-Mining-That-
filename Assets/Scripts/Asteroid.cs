using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public OreData oreData;

    public GameObject explosion;
    public GameObject hitEffect;

    public int minAmountToDrop;
    public int maxAmountToDrop;

    public float playerDmg;

    [Header("Movement Values")]
    public float driftSpeed = 1f;
    public float steeringForce = 0.5f;
    public float randomAngleRange = 45f;

    private Vector2 desiredVelocity;
    private Rigidbody2D rb;

    float currentHealth;
    float durability;
    float currentLifespan;
    float startingTime;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startingTime = Time.time;
        rb = GetComponent<Rigidbody2D>();

        Vector2 toCenter = (-transform.position).normalized;

        float randomAngle = Random.Range(-randomAngleRange, randomAngleRange);

        Vector2 randomizedDirection =
            Quaternion.Euler(0, 0, randomAngle) * toCenter;

        desiredVelocity = randomizedDirection.normalized * driftSpeed;
        SetStats();
    }

    public void SetStats()
    {
        rb = GetComponent<Rigidbody2D>();
        float size = transform.localScale.x;
        rb.mass = size * size / 4f;
        durability = oreData.oreDurability * size;
        currentHealth = durability;
    }

    void FixedUpdate()
    {
        currentLifespan = Time.time - startingTime;
        Vector2 correction = desiredVelocity - rb.linearVelocity;
        rb.AddForce(correction * steeringForce);

        if (currentLifespan > 100f && Vector2.Distance(transform.position, player.transform.position) > 100)
        {
            Destroy(gameObject, 5f);
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.Instance.AddItem(
            oreData,
            Random.Range(minAmountToDrop, maxAmountToDrop)
        );

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Meteor"))
        {
            rb.AddForce(-rb.linearVelocity * 3, ForceMode2D.Impulse);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            StartCoroutine(collision.gameObject.GetComponent<Player>().Knockback(collision.relativeVelocity));
        }
    }
}