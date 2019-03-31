using UnityEngine;

public class Shop : MonoBehaviour
{
    //WeaponManager weaponManager;
    GameManager gameManager;
    private GameObject selectedWeapon;
    public Factory factory1;
    public Factory factory2;
    public Factory factory3;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }


    private void SelectTank(GameObject prefab, Factory factory)
    {
        CreateWeapon(prefab, factory);
//        if (selectedWeapon == prefab)
//        {
//            CreateWeapon(prefab, factory);
//            hideUpgradeUI();
//        }
//        else
//        {
//            selectedWeapon = prefab;
//            showUpgradeUI();
//        }
    }

    private void CreateWeapon(GameObject prefab, Factory factory)
    {
        factory.AddToQueue(prefab);
        
        var weaponPrice = prefab.GetComponent<Weapon>().price;
        if (PlayerStats.Money < weaponPrice)
        {
            Debug.Log("Not enough money!");
            return;
        }
        else
        {
            PlayerStats.Money -= weaponPrice;
            gameManager.playerStatsScript.UpdateMoneyTextUI();
        }
    }

    public void SelectTank1()
    {
        var prefab = gameManager.weaponManagerScript.tank1Prefab;
        var factory = factory1;
        SelectTank(prefab, factory);
    }

    public void SelectTank2()
    {
        var prefab = gameManager.weaponManagerScript.tank2Prefab;
        var factory = factory2;
        SelectTank(prefab, factory);
    }

    public void SelectTank3()
    {
        var prefab = gameManager.weaponManagerScript.tank3Prefab;
        var factory = factory3;
        SelectTank(prefab, factory);
    }
}