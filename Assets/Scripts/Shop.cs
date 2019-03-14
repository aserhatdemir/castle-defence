﻿using UnityEngine;

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

    public void SelectSoldier()
    {
        Debug.Log("Soldier selected");
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.soldierPrefab);
    }

    public void SelectTank()
    {
        Debug.Log("Tank selected");
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.tankPrefab);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("MissileLauncher selected");
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.missileLauncherPrefab);
    }
}