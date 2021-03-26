using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class PlatformingAbilityChargeVisual : MonoBehaviour
{
    public TransitionableFloat ScaleTransition;

    void Start ()
    {
        ScaleTransition.AttachMonoBehaviour(this);
        ScaleTransition.FlashFromTo(0, 1);
    }

    void Update ()
    {
        transform.localScale = Vector3.one * ScaleTransition.Value;
    }

    public void Despawn ()
    {
        StartCoroutine(despawnRoutine());
    }

    IEnumerator despawnRoutine ()
    {
        ScaleTransition.FlashFromTo(1, 0);
        yield return new WaitWhile(() => ScaleTransition.Transitioning);
        Destroy(gameObject);
    }
}
