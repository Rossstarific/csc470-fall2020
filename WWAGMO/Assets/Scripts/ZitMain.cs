using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZitMain : MonoBehaviour
{
    PlayerPlatformerController playerObj;
    float distFromLeft = 0.25f;
    float distFromRight = 0.20f;
    private Vector2 initialPosition;
    private Vector2 playerColPointY;
    private Vector2 playerColPointLeft;
    private Vector2 playerColPointRight;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            playerObj = col.gameObject.GetComponent<PlayerPlatformerController>();
            playerColPointY = col.gameObject.transform.GetChild(1).gameObject.transform.position;
            if (playerObj.dashtimer > 0)
            {
                playerObj.enemyKill = true;
                gameObject.SetActive(false);
                Invoke("Respawn", 4f);
            } else if (playerColPointY.y > transform.position.y) 
            {
                playerObj.velocity.y = playerObj.jumpTakeOffSpeed;
                gameObject.SetActive(false);
                Invoke("Respawn", 4f);
            } else
            {
                playerObj.healthCount--;
                if(col.GetContact(0).point.x < transform.position.x - distFromLeft)
                {
                    playerObj.colLeft = true;
                } else if(col.GetContact(0).point.x > transform.position.x + distFromRight)
                {
                    playerObj.colRight = true;
                }
            playerObj.knockbacktimerx = playerObj.knockbackDurationX;
            playerObj.knockbacktimery = playerObj.knockbackDurationY;
            }
        }
    }

    void Respawn()
    {
        gameObject.transform.position = initialPosition;
        gameObject.SetActive(true);
    }
}
