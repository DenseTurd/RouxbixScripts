using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    Rubix rubix;
    private void Start()
    {
        rubix = FindObjectOfType<Rubix>();
    }
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            Vector3Int hitCubePosition = hit.transform.gameObject.GetComponent<Plane>().GetParentPosition();
    //            Debug.Log(hitCubePosition);
    //            // clockwise
    //            rubix.MoveRubix(hitCubePosition, true);
    //        }
    //    }

    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            Vector3Int hitCubePosition = hit.transform.gameObject.GetComponent<Plane>().GetParentPosition();
    //            Debug.Log(hitCubePosition);
    //            // counter clockwise
    //            rubix.MoveRubix(hitCubePosition, false);
    //        }
    //    }
    //}

    public void Select(Vector2 pos, bool clockWise)
    {
        var ray = Camera.main.ScreenPointToRay(pos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3Int hitCubePosition = hit.transform.gameObject.GetComponent<Plane>().GetParentPosition();
            Debug.Log(hitCubePosition);
            // counter clockwise
            rubix.MoveRubix(hitCubePosition, clockWise);
        }
    }
}
