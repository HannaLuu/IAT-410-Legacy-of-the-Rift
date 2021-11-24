using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BjornAttacks : MonoBehaviour
{
    // public int health;

    public Transform[] tpPoints;
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

    public void basicAttack()
    {
        float angleIncrease = 15;

        for (int i = 0; i < 3; i++)
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


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();

        enemyScript = GetComponent<Enemy>();

        //rbBasic = GetComponent<Rigidbody2D>();

        InvokeRepeating("Teleport", 0, 10); //calls ChangePosition() every 10 secs
        //Physics2D.IgnoreLayerCollision(7, 6, true);
        // currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyScript.currentHealth <= 40)
        {
            SceneManager.LoadScene("Credits");
        }
    }

    void Teleport()
    {
        Transform _sp = tpPoints[Random.Range(0, tpPoints.Length)];

        Vector2 target = new Vector2(_sp.position.x, _sp.position.y);

        Vector2 newPos = Vector2.MoveTowards(_sp.position, target, Time.deltaTime);

        StartCoroutine(Intangible());
        rb.MovePosition(newPos);
    }

    IEnumerator Intangible()
    {
        Physics2D.IgnoreLayerCollision(7, 6, true); // Ignore cols with player while teleporting
        yield return new WaitForSeconds(0.4f);
        Physics2D.IgnoreLayerCollision(7, 6, false);
    }


}
