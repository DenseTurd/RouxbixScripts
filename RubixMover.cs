using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    X,
    Y,
    Z
}
public class RubixMover
{
    public bool animateFaceRotation;
    public float animationSpeed = 0.2f;

    bool _clockwise;
    Axis rotateAxis;

    Vector3 x = new Vector3(0, 2, 2);
    Vector3 y = new Vector3(2, 0, 2);
    Vector3 z = new Vector3(2, 2, 0);
    Vector3 xn = new Vector3(4, 2, 2);
    Vector3 yn = new Vector3(2, 4, 2);
    Vector3 zn = new Vector3(2, 2, 4);

    Dictionary<Vector3, Axis> axisDict;

    Vector3 xr = new Vector3(1, 0, 0);
    Vector3 yr = new Vector3(0, 1, 0);
    Vector3 zr = new Vector3(0, 0, 1);

    Dictionary<Axis, Vector3> rotateDict;

    Dictionary<Vector3, int> faceIndex;
    Vector3 bl = new Vector3(0, 0, 0);
    Vector3 ml = new Vector3(0, 2, 0);
    Vector3 tl = new Vector3(0, 4, 0);
    Vector3 tm = new Vector3(2, 4, 0);
    Vector3 tr = new Vector3(4, 4, 0);
    Vector3 mr = new Vector3(4, 2, 0);
    Vector3 br = new Vector3(4, 0, 0);
    Vector3 bm = new Vector3(2, 0, 0);
    Vector3 mm = new Vector3(2, 2, 0);

    GameObject[] indexedCubes;

    public bool moving;
    float t;

    public RubixMover()
    {
        axisDict = new Dictionary<Vector3, Axis>();
        axisDict.Add(x, Axis.X);
        axisDict.Add(y, Axis.Y);
        axisDict.Add(z, Axis.Z);
        axisDict.Add(xn, Axis.X);
        axisDict.Add(yn, Axis.Y);
        axisDict.Add(zn, Axis.Z);

        rotateDict = new Dictionary<Axis, Vector3>();
        rotateDict.Add(Axis.X, xr);
        rotateDict.Add(Axis.Y, yr);
        rotateDict.Add(Axis.Z, zr);

        faceIndex = new Dictionary<Vector3, int>();
        faceIndex.Add(bl, 0);
        faceIndex.Add(ml, 1);
        faceIndex.Add(tl, 2);
        faceIndex.Add(tm, 3);
        faceIndex.Add(tr, 4);
        faceIndex.Add(mr, 5);
        faceIndex.Add(br, 6);
        faceIndex.Add(bm, 7);
        faceIndex.Add(mm, 8);
    }

    public void GenericRotation(GameObject[,,] rubixCube, Vector3 centerCube, bool clockwise)
    {
        _clockwise = clockwise;
        rotateAxis = axisDict[centerCube];

        indexedCubes = new GameObject[9];

        int posOrNeg = 1;

        foreach (var cube in rubixCube)
        {
            Vector3 facePos = new Vector3();
            if (rotateAxis == Axis.X)
            {
                if (cube.transform.position.x == 0 && centerCube.x == 0)
                {
                    posOrNeg = 1;
                    facePos = ToFacePositions(rotateAxis, cube.transform.position, posOrNeg);
                    indexedCubes[faceIndex[facePos]] = cube;
                }
                else if(cube.transform.position.x == 4 && centerCube.x == 4)
                {
                    posOrNeg = -1;
                    facePos = ToFacePositions(rotateAxis, cube.transform.position, posOrNeg);
                    indexedCubes[faceIndex[facePos]] = cube;
                }
            }
            if (rotateAxis == Axis.Y)
            {
                if (cube.transform.position.y == 0 && centerCube.y == 0)
                {
                    posOrNeg = 1;
                    facePos = ToFacePositions(rotateAxis, cube.transform.position, posOrNeg);
                    indexedCubes[faceIndex[facePos]] = cube;
                }
                else if (cube.transform.position.y == 4 && centerCube.y == 4)
                {
                    posOrNeg = -1;
                    facePos = ToFacePositions(rotateAxis, cube.transform.position, posOrNeg);
                    indexedCubes[faceIndex[facePos]] = cube;
                }
            }
            if (rotateAxis == Axis.Z)
            {
                if (cube.transform.position.z == 0 && centerCube.z == 0)
                {
                    posOrNeg = 1;
                    facePos = ToFacePositions(rotateAxis, cube.transform.position, posOrNeg);
                    indexedCubes[faceIndex[facePos]] = cube;
                }
                if (cube.transform.position.z == 4 && centerCube.z == 4)
                {
                    posOrNeg = -1;
                    facePos = ToFacePositions(rotateAxis, cube.transform.position, posOrNeg);
                    indexedCubes[faceIndex[facePos]] = cube;
                    
                }
            } 
        }

        MoveCubes(rotateAxis, posOrNeg);
    }

