using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFloorFollower : MonoBehaviour
{
    public FloorList FloorList;

    public void OnPhaseChanged (Phase newPhase)
    {
        if (newPhase != Phase.Build) return;

        transform.position = FloorList.CurrentPlayerFloor.HandSnapPoint.position;
    }
}
