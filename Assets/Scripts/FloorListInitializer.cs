using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorListInitializer : MonoBehaviour
{
    public FloorList FloorList;
    public GameLifeCycleManager GameLifeCycleManager;
    public LayoutGroup HandLayoutGroup;

    public void Start ()
    {
        FloorList.Floors = new List<Floor>();

        int floorNumber = 0;
        foreach (var floor in GetComponentsInChildren<Floor>())
        {
            floor.Initialize(floorNumber, GameLifeCycleManager, HandLayoutGroup);
            FloorList.Floors.Add(floor);

            floorNumber++;
        }
    }
}
