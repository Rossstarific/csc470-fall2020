using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 initialPosition;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    public int direction = -1;
    public float maxRange = 1;
    public float minRange = 1;
    public float moveSpeed = 1;
    float maxDist = 0;
    float minDist = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        initialPosition = rb2D.position;
        maxDist = initialPosition.x + maxRange;
        minDist = initialPosition.x - minRange;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (direction)
        {
            case -1:
                //Moving Left
                if (rb2D.position.x > minDist)
                {
                     GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    direction = 1;
                }
            break;
            case 1:
                //Moving Right
                if (rb2D.position.x < maxDist)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    direction = -1;
                }
            break;
        }

        bool flipSprite = (spriteRenderer.flipX ? (direction < 0) : (direction > 0));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
