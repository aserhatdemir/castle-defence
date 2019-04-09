using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WeaponManager : MonoBehaviour
{
    private float countDown = 2f;

    GameManager gameManager;
    public GameObject missileLauncherPrefab;
    public WeaponUpgradableAttributes tankUpgradableAttributes = new WeaponUpgradableAttributes();
    public WeaponUpgradableAttributes tank1UpgradableAttributes = new WeaponUpgradableAttributes();
    public WeaponUpgradableAttributes tank2UpgradableAttributes = new WeaponUpgradableAttributes();
    public WeaponUpgradableAttributes tank3UpgradableAttributes = new WeaponUpgradableAttributes();

    private float randomizeSpawn = 3f; //for spawnwave to spawn different places

    public GameObject soldierPrefab;

    public GameObject tankPrefab;
    public GameObject tank1Prefab;
    public GameObject tank2Prefab;
    public GameObject tank3Prefab;

    public GameObject factoryRedPanel;
    public Factory[] factoriesRed;

    public GameObject clickedDestination;
//    public Factory factory1;
//    public Factory factory2;
//    public Factory factory3;

//    public GameObject[] tankPrefabList;
    private Transform spawnPoint;

    //-------wave spawn variables
    public float timeBetweenWaves = 20f;
    public Text waveCountdownText;
    private int waveIndex = 0;
    private float waveSeparator = 0.5f; //sepearete instantiations in wave
    private GameObject weapon;

    private GameObject weaponToCreate;

    private int numCreated = 0;

    //--------

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        spawnPoint = this.transform;
        clickedDestination = null;
//        tankPrefabList = new[] {tank1Prefab, tank2Prefab, tank3Prefab};
        tankUpgradableAttributes.refresh();
        tank1UpgradableAttributes.refresh();
        tank2UpgradableAttributes.refresh();
        tank3UpgradableAttributes.refresh();
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
        factoriesRed = factoryRedPanel.GetComponentsInChildren<Factory>();
        if (factoriesRed.Length == 0) yield return null;
        for (int i = 0; i < (waveIndex / 3) + 5; i++)
        {
            SpawnTeamRedWeapon();
            yield return new WaitForSeconds(waveSeparator);
        }
    }

    void SpawnTeamRedWeapon()
    {
        int arraySize = factoriesRed.Length;

        var factory = factoriesRed[numCreated % arraySize];
        factory.AddToQueue(tankPrefab);
        numCreated++;
    }

    public void SetWeaponToCreate(GameObject weapon)
    {
        weaponToCreate = weapon;
    }

    public GameObject GetWeaponToCreate()
    {
        return weaponToCreate;
    }

    public void CreateWeapon(GameObject prefab, Factory factory, String tag)
    {
        Transform factoryTransform;
        var gateTransform = (factoryTransform = factory.transform).Find("Gate");
        var gateTransformPosition = gateTransform.position;
        var vectorToTarget = gateTransformPosition - factoryTransform.position;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        weapon = Instantiate(prefab, new Vector2(gateTransformPosition.x, gateTransformPosition.y), q);
        weapon.tag = tag;
        weapon.layer = LayerMask.NameToLayer(tag + "Layer");
    }
}