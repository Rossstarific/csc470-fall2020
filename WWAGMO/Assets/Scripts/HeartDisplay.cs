using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartDisplay : MonoBehaviour
{
    public GameObject Player;
    public Image ObjectwithImage;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public int heartFull;
    public int heartHalf;
    public int heartEmpty;
    public int healthCount;

    void Start()
    {

    }

    void Update()
    {
        healthCount = Player.GetComponent<PlayerPlatformerController>().healthCount;
        if (healthCount == heartFull)
        {
            ObjectwithImage.sprite = fullHeart;
        }
        else if(healthCount == heartHalf)
        {
            ObjectwithImage.sprite = halfHeart;
        } 
        else if (healthCount <= heartEmpty)
        {
            ObjectwithImage.sprite = emptyHeart;
        }
    }
}
