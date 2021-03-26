using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms.BaseAtoms;

public class Floor : MonoBehaviour
{
    public int FloorNumber { get; private set; }
    public IEnumerable<CardBuildZone> BuildZones => GetComponentsInChildren<CardBuildZone>();

    public Transform SpawnPoint, CameraCenterPoint;

    public IntVariable CurrentPlayerFloorNumber;

    GameLifeCycleManager gameLifeCycleManager;

    void OnTriggerEnter2D (Collider2D collision)
    {
        var player = collision.GetComponent<PlayerLifeCycleManager>();

        if (player == null || FloorNumber == CurrentPlayerFloorNumber.Value) return;

        gameLifeCycleManager.PlayerReachedNewFloor(FloorNumber);
    }

    public void Initialize (int floorNumber, GameLifeCycleManager gameLifeCycleManager, LayoutGroup handLayoutGroup)
    {
        FloorNumber = floorNumber;
        this.gameLifeCycleManager = gameLifeCycleManager;

        foreach (var buildZone in BuildZones)
        {
            buildZone.Initialize(floorNumber, handLayoutGroup);
        }
    }
}
