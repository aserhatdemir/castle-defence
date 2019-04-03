using UnityEngine;

public class TankUpgradePanel : MonoBehaviour
{
    public Weapon weaponPrefab;

    public UpgradeButtonBehaviour damageButton;
    public UpgradeButtonBehaviour speedButton;
    public UpgradeButtonBehaviour healthButton;
    public UpgradeButtonBehaviour rangeButton;
    public UpgradeButtonBehaviour attackSpeedButton;
    public UpgradeButtonBehaviour aimingSpeedButton;
    
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.instance;
        var upgradePanel = transform.Find("UpgradeButtons");
        damageButton = upgradePanel.Find("Damage").GetComponent<UpgradeButtonBehaviour>();
        speedButton = upgradePanel.Find("Speed").GetComponent<UpgradeButtonBehaviour>();
        healthButton = upgradePanel.Find("Health").GetComponent<UpgradeButtonBehaviour>();
        rangeButton = upgradePanel.Find("Range").GetComponent<UpgradeButtonBehaviour>();
        attackSpeedButton = upgradePanel.Find("AttackSpeed").GetComponent<UpgradeButtonBehaviour>();
        aimingSpeedButton = upgradePanel.Find("AimingSpeed").GetComponent<UpgradeButtonBehaviour>();
        
    }

    public void RefreshAllButtons()
    {
        damageButton.RefreshUI(weaponPrefab.GetUpgradableAttributes().damage);
        speedButton.RefreshUI(weaponPrefab.GetUpgradableAttributes().speed);
        healthButton.RefreshUI(weaponPrefab.GetUpgradableAttributes().health);
        rangeButton.RefreshUI(weaponPrefab.GetUpgradableAttributes().range);
        attackSpeedButton.RefreshUI(weaponPrefab.GetUpgradableAttributes().attackSpeed);
        aimingSpeedButton.RefreshUI(weaponPrefab.GetUpgradableAttributes().aimingSpeed);
    }
    
    private bool HaveBudgetForUpgrade(WeaponUpgradableAttributes.WeaponAttribute upgrade)
    {
        var upgradePrice = upgrade.upgradeCurrentPrice;
        if (PlayerStats.Money < upgradePrice)
        {
            Debug.Log("Not enough money!");
            return false;
        }
        gameManager.playerStatsScript.UpdateMoney(upgradePrice * -1);
        return true;
    }

    public void UpgradeDamage()
    {
        if (!HaveBudgetForUpgrade(weaponPrefab.GetUpgradableAttributes().damage)) return;
        weaponPrefab.GetUpgradableAttributes().damage.LevelUp();
        damageButton.RefreshUI(weaponPrefab.GetUpgradableAttributes().damage);
    }

    public void UpgradeSpeed()
    {
        if (!HaveBudgetForUpgrade(weaponPrefab.GetUpgradableAttributes().speed)) return;
        weaponPrefab.GetUpgradableAttributes().speed.LevelUp();
        speedButton.RefreshUI(weaponPrefab.GetUpgradableAttributes().speed);
    }

    public void UpgradeHealth()
    {
        if (!HaveBudgetForUpgrade(weaponPrefab.GetUpgradableAttributes().health)) return;
        weaponPrefab.GetUpgradableAttributes().health.LevelUp();
        healthButton.RefreshUI(weaponPrefab.GetUpgradableAttributes().health);
    }

    public void UpgradeRange()
    {
        if (!HaveBudgetForUpgrade(weaponPrefab.GetUpgradableAttributes().range)) return;
        weaponPrefab.GetUpgradableAttributes().range.LevelUp();
        rangeButton.RefreshUI(weaponPrefab.GetUpgradableAttributes().range);
    }

    public void UpgradeAttackSpeed()
    {
        if (!HaveBudgetForUpgrade(weaponPrefab.GetUpgradableAttributes().attackSpeed)) return;
        weaponPrefab.GetUpgradableAttributes().attackSpeed.LevelUp();
        attackSpeedButton.RefreshUI(weaponPrefab.GetUpgradableAttributes().attackSpeed);
    }

    public void UpgradeAimingSpeed()
    {
        if (!HaveBudgetForUpgrade(weaponPrefab.GetUpgradableAttributes().aimingSpeed)) return;
        weaponPrefab.GetUpgradableAttributes().aimingSpeed.LevelUp();
        aimingSpeedButton.RefreshUI(weaponPrefab.GetUpgradableAttributes().aimingSpeed);
    }
}