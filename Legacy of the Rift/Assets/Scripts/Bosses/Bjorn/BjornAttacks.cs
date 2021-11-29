using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BjornAttacks : MonoBehaviour
{
    // public int health;

    public Transform[] tpPoints;

    // 
    float maxRadius = 3;
    float minRadius = 1;

    public Rigidbody2D rb;

    // Basic Attack
    public int basicAttackDamage = 10;
    public Transform attackPoint;
    public GameObject basicAttackPrefab;

    // Homing Attack
    public int homingAttackDamage = 20;
    public GameObject homingAttackPrefab;

    // Asteroid Attack
    public Transform[] asteroidSpawnPoints;
    public GameObject asteroidAttackPrefab;

    private float angle;
    public Transform player;
    private Enemy enemyScript;

    // Bjorn State
    public enum BjornState { DISAPPEARING, APPEARING };
    public BjornState bjorn = BjornState.APPEARING;

    bool disappearing = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();

        enemyScript = GetComponent<Enemy>();

        StartCoroutine(Teleport());
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyScript.currentHealth <= 40)
        {
            SceneManager.LoadScene("Credits");
        }
    }

    public void basicAttack()
    {
        float angleIncrease = 15;

        for (int i = 0; i < 5; i++)
        {

            float tempRot = 5 + angleIncrease * i;

            Instantiate(basicAttackPrefab, attackPoint.position, attackPoint.rotation * Quaternion.Euler(0, 0, tempRot));
        }
    }

    public void homingAttack()
    {

        Instantiate(homingAttackPrefab, attackPoint.position, attackPoint.rotation);
    }

    public void asteroidAttack()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(asteroidAttackPrefab, asteroidSpawnPoints[i].position, asteroidSpawnPoints[i].rotation);
        }
    }


    IEnumerator Teleport()
    {
        // Vector3 teleNearPlayer = Random.insideUnitCircle * (maxRadius - minRadius);
        float randX = Random.Range(-8, 8);
        float randY = Random.Range(4, 5);

        while (true)
        {
            // Teleport rate
            yield return new WaitForSeconds(8f);

            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            bjorn = BjornState.DISAPPEARING;

            // Disappear for x seconds then appear near the player
            yield return new WaitForSeconds(2f);

            GetComponent<Renderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            bjorn = BjornState.APPEARING;

            // transform.position = player.transform.position + teleNearPlayer.normalized * minRadius + teleNearPlayer;
            transform.position = new Vector2(player.transform.position.x + randX, player.transform.position.y + randY);
        }
        /// Transform _sp = tpPoints[Random.Range(0, tpPoints.Length)];
        // Vector2 target = new Vector2(_sp.position.x, _sp.position.y);
        // Vector2 newPos = Vector2.MoveTowards(_sp.position, target, Time.deltaTime);
        // StartCoroutine(Intangible());
        // rb.MovePosition(newPos);
    }
}
