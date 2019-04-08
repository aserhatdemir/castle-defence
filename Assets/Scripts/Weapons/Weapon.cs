using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public abstract class Weapon : MonoBehaviour
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
    public float minRange = 2;
    [ReadOnly] public float currentHealth;
    [ReadOnly] public float currentSpeed;
    [ReadOnly] public float currentDamage;
    public WeaponUpgradableAttributes weaponUpgradableAttributes;

    public float Speed
    {
        get => weaponUpgradableAttributes.speed.currentValue;
        set => weaponUpgradableAttributes.speed.currentValue = value;
    }

    public float Health
    {
        get => weaponUpgradableAttributes.health.currentValue;
        set => weaponUpgradableAttributes.health.currentValue = value;
    }

    public float Range
    {
        get => weaponUpgradableAttributes.range.currentValue;
        set => weaponUpgradableAttributes.range.currentValue = value;
    }

    public float AttackSpeed
    {
        get => weaponUpgradableAttributes.attackSpeed.currentValue;
        set => weaponUpgradableAttributes.attackSpeed.currentValue = value;
    }

    public float AimingSpeed
    {
        get => weaponUpgradableAttributes.aimingSpeed.currentValue;
        set => weaponUpgradableAttributes.aimingSpeed.currentValue = value;
    }

    public float Damage
    {
        get => weaponUpgradableAttributes.damage.currentValue;
        set => weaponUpgradableAttributes.damage.currentValue = value;
    }

    public float minAttackAngle = 2f;
    public float aimingStopAngle = 1f;

    public float productionTime;

    public AimingState aimingState = AimingState.NOT_AIMING;
    public MovementState movementState = MovementState.STOPPED;
    public AttackingState attackState = AttackingState.NOT_ATTACKING;

    public Projectile bulletPrefab;


    private Castle enemyCastle;
    public string enemyTag;

    private Transform gun;
    private Transform head;

    public float lastTargetAngle;
    public float lastTargetDistance;

    public float lastDestinationAngle;
    public float lastDestinationDistance;

    private float lastFireTime;
    private Transform muzzle;
    private Transform muzzle1;
    private Transform muzzle2;
    private GameObject[] possibleTargets;

    public GameObject target;

    public float slowUpdateTime;
    public float updateSpeed = 1f;

    private GameManager gameManager;
    public GameObject destroyEffectPrefab;
    public GameObject destination;
    public Vector3 destinationPoint;
    
    public void Start()
    {
        gameManager = GameManager.instance;
        destination = gameManager.weaponManagerScript.clickedDestination;

        weaponUpgradableAttributes = GetUpgradableAttributes();
        currentSpeed = weaponUpgradableAttributes.speed.currentValue;
        currentHealth = weaponUpgradableAttributes.health.currentValue;
        currentDamage = weaponUpgradableAttributes.damage.currentValue;
        //decide the team
        enemyTag = CompareTag("TeamBlue") ? "TeamRed" : "TeamBlue";
        Prepare();

//        InvokeRepeating(nameof(SlowUpdate), 0.1f, 0.4f);
    }

    private void Prepare()
    {
        gun = transform.Find("Base").Find("Gun");

        head = gun.Find("Head");
        muzzle = head.Find("Muzzle");
        muzzle1 = head.Find("Muzzle-1");
        muzzle2 = head.Find("Muzzle-2");
        slowUpdateTime = lastFireTime = Time.time;
    }

    public abstract WeaponUpgradableAttributes GetUpgradableAttributes();


    private void FixedUpdate()
    {
        if (GameManager.GameIsOver) return;
        if (slowUpdateTime + updateSpeed > Time.time)
        {
            slowUpdateTime = Time.time;
            SlowUpdate();
        }

        AimTarget();
        RotateToDestination();
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
//            movementState = MovementState.STOPPED;
            aimingState = AimingState.NOT_AIMING;
            return;
        }

        lastTargetAngle = Mathf.Abs(FindAngle(target));
        lastTargetDistance = Distance(closestTarget);

        destination = gameManager.weaponManagerScript.clickedDestination;
        
        if(!destination || this.CompareTag("TeamRed"))
        {
            lastDestinationAngle = Mathf.Abs(FindAngle(target));
            lastDestinationDistance = Distance(target);
        }
        else if (this.CompareTag("TeamBlue"))
        {
            lastDestinationAngle = Mathf.Abs(FindAngle(destination));
            lastDestinationDistance = Distance(destination);
        }
        
        DecideAimingState();
        DecideMovementState();
    }

    private void DecideMovementState()
    {
        if (this.CompareTag("TeamBlue"))
        {
            if(destination)
                Debug.Log("destination = " + destination.transform.position.ToString());
            Debug.Log("last destination distanve = " + lastDestinationDistance.ToString());
            Debug.Log("range = " + Range.ToString());
        }
        
        
        var prevMovementState = movementState;
        if (lastDestinationDistance > Range)
        {
            movementState = MovementState.MOVING;
            currentSpeed = Speed;
//            aimingState = AimingState.AIMING;
        }

        if (.5f < lastDestinationDistance && Range > lastDestinationDistance)
        {
            var f1 = Mathf.Abs(lastDestinationDistance - Range) / Range;
            var f = currentSpeed = Speed * (1f - f1);
            movementState = MovementState.MOVING;
//            aimingState = AimingState.AIMING;
        }

//        if (.5f + Random.Range(0, 0.6f) > lastDestinationDistance)
        if (Random.Range(0, 0.5f) > lastDestinationDistance)
        {
            movementState = MovementState.STOPPED;
//            aimingState = AimingState.AIMING;
        }


//        if (lastDestinationAngle > aimingStopAngle) aimingState = AimingState.AIMING;

//        if (minAttackAngle > lastDestinationAngle && Range > lastDestinationDistance) attackState = AttackingState.ATTACKING;
    }

    private void DecideAimingState()
    {
        if (lastTargetDistance > Range)
        {
//            movementState = MovementState.MOVING;
//            currentSpeed = Speed;
            aimingState = AimingState.AIMING;
        }

        if (Range * .5f < lastTargetDistance && Range > lastTargetDistance)
        {
//            var f1 = Mathf.Abs(lastDistance - Range) / Range;
//            var f = currentSpeed = Speed * (1f - f1);
//            movementState = MovementState.MOVING;
            aimingState = AimingState.AIMING;
        }

        if (Range * .5f > lastTargetDistance)
        {
//            movementState = MovementState.STOPPED;
            aimingState = AimingState.AIMING;
        }


        if (lastTargetAngle > aimingStopAngle) aimingState = AimingState.AIMING;

        if (minAttackAngle > lastTargetAngle && Range > lastTargetDistance) attackState = AttackingState.ATTACKING;
    }

    private void AimTarget()
    {
        if (aimingState != AimingState.AIMING) return;
        if (!target) return;
        var vectorToTarget = target.transform.position - transform.position;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        gun.rotation = Quaternion.Slerp(gun.rotation, q, Time.deltaTime * AimingSpeed);
        if (movementState == MovementState.STOPPED)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * currentSpeed);
        }
    }

    private void RotateToDestination()
    {
//        if (!destination) return;
        if (!destination || this.CompareTag("TeamRed"))
        {
            destination = target;
        }
            
        var vectorToTarget = destination.transform.position - transform.position;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * currentSpeed);
    }

    private void ApproachTarget()
    {
        if (movementState != MovementState.MOVING) return;
        if (!destination) return;
        transform.Translate((Vector2) transform.up * currentSpeed * Time.deltaTime, Space.World);
    }

    private void Fire()
    {
        if (attackState != AttackingState.ATTACKING) return;
        if (!target || !(Time.time - lastFireTime > 1f / AttackSpeed)) return;
        lastFireTime = Time.time;

        if (muzzle1 && muzzle2)
        {
            FireBullet(muzzle1);
            FireBullet(muzzle2);
        }
        else
        {
            FireBullet(muzzle);
        }
    }

    private void FireBullet(Transform muz)
    {
        var muzzlePosition = muz.position;
        var bullet = Instantiate(bulletPrefab, muzzlePosition, head.rotation);
        var bulletDirection = (Vector2) (muzzle.position - head.position).normalized;
        bullet.direction = bulletDirection;
        bullet.damage = this.currentDamage;
        bullet.target = target;
        bullet.gameObject.layer = CompareTag("TeamBlue")
            ? LayerMask.NameToLayer("TeamBlueLayer")
            : LayerMask.NameToLayer("TeamRedLayer");
        bullet.targetTag = CompareTag("TeamBlue") ? "TeamRed" : "TeamBlue";
        bullet.ttl = Range / bullet.speed;
    }

    private bool IsInRange(GameObject enemy)
    {
        return enemy && Range > Distance(enemy);
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

    public void UpdateHealth(float damage)
    {
        currentHealth += damage;
        if (currentHealth <= 0)
        {
            gameManager.playerStatsScript.UpdateMoney(price / 2); //give some money back
            Destroy(this.gameObject);
            GameObject dEffect = (GameObject) Instantiate(destroyEffectPrefab, transform.position,
                destroyEffectPrefab.transform.rotation);
            Destroy(dEffect, 1f);
        }
    }
}