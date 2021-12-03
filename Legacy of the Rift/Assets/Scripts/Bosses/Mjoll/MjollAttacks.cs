using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MjollAttacks : MonoBehaviour
{
    private GameObject playerHandler;

    private float gasTimer, lightningTimer, spawnTimer;
    public int startGasTimer, startLightningTimer, startSpawnTimer;
    public int startLowGasTimer, startLowLightningTimer, startLowSpawnTimer;

    public Transform firePoint;
    public float attackRange;
    public int lightningDamage;

    public GameObject gasPrefab;

    public GameObject smPrefab, spearMarePrefab, botchlingPrefab, gorewingPrefab;
    /// 100 - 97 = 3%
    private int smSpawnRate = 97;
    /// 97 - 95 = 2% 
    private int spearMareSpawnRate = 95;
    /// 95 - 1 = 94%
    private int botchlingSpawnRate = 1;
    /// 1 - 0 = 1% 
    private int gorewingSpawnRate = 0;

    public int enemiesToSpawn;
    public int enemiesToSpawnWhenLowHealth;
    private int enemiesSpawned = 0;
    public List<Transform> spawnPoints = new List<Transform>();

    public GameObject impactEffect;

    public LineRenderer lineRenderer1, lineRenderer2, lineRenderer3;
    public GameObject lightningPrefab;
    private int numSegments = 24;
    private float maxZ = 8f;
    private float yPosRange = 1f;

    public GameObject player;

    private Animator animator;

    private Enemy enemyScript;
    public float lowHealthPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerHandler = GameObject.FindGameObjectWithTag("PlayerHandler");
        player = GameObject.FindGameObjectWithTag("Player");
        enemyScript = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        gasTimer = startGasTimer;
        lightningTimer = startLightningTimer;
        spawnTimer = 1;
        enemiesSpawned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyScript.currentHealth <= 40)
        {
            SceneManager.LoadScene("Credits");
        }

        if (enemyScript.currentHealth > lowHealthPoint)
        {
            if (gasTimer <= 0)
            {
                gasTimer = startGasTimer;
                animator.SetTrigger("Gas");
            }
            else
            {
                gasTimer -= Time.deltaTime;
            }
            if (spawnTimer <= 0)
            {
                spawnTimer = startSpawnTimer;
                animator.SetTrigger("Spawn");
                enemiesSpawned = 0;
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }

            if (lightningTimer <= 0 && Vector2.Distance(player.transform.position, transform.position) <= attackRange)
            {
                animator.SetBool("Shoot", true);
                lightningTimer = startLightningTimer;
            }
            else
            {
                lightningTimer -= Time.deltaTime;
            }
        }
        
        if (enemyScript.currentHealth <= lowHealthPoint)
        {
            if (gasTimer <= 0)
            {
                gasTimer = startLowGasTimer;
                animator.SetTrigger("Gas");
            }
            else
            {
                gasTimer -= Time.deltaTime;
            }
            if (spawnTimer <= 0)
            {
                spawnTimer = startLowSpawnTimer;
                animator.SetTrigger("Spawn");
                enemiesSpawned = 0;
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }

            if (lightningTimer <= 0 && Vector2.Distance(player.transform.position, transform.position) <= attackRange)
            {
                animator.SetBool("Shoot", true);
                lightningTimer = startLowLightningTimer;
            }
            else
            {
                lightningTimer -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("LeftWall"))
        {
            animator.SetBool("avoidLeftWall", true);
        }
        if (collision.gameObject.CompareTag("RightWall"))
        {
            animator.SetBool("avoidRightWall", true);
        }
    }

    void Gas()
    {
        Instantiate(gasPrefab, player.transform.position, player.transform.rotation);
    }

    void LowGas()
    {
        Instantiate(gasPrefab, player.transform.position, player.transform.rotation);
    }

    void Spawn()
    {
        while (enemiesSpawned <= enemiesToSpawn)
        {
            enemiesSpawned += 1;
            Instantiate(SpawnRandomEnemy(), RandomSpawnPoint().position, Quaternion.identity);
        }
    }

    void LowSpawn()
    {
        while (enemiesSpawned <= enemiesToSpawnWhenLowHealth)
        {
            enemiesSpawned += 1;
            Instantiate(SpawnRandomEnemy(), RandomSpawnPoint().position, Quaternion.identity);
        }
        enemiesSpawned = 0;
    }

    void LowShoot()
    {
        if (enemyScript.isFlipped == false)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, new Vector2(-1, 0));

            GameObject lightning1 = Instantiate(lightningPrefab, hitInfo.transform.position, Quaternion.identity);
            lineRenderer1 = lightning1.GetComponent<LineRenderer>();

            if (hitInfo)
            {
                LokirAttacks lokir = hitInfo.transform.GetComponentInParent<LokirAttacks>();
                HalvarAttacks halvar = hitInfo.transform.GetComponent<HalvarAttacks>();
                UrsaAttacks ursa = hitInfo.transform.GetComponent<UrsaAttacks>();
                if (lokir != null || halvar != null || ursa != null)
                {
                    playerHandler.GetComponent<PlayerHealth>().TakeDamage(lightningDamage);
                    var player = hitInfo.transform.GetComponentInParent<CharacterController2D>();
                    player.bigKnock = true;
                    player.knockbackCount = player.knockbackLength;

                    if (hitInfo.transform.position.x < transform.position.x)
                    {
                        player.knockFromRight = true;
                    }
                    else
                    {
                        player.knockFromRight = false;
                    }
                }

                Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

                for (int i = 1; i < numSegments - 1; ++i)
                {
                    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

                    lineRenderer1.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, hitInfo.point.x), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
                }
                lineRenderer1.SetPosition(0, firePoint.position);
                lineRenderer1.SetPosition(23, hitInfo.point);

                if (lokir != null)
                {
                    //StartCoroutine(lokir.Knockback(1f, 2f, lokir.transform.position));
                }

                if (halvar != null)
                {
                    //StartCoroutine(halvar.Knockback(1f, 2f, halvar.transform.position));
                }

                if (ursa != null)
                {
                    //StartCoroutine(ursa.Knockback(1f, 2f, ursa.transform.position));
                }

            }
            else
            {
                for (int i = 1; i < numSegments - 1; ++i)
                {
                    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

                    lineRenderer1.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, -1 * 100), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
                }
                lineRenderer1.SetPosition(0, firePoint.position);
            }
        }
        if (enemyScript.isFlipped == true)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, Vector2.right);

            GameObject lightning1 = Instantiate(lightningPrefab, hitInfo.transform.position, Quaternion.identity);
            lineRenderer1 = lightning1.GetComponent<LineRenderer>();

            if (hitInfo)
            {
                LokirAttacks lokir = hitInfo.transform.GetComponentInParent<LokirAttacks>();
                HalvarAttacks halvar = hitInfo.transform.GetComponent<HalvarAttacks>();
                UrsaAttacks ursa = hitInfo.transform.GetComponent<UrsaAttacks>();
                if (lokir != null || halvar != null || ursa != null)
                {
                    playerHandler.GetComponent<PlayerHealth>().TakeDamage(lightningDamage);
                    var player = hitInfo.transform.GetComponentInParent<CharacterController2D>();
                    player.bigKnock = true;
                    player.knockbackCount = player.knockbackLength;

                    if (hitInfo.transform.position.x < transform.position.x)
                    {
                        player.knockFromRight = true;
                    }
                    else
                    {
                        player.knockFromRight = false;
                    }
                }

                Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

                for (int i = 1; i < numSegments - 1; ++i)
                {
                    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

                    lineRenderer1.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, hitInfo.point.x), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
                }
                lineRenderer1.SetPosition(0, firePoint.position);
                lineRenderer1.SetPosition(23, hitInfo.point);

                if (lokir != null)
                {
                    //StartCoroutine(lokir.Knockback(1f, 2f, new Vector3(-lokir.transform.position.x, lokir.transform.position.y, lokir.transform.position.z)));
                }

                if (halvar != null)
                {
                    //StartCoroutine(halvar.Knockback(1f, 2f, new Vector3(-halvar.transform.position.x, halvar.transform.position.y, halvar.transform.position.z)));
                }

                if (ursa != null)
                {
                    //StartCoroutine(ursa.Knockback(1f, 2f, new Vector3(-ursa.transform.position.x, ursa.transform.position.y, ursa.transform.position.z)));
                }

            }
            else
            {
                for (int i = 1; i < numSegments - 1; ++i)
                {
                    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

                    lineRenderer1.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, -1 * 100), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
                }
                lineRenderer1.SetPosition(0, firePoint.position);
            }
        }
    }

    void Shoot()
    {
        if(enemyScript.isFlipped == false)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, new Vector2(-1, 0));

            GameObject lightning1 = Instantiate(lightningPrefab, hitInfo.transform.position, Quaternion.identity);
            lineRenderer1 = lightning1.GetComponent<LineRenderer>();

            if (hitInfo)
            {
                LokirAttacks lokir = hitInfo.transform.GetComponentInParent<LokirAttacks>();
                HalvarAttacks halvar = hitInfo.transform.GetComponent<HalvarAttacks>();
                UrsaAttacks ursa = hitInfo.transform.GetComponent<UrsaAttacks>();
                if (lokir != null || halvar != null || ursa != null)
                {
                    playerHandler.GetComponent<PlayerHealth>().TakeDamage(lightningDamage);
                    var player = hitInfo.transform.GetComponentInParent<CharacterController2D>();
                    player.bigKnock = false;
                    player.knockbackCount = player.knockbackLength;

                    if(hitInfo.transform.position.x < transform.position.x)
                    {
                        player.knockFromRight = true;
                    } else
                    {
                        player.knockFromRight = false;
                    }
                }

                Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

                for (int i = 1; i < numSegments - 1; ++i)
                {
                    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

                    lineRenderer1.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, hitInfo.point.x), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
                }
                lineRenderer1.SetPosition(0, firePoint.position);
                lineRenderer1.SetPosition(23, hitInfo.point);

                if (lokir != null)
                {
                    //StartCoroutine(lokir.Knockback(1f, 1f, lokir.transform.position));
                }

                if (halvar != null)
                {
                    //StartCoroutine(halvar.Knockback(1f, 1f, halvar.transform.position));
                }

                if (ursa != null)
                {
                    //StartCoroutine(ursa.Knockback(1f, 1f, ursa.transform.position));
                }

            }
            else
            {
                for (int i = 1; i < numSegments - 1; ++i)
                {
                    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

                    lineRenderer1.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, -1 * 100), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
                }
                lineRenderer1.SetPosition(0, firePoint.position);
            }
        }

        if (enemyScript.isFlipped == true)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, Vector2.right);

            GameObject lightning1 = Instantiate(lightningPrefab, hitInfo.transform.position, Quaternion.identity);
            lineRenderer1 = lightning1.GetComponent<LineRenderer>();

            if (hitInfo)
            {
                LokirAttacks lokir = hitInfo.transform.GetComponentInParent<LokirAttacks>();
                HalvarAttacks halvar = hitInfo.transform.GetComponent<HalvarAttacks>();
                UrsaAttacks ursa = hitInfo.transform.GetComponent<UrsaAttacks>();
                if (lokir != null || halvar != null || ursa != null)
                {
                    playerHandler.GetComponent<PlayerHealth>().TakeDamage(lightningDamage);
                    var player = hitInfo.transform.GetComponentInParent<CharacterController2D>();
                    player.bigKnock = false;
                    player.knockbackCount = player.knockbackLength;

                    if (hitInfo.transform.position.x < transform.position.x)
                    {
                        player.knockFromRight = true;
                    }
                    else
                    {
                        player.knockFromRight = false;
                    }
                }

                Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

                for (int i = 1; i < numSegments - 1; ++i)
                {
                    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

                    lineRenderer1.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, hitInfo.point.x), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
                }
                lineRenderer1.SetPosition(0, firePoint.position);
                lineRenderer1.SetPosition(23, hitInfo.point);

                if (lokir != null)
                {
                    //StartCoroutine(lokir.Knockback(1f, 1f, new Vector3(-lokir.transform.position.x, lokir.transform.position.y, lokir.transform.position.z)));
                }

                if (halvar != null)
                {
                    //StartCoroutine(halvar.Knockback(1f, 1f, new Vector3(-halvar.transform.position.x, halvar.transform.position.y, halvar.transform.position.z)));
                }

                if (ursa != null)
                {
                    //StartCoroutine(ursa.Knockback(1f, 1f, new Vector3(-ursa.transform.position.x, ursa.transform.position.y, ursa.transform.position.z)));
                }

            }
            else
            {
                for (int i = 1; i < numSegments - 1; ++i)
                {
                    float z = ((float)i) * (maxZ) / (float)(numSegments - 1);

                    lineRenderer1.SetPosition(i, new Vector3(Random.Range(firePoint.position.x, -1 * 100), Random.Range(-(firePoint.position.y - yPosRange), (firePoint.position.y + yPosRange)), z));
                }
                lineRenderer1.SetPosition(0, firePoint.position);
            }
        }
    }

    void DoneShoot()
    {
        animator.SetBool("Shoot", false);
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
