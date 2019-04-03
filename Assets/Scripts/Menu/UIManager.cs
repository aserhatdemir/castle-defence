using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI totalTimeText;
    public Shop shopPanel;
    
    private GameManager gameManager;
    private TankUpgradePanel[] upgradePanels;
    private Button[] upgradeButtons;
    private Button[] shopButtons;
    private TankUpgradePanel activeUpgradePanel;

    private GameObject[] prefabs;

    private void Start()
    {
        gameManager = GameManager.instance;
        shopButtons = shopPanel.GetComponentsInChildren<Button>();
    }

    public void FactorySelected(Factory factory)
    {
        if (factory.upgradePanel.activeSelf)
        {
            factory.upgradePanel.SetActive(false);
            
            if (shopButtons == null) return;    //Enable all shop buttons
            foreach (Button shopButton in shopButtons)
            {
                shopButton.interactable = true;
            }
            
            return;
        }
        CloseAllActiveUpgradePanels();
        OpenUpgradePanel(factory.upgradePanel);
    }

    private void OpenUpgradePanel(GameObject upgradePanel)
    {
        upgradePanel.SetActive(true);
        upgradePanel.GetComponent<TankUpgradePanel>().RefreshAllButtons();
        activeUpgradePanel = upgradePanel.GetComponent<TankUpgradePanel>();
        CheckUpgradeButtonsBudget(activeUpgradePanel);
        
        if (shopButtons == null) return;    //Disable all shop buttons
        foreach (Button shopButton in shopButtons)
        {
            shopButton.interactable = false;
        }
    }

    private void CloseAllActiveUpgradePanels()
    {
        upgradePanels = GameObject.FindObjectsOfType<TankUpgradePanel>();
        foreach (TankUpgradePanel tankUpgradePanel in upgradePanels)
        {
            if (tankUpgradePanel.gameObject.activeSelf)
            {
                tankUpgradePanel.gameObject.SetActive(false);
            }
        }
    }

    public void UpdateMoneyTextUI()
    {
        moneyText.text = "$" + PlayerStats.Money.ToString();
        CheckShopButtonsBudget();
        CheckUpgradeButtonsBudget(activeUpgradePanel);
    }

    private void CheckUpgradeButtonsBudget(TankUpgradePanel upgradePanel)
    {
        if (!upgradePanel) return;
        CheckUpgradeButtonBudget(upgradePanel.damageButton, upgradePanel.weaponPrefab.GetUpgradableAttributes().damage);
        CheckUpgradeButtonBudget(upgradePanel.speedButton, upgradePanel.weaponPrefab.GetUpgradableAttributes().speed);
        CheckUpgradeButtonBudget(upgradePanel.healthButton, upgradePanel.weaponPrefab.GetUpgradableAttributes().health);
        CheckUpgradeButtonBudget(upgradePanel.rangeButton, upgradePanel.weaponPrefab.GetUpgradableAttributes().range);
        CheckUpgradeButtonBudget(upgradePanel.attackSpeedButton, upgradePanel.weaponPrefab.GetUpgradableAttributes().attackSpeed);
        CheckUpgradeButtonBudget(upgradePanel.aimingSpeedButton, upgradePanel.weaponPrefab.GetUpgradableAttributes().aimingSpeed);
    }

    void CheckUpgradeButtonBudget(UpgradeButtonBehaviour button, WeaponUpgradableAttributes.WeaponAttribute upgrade)
    {
        if (PlayerStats.Money < upgrade.upgradeCurrentPrice)
        {
            button.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            button.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    private void CheckShopButtonsBudget()
    {
        if (shopButtons == null) return;
        foreach (Button shopButton in shopButtons)
        {
            if (PlayerStats.Money < shopButton.GetComponent<ShopButton>().weaponPrefab.GetComponent<Weapon>().price)
            {
                shopButton.interactable = false;
            }
            else
            {
                if (!shopButton.interactable && !shopButton.GetComponent<ShopButton>().factoryDestroyed)
                {
                    shopButton.interactable = true;
                }
            }
        }
    }

    public void UpdateTotalTimeTextUI()
    {
        totalTimeText.text = string.Format("{0:00.0}", PlayerStats.TotalTime);
    }

    public void DisableShopButton(Button shopButton)
    {
        shopButton.interactable = false;
    }
}