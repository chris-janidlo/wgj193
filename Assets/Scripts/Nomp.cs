using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class Nomp : MonoBehaviour
{
    public Vector2 PlannedDirection => dirVecs[plannedDir];

    public float MoveDistance;
    public TransitionableVector2 MoveTransition;

    public Vector2 MoveCheckBoxDimensions;
    public LayerMask MoveCheckLayerMask;
    [Tooltip("0 is Up, 1 is Right, 2 is Down, 3 is Left")]
    public List<SpriteRenderer> Indicators;

    int plannedDir;
    List<Vector2> dirVecs = new List<Vector2> { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    void Start ()
    {
        MoveTransition.AttachMonoBehaviour(this);
        MoveTransition.Value = transform.position;
    }

    void Update ()
    {
        transform.position = MoveTransition.Value;
    }

    public void ChooseDirection ()
    {
        List<int> dirInts = new List<int> { 0, 1, 2, 3 };
        dirInts.Remove(plannedDir);
        dirInts.ShuffleInPlace();

        plannedDir = -1;

        foreach (int dir in dirInts)
        {
            if (!Physics2D.BoxCast(transform.position, MoveCheckBoxDimensions, 0, dirVecs[dir], MoveDistance, MoveCheckLayerMask))
            {
                Indicators[dir].enabled = true;
                plannedDir = dir;
                break;
            }
        }
    }

    public void Move ()
    {
        if (plannedDir == -1) return;

        MoveTransition.StartTransitionTo((Vector2) transform.position + PlannedDirection * MoveDistance);
        Indicators[plannedDir].enabled = false;
    }
}
