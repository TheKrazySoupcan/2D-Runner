using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float maxSpeed = 10f;
    public float jumpForce = 400f;
    [Range(0, 1)]
    public float crouchSpeed = 0.30f;
    public bool airControl = false;
    public LayerMask whatIsGround;

    private bool facingRight = true;
    private Transform groundCheck;
    private float groundRadius = 0.2f;
    private bool grounded = false;
    private Transform ceilingCheck;
    private float ceilingRadius = 0.1f;
    private Animator anim;
    private Rigidbody2D rigid;

    void Awake()
    {
        groundCheck = transform.Find("GroundCheck");
        ceilingCheck = transform.Find("CeilingCheck");
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

    }
    // FixedUpdate called at specific framerate 
    void FixedUpdate()
    {
        // Performing ground check using Physics2D
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigid.velocity.y);

    }

    void Flip()
    {
        // Switch the player is facing
        facingRight = !facingRight;
        // Invert scale of player on x
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Inverts x
        transform.localScale = scale;

    }

    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching check to see if you can stand up
        if (crouch == false)
        {
            // Check to see if we hit ceiling
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
            {
                crouch = true;

            }
        }

        anim.SetBool("Crouch", crouch);
        if (grounded || airControl)
        {
            // Reduce speed if crouching
            move = (crouch ? move * crouchSpeed : move);

            anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);

            if (move > 0 && !facingRight)
            {
                Flip();

            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }


        }
        if (grounded && jump && anim.GetBool("Ground"))
        {
            anim.SetBool("ground", false);
            grounded = false;
            rigid.AddForce(new Vector2(0, jumpForce));

        }
    }
}
