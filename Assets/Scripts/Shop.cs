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

    public void SelectSoldier()
    {
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.tank1Prefab);
    }

    public void SelectTank()
    {
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.tank2Prefab);
    }

    public void SelectMissileLauncher()
    {
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.tank3Prefab);
    }
}