using System.Collections.Generic;
using UnityEngine;

public class AsteroidForge : MonoBehaviour
{
    public List<GameObject> asteroids;
    public GameObject effect;

    public float timeToStart = 2f;
    public float maxSize = 3f;
    public float sizeStep = 1f;

    float timeLeft;
    bool canScale;

    bool scale = true;

    GameObject instantiatedAsteroid;

    void Start()
    {
        timeLeft = timeToStart;

        canScale = false;
    }

    void Update()
    {
        if (!canScale)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0f)
            {
                Create();
            }
        }

        if (canScale && instantiatedAsteroid != null && scale)
        {
            instantiatedAsteroid.transform.localScale += sizeStep * Time.deltaTime * Vector3.one;

            if (instantiatedAsteroid.transform.localScale.x >= maxSize)
            {
                scale = false;
                instantiatedAsteroid.GetComponent<Asteroid>().SetStats();
            }
        }

        if (instantiatedAsteroid == null && canScale)
        {
            Destroy(gameObject);
        }
        if (instantiatedAsteroid != null)
        {
            transform.position = instantiatedAsteroid.transform.position;
        }
    }

    void Create()
    {
        canScale = true;

        if (asteroids == null || asteroids.Count == 0)
        {
            return;
        }

        int randomInt = Random.Range(0, 2);

        if (randomInt == 0)
        {
            instantiatedAsteroid = GameManager.Instance.roundAsteroidPool.GetObject();
        }
        else
        {
            instantiatedAsteroid = GameManager.Instance.normalAsteroidPool.GetObject();
        }

        instantiatedAsteroid.transform.localScale = new Vector2(1f, 1f);
        instantiatedAsteroid.GetComponent<Asteroid>().oreData = null;
        instantiatedAsteroid.GetComponent<Asteroid>().SetStats();
        instantiatedAsteroid.transform.position = transform.position;
        instantiatedAsteroid.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
    }
}