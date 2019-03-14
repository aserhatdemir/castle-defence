using UnityEngine;
using UnityEngine.UI;

public class Platform : MonoBehaviour
{
    GameManager gameManager;
    public Text moneyText;
    private GameObject weapon;
    private int weaponPrice;
    private GameObject weaponToCreate;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        moneyText.text = "$" + PlayerStats.money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        weaponToCreate = gameManager.weaponManagerScript.GetWeaponToCreate();
        if (weaponToCreate == null)
            return;
        weaponPrice = weaponToCreate.GetComponent<Weapon>().price;
        if (PlayerStats.money < weaponPrice)
        {
            Debug.Log("Not enough money!");
            return;
        }

        SpawnTeamBlueWeapon();
    }

    void SpawnTeamBlueWeapon()
    {
        if (Camera.main == null) return;
        PlayerStats.money -= weaponPrice;
        moneyText.text = "$" + PlayerStats.money.ToString();
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        weapon = Instantiate(weaponToCreate, new Vector2(mousePositionInWorld.x, mousePositionInWorld.y),
            Quaternion.identity);
        weapon.tag = "TeamBlue";
    }
}