using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideColorSetter : MonoBehaviour
{
    public static SideColorSetter Instance { get; private set; }

    public Material left;
    public Material bottom;
    public Material front;
    public Material right;
    public Material top;
    public Material back;
    public Material grey;

    Dictionary<int, Material> colorDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        colorDict = new Dictionary<int, Material>()
        {
            {0, left },
            {1, bottom },
            {2, front },
            {3, right },
            {4, top },
            {5, back }
        };
    }

    public Material GetColor(Vector3Int cubeLocation, int side)
    {
        if(cubeLocation.x == 0 && side == 0)
        {
            return left;
        }
        if(cubeLocation.x == 4 && side == 3)
        {
            return right;
        }
        if(cubeLocation.y == 0 && side == 1)
        {
            return bottom;
        }
        if(cubeLocation.y == 4 && side == 4)
        {
            return top;
        }
        if(cubeLocation.z == 0 && side == 2)
        {
            return front;
        }
        if(cubeLocation.z == 4 && side == 5)
        {
            return back;
        }
        else
        {
            return grey;
        }
    }
}
