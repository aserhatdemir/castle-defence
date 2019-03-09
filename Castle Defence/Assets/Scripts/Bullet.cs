using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    public Vector3 direction;
    public string targetTag;
    private float ttl = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, ttl);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction != null)
            transform.Translate(direction * Time.deltaTime * speed, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            Destroy(gameObject);
            if(collision.gameObject.GetComponent<Soldier>())
            {
                Soldier enemy = collision.gameObject.GetComponent<Soldier>();
                enemy.health -= damage;
                if (enemy.health <= 0)
                {
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                Castle enemy = collision.gameObject.GetComponent<Castle>();
                enemy.health -= damage;
                if (enemy.health <= 0)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }

}