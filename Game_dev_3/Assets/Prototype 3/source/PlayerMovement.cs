using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    public float moveSpeed = 5f;
    bool isFacingRight = false;
    public float jumpPower = 5f;
    bool isJumping = false;

    public static bool timeFrozen = false;

    public static float timeTimer = 0f;
    public static float timeCooldown = 0f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isJumping = true;
        }

        // Freeze time when Z is pressed and cooldown is over
        if (Input.GetKeyDown(KeyCode.Z) && timeCooldown <= 0f)
        {
            timeFrozen = true;
            timeTimer = 5f; // Freeze for 5 seconds
        }

        // Count down freeze timer
        if (timeFrozen)
        {
            timeTimer -= Time.deltaTime;

            if (timeTimer <= 0f)
            {
                timeFrozen = false;
                timeCooldown = 3f; // Start 3 second cooldown
            }
        }

        // Count down cooldown
        if (!timeFrozen && timeCooldown > 0f)
        {
            timeCooldown -= Time.deltaTime;
        }

        //Debug.Log($"Frozen: {timeFrozen} | Freeze Timer: {timeTimer:F2} | Cooldown: {timeCooldown:F2}");
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the thing touched is a Spike
        if (collision.gameObject.CompareTag("Spike"))
        {
            RestartLevel();
        }

         if (collision.gameObject.CompareTag("Win"))
        {
            SceneManager.LoadScene("timeWin");
        }

        print(collision.gameObject.name);
        isJumping = false;

    }


    void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}