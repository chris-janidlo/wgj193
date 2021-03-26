using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

[CreateAssetMenu(menuName = "Floor List", fileName = "newFloorList.asset")]
public class FloorList : ScriptableObject
{
    public List<Floor> Floors { get; set; }

    public Floor CurrentPlayerFloorObject => Floors[CurrentPlayerFloorNumberVariable.Value];

    public IntVariable CurrentPlayerFloorNumberVariable;
}
