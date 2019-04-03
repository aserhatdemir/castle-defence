using System.Collections.Generic;
using UnityEngine;

public class TankUpgradePanel : MonoBehaviour
{
    public Weapon weaponPrefab;
    public Dictionary<UpgradeButtonBehaviour, WeaponUpgradableAttributes.WeaponAttribute> buttonsWithWeaponAttributes;

    private UpgradeButtonBehaviour damageButton;
    private UpgradeButtonBehaviour speedButton;
    private UpgradeButtonBehaviour healthButton;
    private UpgradeButtonBehaviour rangeButton;
    private UpgradeButtonBehaviour attackSpeedButton;
    private UpgradeButtonBehaviour aimingSpeedButton;
    
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
        
        //keep upgrade button and related weapon upgrade attribute together
        buttonsWithWeaponAttributes = new Dictionary<UpgradeButtonBehaviour, WeaponUpgradableAttributes.WeaponAttribute>()
        {
            {damageButton, weaponPrefab.GetUpgradableAttributes().damage},
            {speedButton, weaponPrefab.GetUpgradableAttributes().speed},
            {healthButton, weaponPrefab.GetUpgradableAttributes().health},
            {rangeButton, weaponPrefab.GetUpgradableAttributes().range},
            {attackSpeedButton, weaponPrefab.GetUpgradableAttributes().attackSpeed},
            {aimingSpeedButton, weaponPrefab.GetUpgradableAttributes().aimingSpeed}
        };
        
    }

    public void RefreshAllButtons()
    {
        foreach (KeyValuePair<UpgradeButtonBehaviour, WeaponUpgradableAttributes.WeaponAttribute> buttonAttributePair in buttonsWithWeaponAttributes)
        {
            buttonAttributePair.Key.RefreshUI(buttonAttributePair.Value);
        }
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