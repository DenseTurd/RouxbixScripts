using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubix : MonoBehaviour
{
    GameObject[,,] rubixCube;

    public GameObject prefab;

    public RubixMover rubixMover;

    public Vector3 centerCubeOfFaceToRotate;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        rubixMover = new RubixMover();
        rubixCube = new GameObject[3, 3, 3];
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    rubixCube[x, y, z] = Instantiate(prefab, transform);
                    prefab.GetComponent<Cube>().Init(new Vector3Int(x * 2,y * 2,z * 2));
                }
            }
        }
    }

    public void MoveRubix(Vector3 selectedCubePosition, bool clockwise)
    {
        if(!rubixMover.moving)
            rubixMover.GenericRotation(rubixCube, selectedCubePosition, clockwise);
    }

    public int BallsTest()
    {
        return 1;
    }

    private void Update()
    {
        rubixMover.Tick();
    }
}
