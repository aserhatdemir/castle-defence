using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{

    public GameObject soldierPrefab;
    private GameObject soldier;

    public float timeBetweenWaves = 5f;
    private float countDown = 2f;
    private int WaveIndex = 0;
    private float waveSeparator = 0.5f; //sepearete instantiations in wave
    private Transform spawnPoint;
    private float randomizeSpawn = 3f; //for spawnwave to spawn different places



    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag == "TeamRed")
        {
            if (countDown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countDown = timeBetweenWaves;
            }
            countDown -= Time.deltaTime;
        }
    }

    private void OnMouseDown()
    {
        if(this.tag == "TeamBlue")
        {
            SpawnTeamBlueSoldier();
        }
    }

    //make it coroutine to seperate instantiations from each other
    IEnumerator SpawnWave()
    {
        WaveIndex++;
        randomizeSpawn = Random.Range(-3f, 3f);
        for (int i = 0; i < WaveIndex; i++)
        {
            SpawnTeamRedSoldier();
            yield return new WaitForSeconds(waveSeparator);
        }
    }

    void SpawnTeamRedSoldier()
    {
        soldier = Instantiate(soldierPrefab, new Vector2(spawnPoint.position.x, spawnPoint.position.y + randomizeSpawn), Quaternion.identity);
        soldier.tag = "TeamRed";
    }


    void SpawnTeamBlueSoldier()
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        soldier = Instantiate(soldierPrefab, new Vector2(mousePositionInWorld.x, mousePositionInWorld.y), Quaternion.identity);
        soldier.tag = "TeamBlue";
    }
}
