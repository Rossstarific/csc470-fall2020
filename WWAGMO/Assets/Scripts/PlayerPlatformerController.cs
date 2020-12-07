using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public float dashSpeed = 5;
    public float dashLength = 0.1f;
    bool inputEnabled;
    [SerializeField] private Animator animator;
    private SpriteRenderer spriteRenderer;

    [HideInInspector] public float dashtimer = 0;
    public float knockbackDurationX;
    public float knockbackDurationY;
    [HideInInspector] public float knockbacktimerx;
    [HideInInspector] public float knockbacktimery;
    public float preVelX = 0;
    public float preVelY = 0;
    public float preGrav = 0;
    public int healthCount;
    public Vector2 move;
    [HideInInspector] public bool colLeft = false;
    [HideInInspector] public bool colRight = false;
    [HideInInspector] public bool enemyKill = false;
    bool extraJump = false;
    bool prevDash = false;


    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        inputEnabled = true;
        preGrav = gravityModifier;
        healthCount = 6;
        animator.SetInteger("healthCount", healthCount);
    }

    protected override void ComputeVelocity()
    {
        if (inputEnabled == true)
        {
            move = Vector2.zero;

            move.x = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && (grounded || extraJump))
            {
                velocity.y = jumpTakeOffSpeed;
                dashtimer = 0;
                extraJump = false;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }

            targetVelocity = move * maxSpeed;

            bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
            if (dashtimer <= 0)
            {
                if (flipSprite)
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                }
            }
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetFloat("isDashing", dashtimer);
        animator.SetInteger("healthCount", healthCount);
        animator.SetFloat("knockbackTimer", knockbacktimerx);

        preVelX = targetVelocity.x;
        preVelY = velocity.y;

        if (grounded)
        {
            prevDash = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (grounded)
            {
                if (velocity.x > 0 || velocity.x < 0)
                {
                    dashtimer = dashLength;
                }
            }
            else if (!grounded && prevDash == false)
            {
                if (velocity.x > 0 || velocity.x < 0)
                {
                    dashtimer = dashLength;
                    prevDash = true;
                }
            }
            else if (!grounded && prevDash == true)
            {
                return;
            }

        }

        dashtimer -= Time.deltaTime;

        if (dashtimer > 0 && knockbacktimerx <= 0 && knockbacktimery <= 0)
        {
            velocity.y = 0;
            gravityModifier = 0;
            if (velocity.x > 0)
            {
                targetVelocity.x = maxSpeed * dashSpeed;
                if (enemyKill == true)
                {
                    prevDash = false;
                    extraJump = true;
                }
            }
            else if (velocity.x < 0)
            {
                targetVelocity.x = maxSpeed * dashSpeed * -1;
                if (enemyKill == true)
                {
                    prevDash = false;
                    extraJump = true;
                }
            }
        }
        else
        {
            gravityModifier = preGrav;
            enemyKill = false;
        }
    
        knockbacktimerx -= Time.deltaTime;
        knockbacktimery -= Time.deltaTime;
        if (knockbacktimerx > 0)
        {
            rb2d.isKinematic = false;
            if (colLeft == true)
            {
                targetVelocity.x = maxSpeed * -1.5f;
                if (knockbacktimery > 0)
                {
                    velocity.y = jumpTakeOffSpeed * 0.3f;
                }
            } else if (colRight == true)
            {
                targetVelocity.x = maxSpeed * 1.5f;
                if (knockbacktimery > 0)
                {
                    velocity.y = jumpTakeOffSpeed * 0.3f;
                }
            }

        } else
        {
            rb2d.isKinematic = true;
            colLeft = false;
            colRight = false;
        }

        StartCoroutine("LifeEmpty");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("EndFlag"))
        {
            StartCoroutine("GameWin");
        }
    }
    IEnumerator LifeEmpty()
    {
        if (healthCount == 0)
        {
            inputEnabled = false;
            velocity.x = 0;
            velocity.y = 0;
            gravityModifier = 0;
            knockbacktimerx = 0;
            knockbacktimery = 0;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(1.4f);
            gameObject.GetComponentInChildren<Animator>().enabled = false;
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("StartMenu");
        } else
        {
            yield return null;
        }
    }

    IEnumerator GameWin()
    {
        yield return new WaitForSeconds(1);
        inputEnabled = false;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("End");
    }
}



