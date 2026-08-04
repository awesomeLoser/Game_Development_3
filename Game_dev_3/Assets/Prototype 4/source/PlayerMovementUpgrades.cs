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

    // --- Ability unlock flags (set to true by pickups, see AbilityPickup.cs) ---
    public bool canWallJump = false;
    public bool canSlash = false;

    // --- Sword slash settings ---
    public Transform attackPoint;      // empty child GameObject positioned in front of the player
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
    public float attackCooldown = 0.5f;
    float attackCooldownTimer = 0f;
    public GameObject swordSlash;      // sword sprite object (child of AttackPoint), disabled by default
    public float slashVisibleTime = 0.15f;

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
        else if (jumpBufferCounter > 0f && isTouchingWall && canWallJump)
        {
            rb.linearVelocity = new Vector2(-wallDirection * moveSpeed, wallJumpPower);
            isJumping = true;
            jumpBufferCounter = 0f;
        }

        attackCooldownTimer -= Time.deltaTime;
        if (canSlash && Input.GetButtonDown("Fire1") && attackCooldownTimer <= 0f)
        {
            Slash();
            attackCooldownTimer = attackCooldown;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Wall slide is now also gated behind the wall jump unlock
        if (canWallJump && isTouchingWall && !isGrounded && rb.linearVelocity.y < 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }
    }

    void Slash()
    {
        if (swordSlash != null)
        {
            StartCoroutine(FlashSword());
        }

        if (attackPoint == null) return;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    System.Collections.IEnumerator FlashSword()
    {
        swordSlash.SetActive(true);
        yield return new WaitForSeconds(slashVisibleTime);
        swordSlash.SetActive(false);
    }

    // --- Called by AbilityPickup.cs when the player grabs an upgrade item ---
    public void UnlockWallJump()
    {
        canWallJump = true;
    }

    public void UnlockSlash()
    {
        canSlash = true;
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

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}