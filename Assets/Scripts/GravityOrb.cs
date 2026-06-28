using System.Collections.Generic;
using UnityEngine;

public class GravityOrb : MonoBehaviour
{
    [Header("Detection")]
    public float radius = 8f;
    public LayerMask affectedLayers;
    public GameObject indicatorCircle;

    [Header("Gravity")]
    public float attractionStrength = 20f;

    public AnimationCurve forceFalloff =
        AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

    [Header("Other Things")]
    public float destroyTime = 5f;

    [Header("Indicator Animation")]
    public float growDuration = 1f;
    public float shrinkDuration = 1f;
    public float minScale = 0.001f;

    private List<Collider2D> hits = new();

    private float lifeTimer;
    private Vector3 fullScale;

    void Start()
    {
        fullScale = Vector3.one * radius;

        indicatorCircle.transform.localScale = Vector3.one * minScale;

        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;

        float scaleMultiplier = 1f;

        if (lifeTimer < growDuration)
        {
            scaleMultiplier = Mathf.Lerp(
                minScale,
                1f,
                lifeTimer / growDuration
            );
        }
        else if (lifeTimer > destroyTime - shrinkDuration)
        {
            float t = (lifeTimer - (destroyTime - shrinkDuration)) / shrinkDuration;

            scaleMultiplier = Mathf.Lerp(
                1f,
                minScale,
                t
            );
        }

        indicatorCircle.transform.localScale =
            fullScale * scaleMultiplier;
    }

    private void FixedUpdate()
    {
        hits.Clear();

        Physics2D.OverlapCircle(
            transform.position,
            radius,
            new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = affectedLayers
            },
            hits
        );

        foreach (var hit in hits)
        {
            Rigidbody2D rb = hit.attachedRigidbody;

            if (rb == null || rb.gameObject == gameObject)
                continue;

            Vector2 direction = (Vector2)transform.position - rb.position;
            float distance = direction.magnitude;

            if (distance < 0.01f)
                continue;

            float normalizedDistance = distance / radius;
            float strength = forceFalloff.Evaluate(normalizedDistance);

            if (hit.gameObject.CompareTag("Meteor"))
            {
                rb.AddForce(attractionStrength * strength * 5f * direction.normalized, ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(attractionStrength * strength * direction.normalized, ForceMode2D.Force);
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}