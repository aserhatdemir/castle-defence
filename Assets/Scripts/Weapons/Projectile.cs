using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public Vector3 direction;
    public float speed = 10f;
    public string targetTag;
    public GameObject target;

    public float ttl = 3f;
    private GameManager gameManager;
    private bool hasCollided;

    public GameObject destroyEffect;

    void Start()
    {
        Destroy(gameObject, ttl);
        gameManager = GameManager.instance;
        hasCollided = false;
    }

    void Update()
    {
        if (direction != null)
            transform.Translate(direction * Time.deltaTime * speed, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(targetTag)) return;
        //bullet effects only one weapon
        if (hasCollided) return;
        hasCollided = true;
        Destroy(this.gameObject);
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

    private void OnDestroy()
    {
        if (destroyEffect && !hasCollided)
        {
            var p = Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(p,1.0f);
        }
    }
}