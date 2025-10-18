using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected bool enableMovement = true;
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected bool isGrounded;
    [SerializeField] protected bool facingRight = true;
    [SerializeField] protected Transform target;

    [Header("Attack")]
    [SerializeField] protected bool enableAttack = true;
    [SerializeField] protected float attackRadius = 1f;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask targetLayer;

    [Header("Jump Settings")]
    [SerializeField] protected float jumpForce = 10f;
    [SerializeField] protected float rayLength = 1.5f;
    [SerializeField] protected LayerMask groundLayer;

    [Header("Health Settings")]
    [SerializeField] protected float health = 2f;
    [SerializeField] protected Material damageMaterial;
    [SerializeField] protected float flashDuration = 0.4f;


    protected Rigidbody2D rb;
    protected Animator animator;
    protected Collider2D col;
    protected SpriteRenderer spriteRenderer;
    protected bool isMovementAndJumpEnabled = true;
    protected virtual bool Attack => false;
    protected virtual float MoveDirectionX => 0f;
    protected virtual bool Jump => false;

    private Coroutine damageFlashCoroutine = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (enableAttack) HandleAttack();
        if (enableMovement)
        {
            HandleMovement();
            HandleFlip();
            HandleJump();
        }
    }

    public void DamageTarget()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, targetLayer);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<Entity>()?.TakeDamage();
        }
    }

    protected virtual void DamageFeedback()
    {
        if (damageFlashCoroutine != null)
        {
            StopCoroutine(damageFlashCoroutine);
        }
        damageFlashCoroutine = StartCoroutine(DamageFlashCoroutine());
    }

    IEnumerator DamageFlashCoroutine()
    {
        var existingMaterial = spriteRenderer.material;
        spriteRenderer.material = damageMaterial;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = existingMaterial;
    }

    virtual protected void Die()
    {
        col.enabled = false;
        animator.enabled = false;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 5);
    }


    protected virtual void TakeDamage(float damage = 1f)
    {
        health -= damage;

        DamageFeedback();

        if (health <= 0)
            Die();
    }

    protected virtual void HandleAttack()
    {
        if (Attack)
        {
            animator.SetTrigger("Attack");
        }
    }

    protected virtual void HandleMovement()
    {
        if (!isMovementAndJumpEnabled || Attack)
        {
            rb.linearVelocityX = 0;
        }
        else
        {
            rb.linearVelocityX = MoveDirectionX * speed;
        }

        animator.SetFloat("VelocityX", rb.linearVelocityX);
    }

    private void HandleFlip()
    {
        if (rb.linearVelocityX > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.linearVelocityX < 0 && facingRight)
        {
            Flip();

        }
    }

    protected void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    public void EnableMovementAndJump(bool enable)
    {
        isMovementAndJumpEnabled = enable;
        animator.ResetTrigger("Attack");
    }

    protected void HandleJump()
    {
        IsGrounded();

        if (Jump && isGrounded && isMovementAndJumpEnabled)
        {
            rb.linearVelocityY = jumpForce;
        }

        animator.SetFloat("VelocityY", rb.linearVelocityY);
    }

    private void IsGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
        animator.SetBool("IsGrounded", isGrounded);
    }

    void OnDrawGizmos()
    {

        Gizmos.DrawRay(transform.position, Vector3.down * rayLength);

        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
