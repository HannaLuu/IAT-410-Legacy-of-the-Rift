using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MjollAttacks : MonoBehaviour
{
    private float gasTimer, lightningTimer, spawnTimer;
    public int startGasTimer, startLightningTimer, startSpawnTimer;

    public float attackRange;

    public GameObject gasPrefab;

    public GameObject smPrefab, spearMarePrefab, botchlingPrefab, gorewingPrefab;
    /// 100 - 70 = 30%
    private int smSpawnRate = 70;
    /// 70 - 30 = 40%
    private int spearMareSpawnRate = 30;
    /// 30 - 10 = 20%
    private int botchlingSpawnRate = 10;
    /// 10 - 0 = 10%
    private int gorewingSpawnRate = 0;
    public int enemiesToSpawn;
    private int enemiesSpawned = 0;
    public List<Transform> spawnPoints = new List<Transform>();

    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gasTimer = startGasTimer;
        lightningTimer = startLightningTimer;
        spawnTimer = startSpawnTimer;
        enemiesSpawned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gasTimer <= 0)
        {
            gasTimer = startGasTimer;
            Instantiate(gasPrefab, player.transform.position, player.transform.rotation);
        } else
        {
            gasTimer -= Time.deltaTime;
        }
        if (spawnTimer <= 0)
        {
            while (enemiesSpawned <= enemiesToSpawn)
            {
                enemiesSpawned += 1;
                Instantiate(SpawnRandomEnemy(), RandomSpawnPoint().position, Quaternion.identity);
            }
            spawnTimer = startSpawnTimer;
            enemiesSpawned = 0;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
        //if (lightningTimer <= 0 && Vector2.Distance(player.transform.position, transform.position) <= attackRange)
        //{
        //    lightningTimer = startLightningTimer;
        //}
        //else
        //{
        //    lightningTimer -= Time.deltaTime;
        //}
    }

    public Transform RandomSpawnPoint()
    {
        int random = Random.Range(0, spawnPoints.Count);
        return spawnPoints[random];
    }

    public GameObject SpawnRandomEnemy()
    {
        int random = Random.Range(0, 101);
        if (random > smSpawnRate) return smPrefab;
        else if (random > spearMareSpawnRate) return spearMarePrefab;
        else if (random > botchlingSpawnRate) return botchlingPrefab;
        else if (random > gorewingSpawnRate) return gorewingPrefab;
        else return null;
    }
}
