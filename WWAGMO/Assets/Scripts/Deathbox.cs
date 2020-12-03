using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathbox : MonoBehaviour
{
    Collider2D deathbox;
    // Start is called before the first frame update
    void Start()
    {
        deathbox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name.Equals("Player"))
        {
            col.gameObject.GetComponent<PlayerPlatformerController>().healthCount = 0;
        }
    }
}
