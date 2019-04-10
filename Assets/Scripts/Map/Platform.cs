using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Platform : MonoBehaviour
{
    private GameManager gameManager;
    private Camera mainCamera;
    private Vector3 mousePositionInWorld;
    
    public GameObject clickEffectPrefab;
    
//    public Texture2D cursorTexture;
//    public CursorMode cursorMode = CursorMode.Auto;
//    public Vector2 hotSpot = Vector2.zero;

    private void Start()
    {
        gameManager = GameManager.instance;
        mainCamera = Camera.main;
    }


    private void Update()
    {
        if (gameManager.deviceType == "Handheld")
        {
            if (Input.touchCount <= 0 || Input.GetTouch(0).phase != TouchPhase.Began) return;
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
            ChangeClickDestination();
            ShowClickDestinationAnimation();
        }
        else   //PC mouse pointer
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;
            ChangeClickDestination();
            ShowClickDestinationAnimation();
        }
    }

    private void ChangeClickDestination()
    {
        mousePositionInWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePositionInWorld.z = 0;
        gameManager.weaponManagerScript.ChangeClickedDestination(mousePositionInWorld);
    }

    private void ShowClickDestinationAnimation()
    {
        GameObject cEffect = (GameObject) Instantiate(clickEffectPrefab, mousePositionInWorld,
            clickEffectPrefab.transform.rotation);
        Destroy(cEffect, 0.3f);
    }

//    void OnMouseEnter()
//    {
//        Debug.Log("tikladi");
//        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
//    }
//
//    void OnMouseExit()
//    {
//        Debug.Log("birakti");
//        Cursor.SetCursor(null, Vector2.zero, cursorMode);
//    }
//
//    private void OnMouseDown()
//    {
//        if (Camera.main != null)
//        {
//            if (!EventSystem.current.IsPointerOverGameObject()) return;
//            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            mousePositionInWorld.z = 0;
//            gameManager.weaponManagerScript.ChangeClickedDestination(mousePositionInWorld);
//        }
//    }
}