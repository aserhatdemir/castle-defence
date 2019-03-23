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

    public void SelectTank1()
    {
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.tank1Prefab);
    }

    public void SelectTank2()
    {
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.tank2Prefab);
    }

    public void SelectTank3()
    {
        gameManager.weaponManagerScript.SetWeaponToCreate(gameManager.weaponManagerScript.tank3Prefab);
    }
}