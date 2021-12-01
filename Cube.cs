using System;
using System.Collections.Generic;
using UnityEngine;
public class Cube : MonoBehaviour
{
    SideSetter sideSetter;
    public Vector3Int me;

    public Vector3 prevPos;
    public Vector3 desiredPos;

    public Quaternion prevRot;
    public Quaternion desiredRot;
    public Quaternion lerpRot;

    List<Transform> children;
    public void Init(Vector3Int id)
    {
        sideSetter = new SideSetter();
        transform.position = id;
        me = id;
        ArrangeSides();
    }

    void ArrangeSides()
    {
        for (int i = 0; i < 6; i++)
        {
            Transform child = transform.GetChild(i);
            child.localPosition = sideSetter.SetSidePosition(i);
            child.rotation = Quaternion.Euler(sideSetter.SideSetRotation(i));
            child.GetComponent<MeshRenderer>().material = SideColorSetter.Instance.GetColor(me, i);
            child.GetComponent<Plane>().side = i;
        }
    }

    public void ResetParentTransform()
    {
        children = new List<Transform>();

        for (int i = 0; i < 6; i++)
        {
            Transform child = transform.GetChild(i);
            children.Add(child);
        }

        for (int i = 0; i < 6; i++)
        {  
            children[i].GetComponent<Plane>().ForgetParent();
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        for (int i = 0; i < 6; i++)
        {
            children[i].GetComponent<Plane>().SetParent(transform);
        }
    }
}
