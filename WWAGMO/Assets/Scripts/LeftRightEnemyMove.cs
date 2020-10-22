using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightEnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 initialPosition;
    private SpriteRenderer spriteRenderer;
    int direction = -1;
    public float maxRange = 1;
    public float minRange = 1;
    public float moveSpeed = 1;
    float maxDist = 0;
    float minDist = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
        maxDist = initialPosition.x + maxRange;
        minDist = initialPosition.x - minRange;
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case -1:
                //Moving Left
                if (transform.position.x > minDist)
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
                if (transform.position.x < maxDist)
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
