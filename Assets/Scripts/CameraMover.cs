using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using crass;

public class CameraMover : MonoBehaviour
{
    public float SmoothFollowTime;
    public float FollowSize, BuildPhaseSize;
    public TransitionableFloat ZoomTransition;

    public Camera Camera;
    public FloorList FloorList;
    public Vector3Variable CurrentPlayerPosition;

    bool followingPlayer;
    Vector3 smoothFollowVelocity;

    void Start ()
    {
        ZoomTransition.AttachMonoBehaviour(this);
    }

    void Update ()
    {
        Camera.orthographicSize = ZoomTransition.Value;

        var target = getTargetPosition();
        transform.position = Vector3.SmoothDamp(transform.position, target, ref smoothFollowVelocity, SmoothFollowTime);
    }

    public void OnPhaseChanged (Phase newPhase)
    {
        followingPlayer = newPhase == Phase.Platforming;

        if (!followingPlayer)
        {
            // normally I would avoid ever assigning to the same atom variable in more than one script, but...
            CurrentPlayerPosition.Value = FloorList.CurrentPlayerFloor.SpawnPoint.position;
        }

        ZoomTransition.StartTransitionTo(followingPlayer ? FollowSize : BuildPhaseSize);
    }

    Vector3 getTargetPosition ()
    {
        var yPosition = (followingPlayer ? CurrentPlayerPosition.Value : FloorList.CurrentPlayerFloor.CameraCenterPoint.position).y;

        var minYPosition = FloorList.CurrentPlayerFloor.GetComponent<Collider2D>().bounds.min.y + Camera.orthographicSize;
        yPosition = Mathf.Max(yPosition, minYPosition);

        return new Vector3(0, yPosition, transform.position.z);
    }
}
