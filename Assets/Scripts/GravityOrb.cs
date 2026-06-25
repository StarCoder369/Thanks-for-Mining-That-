using System.Collections.Generic;
using UnityEngine;

public class GravityOrb : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float radius = 8f;
    [SerializeField] private LayerMask affectedLayers;

    [Header("Gravity")]
    [SerializeField] private float attractionStrength = 20f;
    private List<Collider2D> hits = new();

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

            float strength = 1f - (distance / radius);

            strength *= strength;

            rb.AddForce(
                attractionStrength * strength * direction.normalized,
                ForceMode2D.Force
            );
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}