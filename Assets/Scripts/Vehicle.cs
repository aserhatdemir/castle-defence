using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float aimingSpeed = 3f;
    public float attackSpeed = 5f;
    public Bullet bulletPrefab;
    private Castle enemyCastle;
    private string enemyTag;
    private Transform head;
    public float health = 10f;
    private float lastFireTime;
    public float minRange = 2;
    private Transform muzzle;
    private GameObject[] possibleTargets;
    public float range = 5;
    public float speed = 1f;
    public GameObject target;

    private void Start()
    {
        //decide the team
        enemyTag = this.CompareTag("TeamBlue") ? "TeamRed" : "TeamBlue";

        head = transform.GetChild(0);
        muzzle = transform.GetChild(0).GetChild(0);
        lastFireTime = Time.time;

        InvokeRepeating(nameof(FindClosestEnemy), 0.1f, 0.4f);
    }

    private void FixedUpdate()
    {
        //FindClosestEnemy();
        AimTarget();
        ApproachTarget();
        Fire();
    }

    private void FindClosestEnemy()
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

    private void AimTarget()
    {
        if (!IsInRange(target)) return;
        //Vector2 pos = (Vector2)target.transform.position;
        //Vector2 dir = new Vector2(pos.x, pos.y) - (Vector2)transform.position;
        //float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        //Quaternion neededRotation = Quaternion.LookRotation(Vector3.forward, dir - (Vector2)transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, neededRotation, aimingSpeed * Time.deltaTime);

        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private bool IsAimingFinished()
    {
        if (target == null) return false;
        Vector2 pos = (Vector2) target.transform.position;
        Vector2 dir = new Vector2(pos.x, pos.y) - (Vector2) transform.position;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

        return angle < 1.0f;
    }

    private void ApproachTarget()
    {
        if (!target) return;
        if (IsTooClose(target))
        {
            //stop
            //bunun yerine collider da yapilabilir? 
            return;
        }
        else
        {
            //transform.Translate((target.transform.position - transform.position).normalized * Time.deltaTime * speed); 
            transform.position =
                Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    private void Fire()
    {
        if (!target || (!(Time.time - lastFireTime > 1f / attackSpeed))) return;
        lastFireTime = Time.time;
        Bullet bullet1 = Instantiate(bulletPrefab, muzzle.position, transform.rotation);
        //bullet1.direction = soldierMuzzle.right;
        bullet1.direction = (target.transform.position - transform.position).normalized;
        bullet1.targetTag = enemyTag;
    }

    private bool IsInRange(GameObject enemy)
    {
        return enemy != null && range > Distance(enemy);
    }

    private bool IsTooClose(GameObject enemy)
    {
        return enemy != null && minRange > Distance(enemy);
    }

    private float Distance(GameObject obj)
    {
        return (transform.position - obj.transform.position).magnitude;
    }
}