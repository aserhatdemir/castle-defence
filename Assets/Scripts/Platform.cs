using UnityEngine;

public class Platform : MonoBehaviour
{
    GameManager gameManager;
    private GameObject weapon;
    private int weaponPrice;
    private GameObject weaponToCreate;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        if (!this.CompareTag("TeamBlue")) return;
        weaponToCreate = gameManager.weaponManagerScript.GetWeaponToCreate();
        if (weaponToCreate == null)
            return;
        weaponPrice = weaponToCreate.GetComponent<Weapon>().price;
        if (PlayerStats.Money < weaponPrice)
        {
            Debug.Log("Not enough money!");
            return;
        }

        SpawnTeamBlueWeapon();
    }

    void SpawnTeamBlueWeapon()
    {
        if (Camera.main == null) return;
        PlayerStats.Money -= weaponPrice;
        gameManager.playerStatsScript.UpdateMoneyTextUI();
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        weapon = Instantiate(weaponToCreate, new Vector2(mousePositionInWorld.x, mousePositionInWorld.y),
            Quaternion.identity);
        weapon.tag = "TeamBlue";
    }
}