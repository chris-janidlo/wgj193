using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

public class Floor : MonoBehaviour
{
    public int FloorNumber;
    public List<CardBuildZone> BuildZones;
    public Transform SpawnPoint, CameraCenterPoint;

    public IntVariable CurrentPlayerFloor;
    public GameLifeCycleManager GameLifeCycleManager;

    void OnTriggerEnter2D (Collider2D collision)
    {
        var player = collision.GetComponent<PlayerLifeCycleManager>();

        if (player == null || FloorNumber == CurrentPlayerFloor.Value) return;

        GameLifeCycleManager.PlayerReachedNewFloor(FloorNumber);
    }
}
