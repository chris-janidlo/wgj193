using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisWheelPlatform : MonoBehaviour
{
    public Transform CenterPoint;
    public float Radius, RotationSpeed;

    float t;

    void Update ()
    {
        t += RotationSpeed * Time.deltaTime;

        // parametric clockwise circle equation
        transform.position = new Vector2
        (
            Radius * Mathf.Cos(t) + CenterPoint.position.x,
            -Radius * Mathf.Sin(t) + CenterPoint.position.y
        );
    }
}
