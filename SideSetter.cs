using System.Collections.Generic;
using UnityEngine;
public class SideSetter 
{
    Dictionary<int, int[]> sidePositioner = new Dictionary<int, int[]>();
    Dictionary<int, int[]> sideRotator = new Dictionary<int, int[]>();

    public SideSetter()
    {
        sidePositioner.Add(0, new int[3] { -1, 0, 0 });
        sidePositioner.Add(1, new int[3] { 0, -1, 0 });
        sidePositioner.Add(2, new int[3] { 0, 0, -1 });
        sidePositioner.Add(3, new int[3] { 1, 0, 0 });
        sidePositioner.Add(4, new int[3] { 0, 1, 0 });
        sidePositioner.Add(5, new int[3] { 0, 0, 1 });

        sideRotator.Add(0, new int[3] {   0,  0, 90 });
        sideRotator.Add(1, new int[3] { 180,  0,  0 });
        sideRotator.Add(2, new int[3] { -90,  0,  0 });
        sideRotator.Add(3, new int[3] {   0,  0,-90 });
        sideRotator.Add(4, new int[3] {   0,-90,  0 });
        sideRotator.Add(5, new int[3] {  90,  0,  0 });
    }

    public Vector3 SetSidePosition(int side)
    {
        return new Vector3(sidePositioner[side][0], sidePositioner[side][1], sidePositioner[side][2]);
    }

    public Vector3 SideSetRotation(int side)
    {
        return new Vector3(sideRotator[side][0], sideRotator[side][1], sideRotator[side][2]);
    }
}
