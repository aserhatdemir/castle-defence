using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Factory : Building
{
    public enum ProductionState
    {
        IDLE,
        PRODUCING
    }

    public Slider countdownBar;
    public TextMeshProUGUI prodQueueSizeText;
    public GameObject upgradePanel;
    public Button shopButton;

    private float productionTime;

    private GameObject weapon;
    private Queue<GameObject> toBeCreatedQueue = new Queue<GameObject>();
    private float productionTimeStart;
    private GameObject toBeCreated;
    private ProductionState state = ProductionState.IDLE;
    private float prodTimeCounter;


    void Update()
    {
        if (state == ProductionState.IDLE)
        {
            if (toBeCreatedQueue.Count > 0)
            {
                state = ProductionState.PRODUCING;
                prodQueueSizeText.text = toBeCreatedQueue.Count.ToString();
                toBeCreated = toBeCreatedQueue.Dequeue();
                productionTimeStart = Time.time;
            }
        }
        else
        {
            productionTime = toBeCreated.GetComponent<Weapon>().productionTime;
            float elapsedTime = Time.time - productionTimeStart;
            UpdateProductionCtrBar(elapsedTime);
            if (productionTime <= elapsedTime)
            {
                CreateWeapon(toBeCreated);
                toBeCreated = null;
                state = ProductionState.IDLE;
            }
        }
    }

    private void CreateWeapon(GameObject prefab)
    {
        GameManager.instance.weaponManagerScript.CreateWeapon(prefab, this);
    }

    public void AddToQueue(GameObject prefab)
    {
        toBeCreatedQueue.Enqueue(prefab);
        prodQueueSizeText.text = toBeCreatedQueue.Count.ToString();
    }

    void UpdateProductionCtrBar(float t)
    {
        if (countdownBar != null)
        {
            countdownBar.value = t / productionTime;
        }
    }

    public void ShowHideUpgradeUI()
    {
        GameManager.instance.uiManager.FactorySelected(this);
    }

    public void UpgradeTank1()
    {
        ShowHideUpgradeUI();
    }

    public void UpgradeTank2()
    {
        ShowHideUpgradeUI();
    }

    public void UpgradeTank3()
    {
        ShowHideUpgradeUI();
    }
}