    void MoveCubes(Axis axis, int posOrNeg)
    {
        List<Vector3> indexedCubesPositions = new List<Vector3>();
        for (int i = 0; i < indexedCubes.Length; i++)
        {
            indexedCubesPositions.Add(indexedCubes[i].transform.position);
        }

        for (int i = 0; i < indexedCubes.Length -1; i++)
        {
            int j;

            // Make it so left click is always clockwise
            if (axis == Axis.Z || axis == Axis.X && posOrNeg == -1 || axis == Axis.Y && posOrNeg == -1)
            {
                if (_clockwise)
                {
                    j = i + 2;
                }
                else
                {
                    j = i - 2;
                }
            }
            else
            {
                if (_clockwise)
                {
                    j = i - 2;
                }
                else
                {
                    j = i + 2;
                }
            }

            if (j > 7)
                j -= 8;

            if (j < 0)
                j += 8;

            if (animateFaceRotation)
            {
                Cube cubeScript = indexedCubes[i].GetComponent<Cube>();
                cubeScript.prevPos = indexedCubes[i].transform.position;
                cubeScript.desiredPos = indexedCubesPositions[j];
            }
            else
            {
                indexedCubes[i].transform.position = indexedCubesPositions[j];
            }

            RotateCube(indexedCubes[i], axis, posOrNeg);
        }
        moving = true;
    }

    // as if camera was facing side in question
    Vector3 ToFacePositions(Axis axis, Vector3 globalPos, int posOrNeg)
    {
        Vector3 facePositions = new Vector3();
        if (posOrNeg == 1)
        {
            if (axis == Axis.X)
            {
                facePositions = new Vector3(globalPos.z, globalPos.y, globalPos.x);
            }
            if (axis == Axis.Y)
            {
                facePositions = new Vector3(globalPos.x, globalPos.z, globalPos.y);
            }
            if (axis == Axis.Z)
            {
                facePositions = new Vector3(globalPos.x, globalPos.y, globalPos.z);
            }
        }
        else
        {
            if (axis == Axis.X)
            {
                facePositions = new Vector3(globalPos.z, globalPos.y, globalPos.x - 4);
            }
            if (axis == Axis.Y)
            {
                facePositions = new Vector3(globalPos.x, globalPos.z, globalPos.y - 4);
            }
            if (axis == Axis.Z)
            {
                facePositions = new Vector3((globalPos.x -4) * posOrNeg, globalPos.y, globalPos.z - 4);
            }
        }

        return facePositions;
    }

    private void RotateCube(GameObject cube, Axis axis, int posOrNeg)
    {
        if (!_clockwise)
            posOrNeg *= -1;

        Cube cubeScript = cube.GetComponent<Cube>();
        cubeScript.prevRot = cube.transform.root.rotation;
        cubeScript.desiredRot = cube.transform.rotation * Quaternion.AngleAxis(-90, rotateDict[axis] * posOrNeg);

    }

    public void Tick()
    {
        if (moving)
        {
            
            t += Time.deltaTime * (0.5f + (animationSpeed * 20f));

            for (int i = 0; i < indexedCubes.Length - 1; i++)
            {
                Cube cubeScript = indexedCubes[i].GetComponent<Cube>();
                indexedCubes[i].transform.rotation = Quaternion.Lerp(cubeScript.prevRot, cubeScript.desiredRot, t);

                if(animateFaceRotation)
                    indexedCubes[i].transform.position = Vector3.Lerp(cubeScript.prevPos, cubeScript.desiredPos, t);
            }

            if (t >= 1)
            {
                t = 0;
                moving = false;
                for (int i = 0; i < indexedCubes.Length - 1; i++)
                {
                    Cube cubeScript = indexedCubes[i].GetComponent<Cube>();
                    indexedCubes[i].transform.rotation = cubeScript.desiredRot;

                    if(animateFaceRotation)
                        indexedCubes[i].transform.position = cubeScript.desiredPos;
                    
                    cubeScript.ResetParentTransform();
                }
            }
        }
    }
}
