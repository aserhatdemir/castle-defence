using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TankUpgradePanel[] factoryPanels;

    public void FactorySelected(Factory factory)
    {
        if (factory.upgradePanel.activeSelf)
        {
            factory.upgradePanel.SetActive(false);
            return;
        }
        CloseAllActiveUpgradePanels();
        factory.upgradePanel.SetActive(true);
    }

    private void CloseAllActiveUpgradePanels()
    {
        factoryPanels = GameObject.FindObjectsOfType<TankUpgradePanel>();
        foreach (TankUpgradePanel tankUpgradePanel in factoryPanels)
        {
            if (tankUpgradePanel.gameObject.activeSelf)
            {
                tankUpgradePanel.gameObject.SetActive(false);
            }
        }
    }
}