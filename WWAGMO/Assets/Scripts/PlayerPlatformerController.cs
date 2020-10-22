using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public float dashSpeed = 5;
    [SerializeField] private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float dashLength = 0.1f;
    float dashtimer = 0;
    float preVelX = 0;
    float preVelY = 0;
    float preGrav = 0;


    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        preGrav = gravityModifier;
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetFloat("isDashing", dashtimer);

        targetVelocity = move * maxSpeed;
        
        preVelX = targetVelocity.x;
        preVelY = velocity.y;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (velocity.x > 0 || velocity.x < 0)
            {
                dashtimer = dashLength;
            }
        }
        
        dashtimer -= Time.deltaTime;

        if (dashtimer > 0)
        {
            velocity.y = 0;
            gravityModifier = 0;
            rb2d.gravityScale = 0;
            if (velocity.x > 0)
            {
                targetVelocity.x = maxSpeed * dashSpeed;
            }
            else if (velocity.x < 0)
            {
                targetVelocity.x = maxSpeed * dashSpeed * -1;
            }
        }
        else
        {
            targetVelocity.x = preVelX;
            velocity.y = preVelY;
            gravityModifier = preGrav;
            rb2d.gravityScale = 1;

        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("PimpleLg"))
        {
            velocity.y = jumpTakeOffSpeed * 1.5f;
        }
    }
}



