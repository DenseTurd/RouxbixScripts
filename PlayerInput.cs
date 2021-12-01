using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public CamMover camMover;
    public SelectionManager selectionManager;

    public bool clockWise = true;

    Vector2 firstPoint;
    Vector2 secondPoint;

    Vector2 prevPos;

    bool reverseHorizontal;
    bool reverseVertical;

    public bool pCControls;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        prevPos = Input.mousePosition;
    }

    private void Update()
    {
        if (pCControls)
        {
            HandleMouse();
        }
        else
        {
            HandleSwipe();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void HandleSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                firstPoint = touch.position;
                selectionManager.Select(touch.position, clockWise);
            }
            if (touch.phase == TouchPhase.Moved)
            {
                secondPoint = touch.position;

                var rotateAmmountHor = firstPoint.x - secondPoint.x;
                var rotateAmmountVer = firstPoint.y - secondPoint.y;

                camMover.ManualRotation(rotateAmmountVer, -rotateAmmountHor);

                firstPoint = secondPoint;
            }
        }
    }

    public void HandleMouse() // swipe sensitivity wont change mouse swipe
    {
        if (Input.GetMouseButton(2))
        {
            float correctDirectionRotationHorizontal = prevPos.x - Input.mousePosition.x;
            float correctDirectionRotationVertical = prevPos.y - Input.mousePosition.y;

            if (reverseHorizontal)
                correctDirectionRotationHorizontal = -correctDirectionRotationHorizontal;

            if (reverseVertical)
                correctDirectionRotationVertical = -correctDirectionRotationVertical;

            camMover.ManualRotation(correctDirectionRotationVertical, -correctDirectionRotationHorizontal);
        }
        prevPos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            selectionManager.Select(Input.mousePosition, true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            selectionManager.Select(Input.mousePosition, false);
        }
    }

    public void ToggleClockwise(Toggle toggle)
    {
        clockWise = toggle.isOn;
    }
}
