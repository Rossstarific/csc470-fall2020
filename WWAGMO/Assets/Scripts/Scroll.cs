using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{

    public float speed = 0.5f;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset.x = 0;
        offset.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        offset.x += Time.deltaTime * speed;
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
