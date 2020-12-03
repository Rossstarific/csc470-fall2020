using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;
    private float oldPosition;
    float diff;
    float currPos;
    void Start()
    {
        oldPosition = transform.position.x;
    }
    void Update()
    {
        if (transform.position.x != oldPosition)
        {
            if (onCameraTranslate != null)
            {
                currPos = transform.position.x;
                float delta = oldPosition - transform.position.x;
                diff = delta;
                onCameraTranslate(delta);
            }
            oldPosition = transform.position.x;
        }
    }
}