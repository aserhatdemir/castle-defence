using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    private GameManager gameManager;
    public float health = 100f;
    public Slider healthBar;

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