using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeButtonBehaviour : MonoBehaviour
{
    private TextMeshProUGUI level;

    private TextMeshProUGUI price;

    // Start is called before the first frame update
    void Start()
    {
        level = transform.Find("Level").GetComponent<TextMeshProUGUI>();
        price = transform.Find("Price").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void refreshUI(WeaponUpgradableAttributes.WeaponAttribute weaponAttribute)
    {
        level.text = "Lv " + weaponAttribute.level.ToString();
        price.text = "$5";
    }
}