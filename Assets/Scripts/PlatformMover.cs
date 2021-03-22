using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class PlatformMover : MonoBehaviour
{
    public Transform StartPoint, EndPoint;
    public TransitionableVector2 MovementTransition;

    IEnumerator Start ()
    {
        MovementTransition.AttachMonoBehaviour(this);
        MovementTransition.Value = StartPoint.position;

        bool @switch = false;

        while (true)
        {
            MovementTransition.StartTransitionTo((@switch ? StartPoint : EndPoint).position);
            @switch = !@switch;

            yield return new WaitWhile(() => MovementTransition.Transitioning);
        }
    }

    void Update ()
    {
        // could put this in the Start coroutine, but then you can't stop the platform from moving by disabling the script
        transform.position = MovementTransition.Value; // even if there's a rigidbody attached, only ever move the transform directly; otherwise, if the player's on top, they won't move with this
    }
}
