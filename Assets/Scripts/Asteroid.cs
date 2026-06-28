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

    public bool asteroid1;
    public bool asteroid2;

    private Vector2 desiredVelocity;
    private Rigidbody2D rb;

    [Header("Others")]
    public Sprite[] oreIndication;
    public float maxLockTime;

    [Header("Growth")]
    public float growthMultiplier = 1.5f;
    public float growthSpeed = 2f;
    public float pushForce = 20f;
    private Collider2D[] overlapResults = new Collider2D[16];
    private ContactFilter2D overlapFilter;

    private bool isGrowing;
    private Vector3 targetScale;

    float currentHealth;
    float durability;
    float currentLifespan;
    float startingTime;
    float lockTime;

    GameObject player;

    void Start()
    {
        lockTime = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        startingTime = Time.time;
        rb = GetComponent<Rigidbody2D>();

        Vector2 toCenter = (-transform.position).normalized;

        float randomAngle = Random.Range(-randomAngleRange, randomAngleRange);

        Vector2 randomizedDirection =
            Quaternion.Euler(0, 0, randomAngle) * toCenter;

        desiredVelocity = randomizedDirection.normalized * driftSpeed;
        SetStats();

        overlapFilter = new ContactFilter2D();
        overlapFilter.useTriggers = false;
    }

    public void SetStats()
    {
        rb = GetComponent<Rigidbody2D>();
        float size = transform.localScale.x;
        rb.mass = size * size / 4f;
        durability = oreData.oreDurability * size * 1f;
        currentHealth = durability;
        Transform children = GetComponentInChildren<Transform>();

        foreach (Transform child in children)
        {
            child.GetComponent<SpriteRenderer>().sprite = oreData.oreIcon;
            float randomScale = Random.Range(0.09f, 0.12f);
            child.localScale = new Vector2(randomScale, randomScale);

            Color tempColor = child.GetComponent<SpriteRenderer>().color;

            tempColor.a = Random.Range(0.2f, 0.4f);

            child.GetComponent<SpriteRenderer>().color = tempColor;
        }
    }

    void FixedUpdate()
    {
        currentLifespan = Time.time - startingTime;
        Vector2 correction = desiredVelocity - rb.linearVelocity;
        rb.AddForce(correction * steeringForce);

        if (currentLifespan > 50f && Vector2.Distance(transform.position, player.transform.position) > 100)
        {
            Destroy(gameObject, 5f);
        }

        if (lockTime > 0)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            lockTime -= Time.deltaTime;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        if (isGrowing)
        {
            transform.localScale = Vector3.MoveTowards(
                transform.localScale,
                targetScale,
                growthSpeed * Time.fixedDeltaTime
            );

            PushOverlappingBodies();

            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
            {
                transform.localScale = targetScale;
                isGrowing = false;
                SetStats();
            }
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

        GameManager.Instance.coins += Random.Range(1, 2);

        if (explosion != null)
        {
            GameObject instantiatedExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(instantiatedExplosion, 5f);
        }

        if (asteroid1)
        {
            GameManager.Instance.normalAsteroidPool.ReturnObject(gameObject);
        }
        else if (asteroid2)
        {
            GameManager.Instance.roundAsteroidPool.ReturnObject(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Meteor"))
        {
            rb.AddForce(collision.relativeVelocity * 7, ForceMode2D.Impulse);
            TakeDamage(rb.mass * rb.linearVelocity.magnitude * 0.0001f * collision.gameObject.GetComponent<Rigidbody2D>().mass);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(rb.mass * rb.linearVelocity.magnitude * 0.002f);
            collision.gameObject.GetComponent<Player>().ApplyKnockback(collision.relativeVelocity * 5);
        }
        if (collision.gameObject.CompareTag("AsteroidLock"))
        {
            lockTime += maxLockTime;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("GrowTool"))
        {
            Grow();
            Destroy(collision.gameObject);
        }
    }

    public void Grow()
    {
        targetScale = transform.localScale * growthMultiplier;
        isGrowing = true;
    }

    void PushOverlappingBodies()
    {
        Collider2D myCollider = GetComponent<Collider2D>();

        int count = myCollider.Overlap(overlapFilter, overlapResults);

        for (int i = 0; i < count; i++)
        {
            Collider2D other = overlapResults[i];

            if (other == myCollider)
                continue;

            Rigidbody2D otherRb = other.attachedRigidbody;

            if (otherRb == null || otherRb == rb)
                continue;

            Vector2 direction =
                (otherRb.worldCenterOfMass - rb.worldCenterOfMass).normalized;

            otherRb.AddForce(direction * 5f, ForceMode2D.Impulse);
        }
    }
}
