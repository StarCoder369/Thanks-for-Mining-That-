using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public GameObject Explosion;

    public float speed;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = new Vector2(target.position.x, target.position.y) - rb.position;
        rb.linearVelocity = direction.normalized * speed;
    }

    public void Die()
    {
        GameObject instantiatedExplosion = Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(instantiatedExplosion, 10f);
        Destroy(gameObject);
    }
}
