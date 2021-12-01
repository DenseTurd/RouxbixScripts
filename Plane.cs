using UnityEngine;

public class Plane : MonoBehaviour
{
    public int side;

    public Vector3Int GetParentPosition()
    {
        Cube cube = transform.GetComponentInParent<Cube>();
        Vector3Int pos = new Vector3Int(cube.me.x, cube.me.y, cube.me.z);
        return pos;
    }

    public void ForgetParent()
    {
        transform.parent = null;
    }
    public void SetParent(Transform parent)
    {
        transform.parent = parent;
    }
}
