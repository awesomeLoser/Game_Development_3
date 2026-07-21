using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovementUpgrades : MonoBehaviour
{
    float horizontalInput;
    public float moveSpeed = 5f;
    bool isFacingRight = false;
    public float jumpPower = 5f;
    bool isJumping = false;
    bool isGrounded = false;

    public float wallJumpPower = 5f;
    bool isTouchingWall = false;
    float wallDirection = 0f; // -1 = wall is on our left, 1 = wall is on our right

    public float wallSlideSpeed = 2f;

    public float jumpBufferTime = 0.15f;
    float jumpBufferCounter = 0f;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isJumping = true;
            isGrounded = false;
            jumpBufferCounter = 0f;
        }
        else if (jumpBufferCounter > 0f && isTouchingWall)
        {
            rb.linearVelocity = new Vector2(-wallDirection * moveSpeed, wallJumpPower);
            isJumping = true;
            jumpBufferCounter = 0f;
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        if (isTouchingWall && !isGrounded && rb.linearVelocity.y < 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }
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
        if (collision.gameObject.CompareTag("Spike"))
        {
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
            wallDirection = Mathf.Sign(collision.GetContact(0).normal.x);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
        print(collision.gameObject.name);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}