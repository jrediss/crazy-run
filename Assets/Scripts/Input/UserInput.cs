using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] private float minSwipeDistanceX = 100f;
    [SerializeField] private float maxSwipeDeviationY = 50f;

    private Vector2 _swipeStartPos;
    private Vector2 _swipeEndPos;

    private Vector2[] _zoomStartPos = new Vector2[2];
    private Vector2[] _zoomEndPos = new Vector2[2];

    void Update()
    {
        RegisterSwipeRight();
        RegisterZoom();
    }

    private void RegisterZoom()
    {
        if (Input.touchCount > 1)
        {
            Touch touch = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            if (touch.phase == TouchPhase.Began && touch1.phase == TouchPhase.Began)
            {
                _zoomStartPos[0] = touch.position;
                _zoomStartPos[1] = touch1.position;
            }

            if (touch.phase == TouchPhase.Ended && touch1.phase == TouchPhase.Ended)
            {
                _zoomEndPos[0] = touch.position;
                _zoomEndPos[1] = touch1.position;
                if (IsZoom())
                {
                    Debug.Log("Жест увеличение");
                }
            }
        }
    }

    private void RegisterSwipeRight()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _swipeStartPos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                _swipeEndPos = touch.position;
                if (IsSwipeRight())
                {
                    Debug.Log("Свайп вправо");
                }
            }
        }
    }

    private bool IsSwipeRight()
    {
        float deltaX = _swipeEndPos.x - _swipeStartPos.x;
        float deltaY = Mathf.Abs(_swipeEndPos.y - _swipeStartPos.y);
        return deltaX >= minSwipeDistanceX && deltaY <= maxSwipeDeviationY;
    }

    private bool IsZoom()
    {
        float startDistance = Vector2.Distance(_zoomStartPos[0], _zoomStartPos[1]);
        float endDistance = Vector2.Distance(_zoomEndPos[0], _zoomEndPos[1]);
        return startDistance < endDistance;
    }
}
