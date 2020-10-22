using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandController : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Ground"))
        {
            Invoke("LandPlatform", 0.5f);
            Destroy(gameObject, 6f);
        }
    }

    void LandPlatform()
    {
        rb.isKinematic = true;
    }
}
