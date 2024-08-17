using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Enemy : CharacterStats
{

    public Transform[] patrolPoints = null;
    public LayerMask playerLayer;
    private Transform player;
    public float searchRadius = 0.5f;
    public float normalSpeed = 1f;
    public float chaseSpeed = 4f;
    private int currentPointIndex = 0;
    private Vector3 originalScale;

    // GroundCheck 
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    private bool isGrounded = true;
    private bool canGroundCheck = true;

    // Knockback 
    private bool isKnocked = false;
    public float knockback;
    public float knockbackDelay;





    void Start()
    {
        originalScale = transform.localScale;
    }


    void Update()
    {
        if (isDying)
        {
            return;
        }
        if (isGrounded && !isKnocked)
        {
            EnemyMove();
        }
    }

    void FixedUpdate()
    {
        if (canGroundCheck)
        {
            GroundCheck();
        }
    }

    private void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheck.position, groundCheck.localScale, 0f, groundLayer);
        isGrounded = colliders.Length > 0;
        animator.SetBool("isGrounded", isGrounded);
        if (isGrounded)
        {
            isKnocked = false;
        }
    }

    private void EnemyMove()
    {
        if (player == null)
        {
            if (patrolPoints != null && patrolPoints.Length > 0)
            {
                Patrol();
            }

            Search();
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
        }
        else
        {

            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
            ChasePlayer();
        }
    }

    public void Search()
    {
        Collider2D searchColliders = Physics2D.OverlapCircle(transform.position, searchRadius, playerLayer);

        if (searchColliders != null)
        {
            player = searchColliders.transform;
        }
    }


    public void ChasePlayer()
    {
        FlipTowards(player.position);
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.position) > searchRadius)
        {
            player = null;
        }
    }


    void Patrol()
    {
        if (patrolPoints.Length == 0 || player != null) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, normalSpeed
 * Time.deltaTime);

        FlipTowards(targetPoint.position);

        if (Vector2.Distance(transform.position, targetPoint.position) < 1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    void FlipTowards(Vector3 targetPosition)
    {
        // Determine if the target is to the left or right
        if (targetPosition.x < transform.position.x)
        {
            // Flip the enemy to face left
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            // Flip the enemy to face right
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            KnockBack(other);
        }
    }

    private void KnockBack(Collision2D other)
    {
        isKnocked = true;
        canGroundCheck = false;
        rb.velocity = Vector2.zero;

        Vector2 direction = (transform.position - other.transform.position).normalized;
        if (direction.x < 0)
        {
            direction.x = -0.5f;
        }
        else
        {
            direction.x = 0.5f;
        }

        Vector2 knockbackForce = new Vector2(direction.x * knockback, direction.y * knockback * 0.5f);
        rb.AddForce(knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(DelayGroundCheck(knockbackDelay));
    }

    private IEnumerator DelayGroundCheck(float delay)
    {
        yield return new WaitForSeconds(delay);
        canGroundCheck = true;
    }

}

