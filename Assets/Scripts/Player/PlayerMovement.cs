using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player
{


    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    private bool isGrounded;
    private int jumpCount;
    public Weapon weapon;

    public bool IsGrounded { get { return isGrounded; } }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60;
    }

    public void Update()
    {
        // Horizontal movement
        float moveX = Input.GetAxis("Horizontal");

        if (isKnocked || isDying){
            return;
        }


        if (moveX > 0)
        {
            transform.localScale = new Vector3(5, 5, 5);
            animator.SetBool("isRunning", true);

        }
        else if (moveX < 0)
        {
            animator.SetBool("isRunning", true);
            transform.localScale = new Vector3(-5, 5, 5);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        // Attacking 
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Attack");
            weapon.Fire();
        }


        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount < 1)
            {
                jumpCount++;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

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
            Debug.Log("Player grounded. isKnocked reset to: " + isKnocked);
            jumpCount = 0;
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the ground check area in the Scene view
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheck.position, groundCheck.localScale);
        }
    }





}