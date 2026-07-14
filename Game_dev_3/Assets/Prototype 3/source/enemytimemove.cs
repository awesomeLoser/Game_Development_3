using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 3f;             // Walking speed
    public float patrolDistance = 5f;    // Distance from start point

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Freeze enemy when time is frozen
        if (PlayerMovement.timeFrozen)
        {
            return;
        }

        // Move enemy
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        // Distance from starting point
        float distanceFromStart = transform.position.x - startPosition.x;

        // Change direction at patrol limits
        if (distanceFromStart >= patrolDistance)
        {
            movingRight = false;
            Flip();
        }
        else if (distanceFromStart <= -patrolDistance)
        {
            movingRight = true;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the thing touched is a Spike
        if (collision.gameObject.CompareTag("Spike"))
        {
            Destroy(gameObject);
        }
    }
}

