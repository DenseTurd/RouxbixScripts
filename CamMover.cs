using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMover : MonoBehaviour
{
    Vector2 orbitAngles = new Vector2(38f, -38f);

    public Vector3 focusPoint;

    [Range(10, 50)]
    public float distance;

    [Range(1, 100)]
    public float rotationSpeed;
    void Update()
    {
        Quaternion lookRotation = Quaternion.Euler(orbitAngles);
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focusPoint - (lookDirection * distance);
        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    public void ManualRotation(float hor, float ver)
    {
        Vector2 input = new Vector2(hor, ver);

        orbitAngles += input * rotationSpeed * Time.deltaTime;
    }
}
