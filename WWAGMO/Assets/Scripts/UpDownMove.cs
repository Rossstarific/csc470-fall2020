using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMove : MonoBehaviour
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
        maxDist = initialPosition.y + maxRange;
        minDist = initialPosition.y - minRange;
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case -1:
                //Moving Down
                if (transform.position.y > minDist)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -moveSpeed);
                }
                else
                {
                    direction = 1;
                }
                break;
            case 1:
                //Moving Up
                if (transform.position.y < maxDist)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, moveSpeed);
                }
                else
                {
                    direction = -1;
                }
                break;
        }
    }
}
