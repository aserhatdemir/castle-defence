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
        waveCountdownText.text = string.Format("{0:00.00}", countDown);
    }

    //make it coroutine to separate instantiations from each other
    IEnumerator SpawnWave()
    {
        waveIndex++;
        randomizeSpawn = Random.Range(-3f, 3f);
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnTeamRedWeapon();
            yield return new WaitForSeconds(waveSeparator);
        }
    }

    void SpawnTeamRedWeapon()
    {
        weapon = Instantiate(tankPrefab, new Vector2(spawnPoint.position.x, spawnPoint.position.y + randomizeSpawn),
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
}