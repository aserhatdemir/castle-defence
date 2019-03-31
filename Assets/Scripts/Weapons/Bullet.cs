using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public GameObject destroyEffectPrefab;
    public Vector3 direction;

    GameManager gameManager;
    public float speed = 10f;
    public string targetTag;

    private float ttl = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, ttl);
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction != null)
            transform.Translate(direction * Time.deltaTime * speed, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(targetTag)) return;
        Destroy(gameObject);
        if (collision.gameObject.GetComponent<Weapon>())
        {
            Weapon enemy = collision.gameObject.GetComponent<Weapon>();
            enemy.health -= damage;
            Vector3 enemyPosition = enemy.transform.position;
            if (enemy.health <= 0 && gameManager)
            {
                PlayerStats.Money += enemy.price / 2; //give money back
                gameManager.playerStatsScript.UpdateMoneyTextUI();
                Destroy(collision.gameObject);
                GameObject dEffect = (GameObject) Instantiate(destroyEffectPrefab, enemyPosition,
                    destroyEffectPrefab.transform.rotation);
                Destroy(dEffect, 1f);
            }
        }
        else if (collision.gameObject.GetComponent<Building>())
        {
            Building enemy = collision.gameObject.GetComponent<Building>();
            enemy.UpdateHealth(damage * -1f);
        }
        else
        {
            Debug.LogError("UNKNOWN ENEMY");
        }
    }
}