using UnityEngine;

public class Thwomp : MonoBehaviour
{
    public float fallSpeed = 25f;
    public float returnSpeed = 5f;
    public float waitTime = 2f;

    private Vector3 startPosition;

    private Rigidbody2D rb;

    private bool isFalling = false;
    private bool isReturning = false;

    private float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        // Freeze during time stop
        if (PlayerMovement.timeFrozen)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        timer += Time.deltaTime;

        // Start falling after waiting
        if (timer >= waitTime && !isFalling && !isReturning)
        {
            isFalling = true;
            rb.linearVelocity = Vector2.down * fallSpeed;
        }

        // Return upwards
        if (isReturning)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                startPosition,
                returnSpeed * Time.deltaTime
            );

            if (transform.position == startPosition)
            {
                isReturning = false;
                timer = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Stop falling when hitting something
        if (isFalling)
        {
            isFalling = false;
            rb.linearVelocity = Vector2.zero;
            isReturning = true;
        }
    }
}