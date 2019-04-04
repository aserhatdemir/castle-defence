using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public Vector3 direction;
    public float speed = 10f;
    public string targetTag;
    public GameObject hitEffectPrefab;

    private float ttl = 3f;
    private GameManager gameManager;

    void Start()
    {
        Destroy(gameObject, ttl);
        gameManager = GameManager.instance;
    }

    void Update()
    {
        if (direction != null)
            transform.Translate(direction * Time.deltaTime * speed, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(targetTag)) return;
        Destroy(this.gameObject);
        GameObject hEffect = (GameObject) Instantiate(hitEffectPrefab, transform.position,
            hitEffectPrefab.transform.rotation);
        Destroy(hEffect, 1f);
        if (collision.gameObject.GetComponent<Weapon>())
        {
            Weapon enemy = collision.gameObject.GetComponent<Weapon>();
            enemy.UpdateHealth(damage * -1f);
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