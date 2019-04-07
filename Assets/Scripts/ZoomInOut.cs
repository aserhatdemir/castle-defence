using UnityEngine;
using UnityEngine.UI;

public class ZoomInOut : MonoBehaviour
{
    Camera mainCamera;

    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    private Vector3 cameraPosition;

    [SerializeField] float zoomModifierSpeed = 0.01f;

    [SerializeField] Text text;
    private int prevTouchCount = 0;

    private Vector3 wantToGo;

    void Start()
    {
//		mainCamera = GetComponent<Camera>();

        mainCamera = Camera.main;
        cameraPosition = mainCamera.transform.position;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);
            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;
            if (prevTouchCount < 2)
            {
                var x = ((firstTouch.position.x + secondTouch.position.x) / 2);
                var y = (firstTouch.position.y + secondTouch.position.y) / 2;
                var tmp = new Vector3(x, y);
                var newPos = mainCamera.ScreenToWorldPoint(tmp);
                newPos.z = cameraPosition.z;
                wantToGo = newPos;
            }


            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;
            mainCamera.transform.position += (wantToGo - mainCamera.transform.position) * zoomModifier;

            if (touchesPrevPosDifference > touchesCurPosDifference)
                mainCamera.orthographicSize += zoomModifier;
            if (touchesPrevPosDifference < touchesCurPosDifference)
                mainCamera.orthographicSize -= zoomModifier;
        }

        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 2f, 10f);
        text.text = "Camera size " + mainCamera.orthographicSize;
        prevTouchCount = Input.touchCount;
    }
}