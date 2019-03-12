using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public float speed = 1f;
    public float aimingSpeed = 3f;
    public float health = 10f;
    public float range = 5;
    public float minRange = 2;
    public float attackSpeed = 5f;
    public Bullet bulletPrefab;
    private GameObject[] possibleTargets;
    public GameObject target;
    private Castle enemyCastle;
    private string enemyTag;
    //private Transform soldierHead;
    private Transform soldierMuzzle;
    private float lastFireTime;

    // Start is called before the first frame update
    void Start()
    {
        //decide the team
        if(this.tag == "TeamBlue")
        {
            enemyTag = "TeamRed";
        }
        else
        {
            enemyTag = "TeamBlue";
        }

        soldierMuzzle = transform.GetChild(0);
        //soldierMuzzle = transform.GetChild(0).GetChild(0);
        lastFireTime = Time.time;

        InvokeRepeating("FindClosestEnemy", 0.1f, 0.4f);

    }

    private void FixedUpdate()
    {
        //FindClosestEnemy();
        AimTarget();
        ApproachTarget();
        Fire();
    }

    void FindClosestEnemy()
    {
        possibleTargets = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject enemy in possibleTargets)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = enemy;
                distance = curDistance;
            }
        }
        target = closest;
    }

    void AimTarget()
    {
        if (IsInRange(target))
        {
            //Vector2 pos = (Vector2)target.transform.position;
            //Vector2 dir = new Vector2(pos.x, pos.y) - (Vector2)transform.position;
            //float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            //Quaternion neededRotation = Quaternion.LookRotation(Vector3.forward, dir - (Vector2)transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, neededRotation, aimingSpeed * Time.deltaTime);

            Vector3 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        }
    }

    bool IsAimingFinished()
    {
        if (target != null)
        {
            Vector2 pos = (Vector2)target.transform.position;
            Vector2 dir = new Vector2(pos.x, pos.y) - (Vector2)transform.position;
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

            if (angle < 1.0f)
            {
                return true;
            }
        }
        return false;
    }

    void ApproachTarget()
    {
        if (target)
        {
            if (IsTooClose(target))
            {
                //stop
                //bunun yerine collider da yapilabilir? 
                return;
            }
            else
            {
                //transform.Translate((target.transform.position - transform.position).normalized * Time.deltaTime * speed); 
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
        }
    }

    void Fire()
    {
        if (target && (Time.time - lastFireTime > 1f / attackSpeed))
        {
            lastFireTime = Time.time;
            Bullet bullet1 = Instantiate(bulletPrefab, soldierMuzzle.position, transform.rotation);
            //bullet1.direction = soldierMuzzle.right;
            bullet1.direction = (target.transform.position - transform.position).normalized;
            bullet1.targetTag = enemyTag;
        }
    }

    bool IsInRange(GameObject enemy)
    {

        return enemy != null && range > Distance(enemy);
    }

    bool IsTooClose(GameObject enemy)
    {

        return enemy != null && minRange > Distance(enemy);
    }

    float Distance(GameObject obj)
    {
        return (transform.position - obj.transform.position).magnitude;
    }

}



