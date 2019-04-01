using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankUpgradePanel : MonoBehaviour
{
    public Weapon weaponPrefab;

    private UpgradeButtonBehaviour damageButton;
    private UpgradeButtonBehaviour speedButton;
    private UpgradeButtonBehaviour healthButton;
    private UpgradeButtonBehaviour rangeButton;
    private UpgradeButtonBehaviour attackSpeedButton;

    private UpgradeButtonBehaviour aimingSpeedButton;

    // Start is called before the first frame update
    void Start()
    {
        var upgradePanel = transform.Find("UpgradeButtons");
        damageButton = upgradePanel.Find("Damage").GetComponent<UpgradeButtonBehaviour>();
        speedButton = upgradePanel.Find("Speed").GetComponent<UpgradeButtonBehaviour>();
        healthButton = upgradePanel.Find("Health").GetComponent<UpgradeButtonBehaviour>();
        rangeButton = upgradePanel.Find("Range").GetComponent<UpgradeButtonBehaviour>();
        attackSpeedButton = upgradePanel.Find("AttackSpeed").GetComponent<UpgradeButtonBehaviour>();
        aimingSpeedButton = upgradePanel.Find("AimingSpeed").GetComponent<UpgradeButtonBehaviour>();
    }

    private void RefreshAllButtons()
    {
        damageButton.refreshUI(weaponPrefab.GetUpgradableAttributes().damage);
        speedButton.refreshUI(weaponPrefab.GetUpgradableAttributes().speed);
        healthButton.refreshUI(weaponPrefab.GetUpgradableAttributes().health);
        rangeButton.refreshUI(weaponPrefab.GetUpgradableAttributes().range);
        attackSpeedButton.refreshUI(weaponPrefab.GetUpgradableAttributes().attackSpeed);
        aimingSpeedButton.refreshUI(weaponPrefab.GetUpgradableAttributes().aimingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpgradeDamage()
    {
        weaponPrefab.GetUpgradableAttributes().damage.LevelUp();
        damageButton.refreshUI(weaponPrefab.GetUpgradableAttributes().damage);
    }

    public void UpgradeSpeed()
    {
        weaponPrefab.GetUpgradableAttributes().speed.LevelUp();
        speedButton.refreshUI(weaponPrefab.GetUpgradableAttributes().speed);
    }

    public void UpgradeHealth()
    {
        weaponPrefab.GetUpgradableAttributes().health.LevelUp();
        healthButton.refreshUI(weaponPrefab.GetUpgradableAttributes().health);
    }

    public void UpgradeRange()
    {
        weaponPrefab.GetUpgradableAttributes().range.LevelUp();
        rangeButton.refreshUI(weaponPrefab.GetUpgradableAttributes().range);
    }

    public void UpgradeAttackSpeed()
    {
        weaponPrefab.GetUpgradableAttributes().attackSpeed.LevelUp();
        attackSpeedButton.refreshUI(weaponPrefab.GetUpgradableAttributes().attackSpeed);
    }

    public void UpgradeAimingSpeed()
    {
        weaponPrefab.GetUpgradableAttributes().aimingSpeed.LevelUp();
        aimingSpeedButton.refreshUI(weaponPrefab.GetUpgradableAttributes().aimingSpeed);
    }
}