using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    private GameManager gameManager;
    public float health = 100f;
    public Slider healthBar;
    public GameObject destroyEffectPrefab;

    private float startHealth;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = health;
        UpdateHealthBar(health);
        gameManager = GameManager.instance;
    }

    public void UpdateHealth(float damage)
    {
        health += damage;
        UpdateHealthBar(health);
        if (health <= 0)
        {
            Destroy(this.gameObject);
            Vector3 effectPosition = transform.position + new Vector3(0, 0, -3);
            GameObject dEffect = (GameObject) Instantiate(destroyEffectPrefab, effectPosition,
                destroyEffectPrefab.transform.rotation);
            Destroy(dEffect, 1f);

            if (this.GetComponent<Factory>())
            {
                Handheld.Vibrate();
                if (this.CompareTag("TeamBlue"))
                {
                    gameManager.uiManager.DisableShopButton(this.GetComponent<Factory>().shopButton);
                    this.GetComponent<Factory>().shopButton.GetComponent<ShopButton>().factoryDestroyed = true;
                }
                else if (this.CompareTag("TeamRed"))
                {
//                    gameManager.weaponManagerScript.RemoveFactoryFromList(this);
                }
                
            }

            if (this.GetComponent<Castle>())
            {
                if (this.CompareTag("TeamRed")) //if red castle destroyed you won, else you lost.
                    PlayerStats.YouWon = true;
                gameManager.EndGame();
            }
        }
    }

    void UpdateHealthBar(float h)
    {
        if (healthBar != null)
        {
            healthBar.value = h / startHealth;
        }
    }
}