using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    private float countDown = 2f;

    GameManager gameManager;
    public GameObject missileLauncherPrefab;
    private float randomizeSpawn = 3f; //for spawnwave to spawn different places

    public GameObject soldierPrefab;
    private Transform spawnPoint;
    public GameObject tankPrefab;
    public GameObject tank1Prefab;
    public GameObject tank2Prefab;
    public GameObject tank3Prefab;

    //-------wave spawn variables
    public float timeBetweenWaves = 5f;
    public Text waveCountdownText;
    private int waveIndex = 0;
    private float waveSeparator = 0.5f; //sepearete instantiations in wave
    private GameObject weapon;

    private GameObject weaponToCreate;

    //--------

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        spawnPoint = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        waveCountdownText.text = "Next Wave: " + string.Format("{0:00.0}", countDown);
    }

    //make it coroutine to separate instantiations from each other
    IEnumerator SpawnWave()
    {
        waveIndex++;
        PlayerStats.Rounds++;
        randomizeSpawn = Random.Range(-3f, 3f);
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnTeamRedWeapon();
            yield return new WaitForSeconds(waveSeparator);
        }
    }

    void SpawnTeamRedWeapon()
    {
        weapon = Instantiate(tankPrefab,
            (Vector2) Random.insideUnitCircle * 2 + new Vector2(spawnPoint.position.x, spawnPoint.position.y),
            Quaternion.identity);
        weapon.tag = "TeamRed";
    }

    public void SetWeaponToCreate(GameObject weapon)
    {
        weaponToCreate = weapon;
    }

    public GameObject GetWeaponToCreate()
    {
        return weaponToCreate;
    }

    public void CreateWeapon(GameObject prefab, Factory factory)
    {
        var transform1 = factory.transform.Find("Gate");
        var position = transform1.position;

        weapon = Instantiate(prefab, new Vector2(position.x, position.y),
            transform1.rotation);
        weapon.tag = "TeamBlue";
    }

    public void UpgradeWeapon(GameObject prefab)
    {
        var weaponPrefab = prefab.GetComponent<Weapon>();
        weaponPrefab.health *= 1.1f;
        weaponPrefab.range *= 1.1f;
        weaponPrefab.speed *= 1.1f;
        weaponPrefab.aimingSpeed *= 1.1f;
        weaponPrefab.attackSpeed *= 1.1f;
    }
}