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
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.soldierPrefab);
    }

    public void SelectTank()
    {
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.tankPrefab);
    }

    public void SelectMissileLauncher()
    {
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.missileLauncherPrefab);
    }
}