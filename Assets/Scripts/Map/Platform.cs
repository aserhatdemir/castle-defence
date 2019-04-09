using UnityEngine;
using UnityEngine.UI;

public class Platform : MonoBehaviour
{
    private GameManager gameManager;
    private Camera mainCamera;

    private void Start()
    {
        gameManager = GameManager.instance;
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        if (Camera.main != null)
        {
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePositionInWorld.z = 0;
            gameManager.weaponManagerScript.ChangeClickedDestination(mousePositionInWorld);
        }
    }
}