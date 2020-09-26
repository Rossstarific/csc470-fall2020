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
    public float preGrav = 0;


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

        targetVelocity = move * maxSpeed;
        
        preVelX = targetVelocity.x;
        preVelY = velocity.y;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashtimer = dashLength;
        }
        
        dashtimer -= Time.deltaTime;

        if (dashtimer > 0)
        {
            velocity.y = 0;
            gravityModifier = 0;
            if (move.x > 0.01f)
            {
                targetVelocity.x = maxSpeed * dashSpeed;
            }
            else if (move.x < -0.01f)
            {
                targetVelocity.x = maxSpeed * dashSpeed * -1;
            }
        }
        else
        {
            targetVelocity.x = preVelX;
            velocity.y = preVelY;
            gravityModifier = preGrav;

        }
    }
}



