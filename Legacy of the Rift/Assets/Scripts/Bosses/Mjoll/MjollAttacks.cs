using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MjollAttacks : MonoBehaviour
{
    private GameObject playerHandler;
    private float gasTimer, lightningTimer, spawnTimer;
    public int startGasTimer, startLightningTimer, startSpawnTimer;

    public Transform firePoint;
    public float attackRange;
    public int lightningDamage;

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

    public GameObject impactEffect;

    public LineRenderer lineRenderer1, lineRenderer2, lineRenderer3;
    public GameObject lightningPrefab;
    private int numSegments = 24;
    private float maxZ = 8f;
    private float yPosRange = 1f;

    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        playerHandler = GameObject.FindGameObjectWithTag("PlayerHandler");
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
                Debug.Log(enemiesSpawned);
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
        //&& Vector2.Distance(player.transform.position, transform.position) <= attackRange
        if (lightningTimer <= 0)
        {
            Shoot();
            lightningTimer = startLightningTimer;
        }
        else
        {
            lightningTimer -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, new Vector2(-1,0));

        GameObject lightning1 = Instantiate(lightningPrefab, hitInfo.transform.position, Quaternion.identity);
        lineRenderer1 = lightning1.GetComponent<LineRenderer>();
        //GameObject lightning2 = Instantiate(lightningPrefab, hitInfo.transform.position, Quaternion.identity);
        //lineRenderer2 = lightning2.GetComponent<LineRenderer>();
        //GameObject lightning3 = Instantiate(lightningPrefab, hitInfo.transform.position, Quaternion.identity);
        //lineRenderer3 = lightning3.GetComponent<LineRenderer>();

        //lineRenderer.gameObject.SetActive(true);

        if (hitInfo)
        {
            LokirAttacks lokir = hitInfo.transform.GetComponentInParent<LokirAttacks>();
            HalvarAttacks halvar = hitInfo.transform.GetComponent<HalvarAttacks>();
            UrsaAttacks ursa = hitInfo.transform.GetComponent<UrsaAttacks>();
            if(lokir != null || halvar != null || ursa != null)
            {
                playerHandler.GetComponent<PlayerHealth>().TakeDamage(lightningDamage);
            }

            Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
            
            for (int i = 1; i < numSegments - 1; ++i)
            {
                float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

                lineRenderer1.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, hitInfo.point.x), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
            }
            lineRenderer1.SetPosition(0, firePoint.position);
            lineRenderer1.SetPosition(23, hitInfo.point);

            //for (int i = 1; i < numSegments - 1; ++i)
            //{
            //    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

            //    lineRenderer2.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, hitInfo.point.x), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
            //}
            //lineRenderer2.SetPosition(0, firePoint.position);
            //lineRenderer2.SetPosition(11, hitInfo.point);

            //for (int i = 1; i < numSegments - 1; ++i)
            //{
            //    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

            //    lineRenderer3.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, hitInfo.point.x), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
            //}
            //lineRenderer3.SetPosition(0, firePoint.position);
            //lineRenderer3.SetPosition(11, hitInfo.point);

            if (lokir != null)
            {
                StartCoroutine(lokir.Knockback(1f, 350f, lokir.transform.position));
            }

            if (halvar != null)
            {
                StartCoroutine(halvar.Knockback(1f, 350f, halvar.transform.position));
            }

            if (ursa != null)
            {
                StartCoroutine(ursa.Knockback(1f, 350f, ursa.transform.position));
            }

        } else
        {
            for (int i = 1; i < numSegments - 1; ++i)
            {
                float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

                lineRenderer1.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, -1 * 100), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
            }
            lineRenderer1.SetPosition(0, firePoint.position);
            //lineRenderer1.SetPosition(11, firePoint.position + new Vector3(-1, 0) * 100);

            //for (int i = 1; i < numSegments - 1; ++i)
            //{
            //    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

            //    lineRenderer2.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, -1 * 100), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
            //}
            //lineRenderer2.SetPosition(0, firePoint.position);
            //lineRenderer2.SetPosition(11, firePoint.position + new Vector3(-1, 0) * 100);

            //for (int i = 1; i < numSegments - 1; ++i)
            //{
            //    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

            //    lineRenderer3.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, -1 * 100), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
            //}
            //lineRenderer3.SetPosition(0, firePoint.position);
            //lineRenderer3.SetPosition(11, firePoint.position + new Vector3(-1, 0) * 100);
        }

        //yield return new WaitForSeconds(1f);

        //lineRenderer.gameObject.SetActive(false);
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
