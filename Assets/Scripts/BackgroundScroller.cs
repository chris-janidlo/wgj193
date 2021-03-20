using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float ScrollSpeed, ScrollSize;

    float y;

    void Update ()
    {
        y = Mathf.Repeat(y + ScrollSpeed * Time.deltaTime, ScrollSize);
        transform.localPosition = new Vector3(0, y, -transform.parent.position.z);
    }
}
