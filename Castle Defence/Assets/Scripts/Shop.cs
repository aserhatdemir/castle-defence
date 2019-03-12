using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //WeaponManager weaponManager;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void PurchaseSoldier()
    {
        Debug.Log("Soldier purchased");
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.soldierPrefab);
    }

    public void PurchaseTank()
    {
        Debug.Log("Tank purchased");
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.tankPrefab);
    }

    public void PurchaseMissileLauncher()
    {
        Debug.Log("MissileLauncher purchased");
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.MissileLauncherPrefab);
    }


}
