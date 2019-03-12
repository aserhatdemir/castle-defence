using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    GameManager gameManager;
    private GameObject weapon;
    private GameObject weaponToCreate;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        weaponToCreate = gameManager.weaponManagerScript.GetWeaponToCreate();
        if (weaponToCreate == null)
            return;
        SpawnTeamBlueWeapon();
    }

    void SpawnTeamBlueWeapon()
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        weapon = Instantiate(weaponToCreate, new Vector2(mousePositionInWorld.x, mousePositionInWorld.y), Quaternion.identity);
        weapon.tag = "TeamBlue";
    }
}
