using System.Collections.Generic;
using UnityEngine;

public class AsteroidForge : MonoBehaviour
{
    public List<GameObject> asteroids;
    public GameObject effect;

    public float timeToStart = 2f;
    public float timeToMaxSize = 3f;
    public float maxSize = 3f;
    public float sizeStep = 1f;

    float timeLeft;
    bool canScale;

    GameObject instantiatedAsteroid;

    void Start()
    {
        timeLeft = timeToStart;

        if (asteroids == null || asteroids.Count == 0)
        {
            return;
        }

        instantiatedAsteroid =
            Instantiate(asteroids[Random.Range(0, asteroids.Count)]);

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

        if (canScale && instantiatedAsteroid != null)
        {
            instantiatedAsteroid.transform.localScale +=
                Vector3.one * sizeStep * Time.deltaTime;

            if (instantiatedAsteroid.transform.localScale.x >= maxSize)
            {
                Destroy(gameObject);
            }
        }

        if (instantiatedAsteroid == null)
        {
            Destroy(gameObject);
        }
    }

    void Create()
    {
        canScale = true;

        if (effect != null)
        {
            Destroy(effect);
        }
    }
}