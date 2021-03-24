using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlatformingBits : MonoBehaviour
{
    public bool ColliderState
    {
        set => Colliders.ForEach(c => c.enabled = value);
    }

    public List<Collider2D> Colliders;
}
