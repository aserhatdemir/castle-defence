using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum AimingState
    {
        NOT_AIMING,
        AIMING
    }

    public enum AttackingState
    {
        NOT_ATTACKING,
        ATTACKING
    }

    public enum MovementState
    {
        MOVING,
        STOPPED
    }

    public int price = 20;
    public float health = 10f;
    public float range = 5;
    public float minRange = 2;
    public float speed = 1f;
    public float maxSpeed = 1f;
    public float attackSpeed = 5f;
    public float minAttackAngle = 2f;
    public float aimingStopAngle = 1f;
    public float aimingSpeed = 3f;

    public float productionTime;

    public AimingState aimingState = AimingState.NOT_AIMING;
    public MovementState movementState = MovementState.STOPPED;
    public AttackingState attackState = AttackingState.NOT_ATTACKING;

    public Bullet bulletPrefab;

    private Castle enemyCastle;
    private string enemyTag;

    private Transform gun;
    private Transform head;
    public float lastAngle;

    public float lastDistance;
    private float lastFireTime;
    private Transform muzzle;
    private Transform muzzle1;
    private Transform muzzle2;
    private GameObject[] possibleTargets;

    public GameObject target;

    public float slowUpdateTime;
    public float updateSpeed = 1f;

    private void Start()
    {
        //decide the team
        enemyTag = CompareTag("TeamBlue") ? "TeamRed" : "TeamBlue";
        gun = transform.Find("Base").Find("Gun");

        head = gun.Find("Head");
        muzzle = head.Find("Muzzle");
        muzzle1 = head.Find("Muzzle-1");
        muzzle2 = head.Find("Muzzle-2");
        slowUpdateTime = lastFireTime = Time.time;

//        InvokeRepeating(nameof(SlowUpdate), 0.1f, 0.4f);
    }

    private void FixedUpdate()
    {
        if (GameManager.GameIsOver) return;
        if (slowUpdateTime + updateSpeed > Time.time)
        {
            slowUpdateTime = Time.time;
            SlowUpdate();
        }

        AimTarget();
        ApproachTarget();
        Fire();
    }


    private GameObject FindClosestTarget()
    {
        possibleTargets = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject closest = null;
        var distance = Mathf.Infinity;
        var position = transform.position;
        foreach (var enemy in possibleTargets)
        {
            var diff = enemy.transform.position - position;
            var curDistance = diff.sqrMagnitude;
            if (!(curDistance < distance)) continue;
            closest = enemy;
            distance = curDistance;
        }

        return closest;
    }

    private void SlowUpdate()
    {
        var closestTarget = FindClosestTarget();
        target = closestTarget;
        if (!target)
        {
            movementState = MovementState.STOPPED;
            aimingState = AimingState.NOT_AIMING;
            return;
        }

        lastDistance = Distance(closestTarget);

        if (lastDistance > range)
        {
            movementState = MovementState.MOVING;
            speed = maxSpeed;
            aimingState = AimingState.AIMING;
        }

        if (range * .5f < lastDistance && range > lastDistance)
        {
            var f1 = Mathf.Abs(lastDistance - range) / range;
            var f = speed = maxSpeed * (1f - f1);
            movementState = MovementState.MOVING;
            aimingState = AimingState.AIMING;
        }

        if (range * .5f > lastDistance)
        {
            movementState = MovementState.STOPPED;
            aimingState = AimingState.AIMING;
        }

        lastAngle = Mathf.Abs(FindAngle(target));

        if (lastAngle > aimingStopAngle) aimingState = AimingState.AIMING;

        if (minAttackAngle > lastAngle && range > lastDistance) attackState = AttackingState.ATTACKING;
    }

    private void AimTarget()
    {
        if (aimingState != AimingState.AIMING) return;
        if (!target) return;
        var vectorToTarget = target.transform.position - transform.position;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        gun.rotation = Quaternion.Slerp(gun.rotation, q, Time.deltaTime * aimingSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }


    private void ApproachTarget()
    {
        if (movementState != MovementState.MOVING) return;
        if (!target) return;
        transform.Translate((Vector2) transform.up * speed * Time.deltaTime, Space.World);
    }

    private void Fire()
    {
        if (attackState != AttackingState.ATTACKING) return;
        if (!target || !(Time.time - lastFireTime > 1f / attackSpeed)) return;
        lastFireTime = Time.time;
        var muzzlePosition = muzzle.position;
        var bulletDirection = (Vector2) (muzzlePosition - head.position).normalized;
        var bullet1 = Instantiate(bulletPrefab, muzzlePosition,
            muzzle.rotation);
//        var bullet1 = Instantiate(bulletPrefab, muzzle.position, transform.rotation);
        bullet1.direction = bulletDirection;
        bullet1.targetTag = enemyTag;
    }

    private bool IsInRange(GameObject enemy)
    {
        return enemy && range > Distance(enemy);
    }

    private bool IsTooClose(GameObject enemy)
    {
        return enemy && minRange > Distance(enemy);
    }

    private float Distance(GameObject obj)
    {
        return (transform.position - obj.transform.position).magnitude;
    }

    private float FindAngle(GameObject go)
    {
        if (go is null)
            return Mathf.Infinity;
        var position = head.position;
        var angle = Vector2.Angle(muzzle.position - position, go.transform.position - position);
        return angle;
    }
}