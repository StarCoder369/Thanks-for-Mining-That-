using UnityEngine;

public class DecoyMovement : MonoBehaviour
{
    public GameObject player;

    public bool decoyMove = true;
    public float moveSpeed = 5f;
    public float maxMoveTime = 5f;

    public float directionChangeInterval = 0.2f;
    public float maxAngleOffset = 15f;
    public float turnSpeed = 360f;

    private float moveTimer;
    private float directionTimer;

    private Vector2 baseDirection;
    private Vector2 currentDirection;
    private bool initialized;

    public GameObject trailEffect;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (!decoyMove)
        {
            trailEffect.SetActive(false);
            moveTimer = 0f;
            directionTimer = 0f;
            initialized = false;

            if (transform.parent != player.transform)
            {
                transform.SetParent(player.transform, true);
                transform.position = player.transform.position;
                transform.rotation = player.transform.rotation;
                transform.localPosition = Vector3.zero;
            }

            return;
        }
        else
        {
            trailEffect.SetActive(true);
        }

        if (!initialized)
        {
            initialized = true;

            transform.SetParent(null, true);
            transform.position = player.transform.position;

            baseDirection = (Quaternion.Euler(0, 0, -90f) * player.transform.up).normalized;
            currentDirection = baseDirection;
        }

        moveTimer += Time.deltaTime;
        directionTimer += Time.deltaTime;

        if (moveTimer >= maxMoveTime)
        {
            StopDecoy();
            return;
        }

        if (directionTimer >= directionChangeInterval)
        {
            directionTimer = 0f;

            float randomAngle = Random.Range(-maxAngleOffset, maxAngleOffset);

            Vector2 targetDirection = (Quaternion.Euler(0, 0, randomAngle) * currentDirection).normalized;

            currentDirection = Vector2.Lerp(currentDirection, targetDirection, 0.5f).normalized;
            currentDirection = Vector2.Lerp(currentDirection, baseDirection, 0.3f).normalized;
        }

        transform.position += (Vector3)(currentDirection * moveSpeed * Time.deltaTime);

        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90f);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void StopDecoy()
    {
        decoyMove = false;
        moveTimer = 0f;
        directionTimer = 0f;
        initialized = false;
    }
}