using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{

    public GameObject soldierPrefab;
    public GameObject tankPrefab;
    public GameObject MissileLauncherPrefab;
    private GameObject weapon;
    private GameObject weaponToCreate;

    GameManager gameManager;

    //-------wave spawn variables
    public float timeBetweenWaves = 5f;
    private float countDown = 2f;
    private int waveIndex = 0;
    private float waveSeparator = 0.5f; //sepearete instantiations in wave
    private Transform spawnPoint;
    private float randomizeSpawn = 3f; //for spawnwave to spawn different places
    public Text waveCountdownText;
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
        waveCountdownText.text = ((int)(countDown + 1f)).ToString();
    }

    //make it coroutine to seperate instantiations from each other
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
        weapon = Instantiate(tankPrefab, new Vector2(spawnPoint.position.x, spawnPoint.position.y + randomizeSpawn), Quaternion.identity);
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
