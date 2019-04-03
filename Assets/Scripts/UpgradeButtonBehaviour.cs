using TMPro;
using UnityEngine;

public class UpgradeButtonBehaviour : MonoBehaviour
{
    private TextMeshProUGUI level;
    private TextMeshProUGUI price;

    // Start is called before the first frame update
    void Awake()
    {
        level = transform.Find("Level").GetComponent<TextMeshProUGUI>();
        price = transform.Find("Price").GetComponent<TextMeshProUGUI>();
    }

    public void RefreshUI(WeaponUpgradableAttributes.WeaponAttribute weaponAttribute)
    {
        level.text = "Lv " + weaponAttribute.level.ToString();
        price.text = "$" + weaponAttribute.upgradeCurrentPrice.ToString();
    }
}