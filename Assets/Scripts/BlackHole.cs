using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float startSize;
    public float endSize;
    public float timeToMaxSize;

    float timer;

    bool sentMessage;

    void Start()
    {
        transform.localScale = Vector3.one * startSize;
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.localScale = Vector3.one * Mathf.Lerp(startSize, endSize, Mathf.Clamp01(timer / timeToMaxSize));

        if (transform.localScale.x >= endSize && !sentMessage)
        {
            sentMessage = true;
            GameManager.Instance.EndGame();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Die();
        }

        if (collision.gameObject.CompareTag("Meteor"))
        {
            collision.gameObject.GetComponent<Asteroid>().Die();
        }
    }
}