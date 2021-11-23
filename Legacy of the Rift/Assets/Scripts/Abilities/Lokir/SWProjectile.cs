using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWProjectile : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 100;

    public Rigidbody2D rb;

    public GameObject impactEffect;
    public GameObject damagePopupPrefab;

    public Transform target;

    private List<Transform> enemies = new List<Transform>();

    private Vector2 offsetPosition;

    // public Vector2 toEnemy;

    private void Start()
    {
        // rb.velocity = transform.right * speed;
        // target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();

        // WaveSpawner waveSpawner = GameObject.FindObjectOfType<WaveSpawner>();
        // if (waveSpawner != null && waveSpawner.EnemyIsAlive() == true)
        // {
        //     FindNearestEnemy();
        //     // ShootBalls();
        // }
        // if (waveSpawner == null)
        // {
        //     Enemy enemy = GameObject.FindObjectOfType<Enemy>();
        //     if (enemy != null)
        //     {
        //         FindNearestEnemy();
        //         // ShootBalls();
        //     }
        // }

        // rb.velocity = new Vector2(offsetPosition.x * speed, offsetPosition.y * speed);


    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        Enemy enemy = hitInfo.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            enemy.TakeDamage(damage);

            //damage popup
            GameObject damagePopup = Instantiate(damagePopupPrefab, hitInfo.transform.position, Quaternion.identity);
            DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
            damagePopupScript.Setup(damage);

            Destroy(gameObject);
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);
    }

    private void Update()
    {
    }


    public void SetDirection(Vector2 dir)
    {
        rb.velocity = dir * speed;
    }

    private void FixedUpdate()
    {

        // WaveSpawner waveSpawner = GameObject.FindObjectOfType<WaveSpawner>();
        // if (waveSpawner != null && waveSpawner.EnemyIsAlive() == true)
        // {
        //     // FindNearestEnemy();
        //     ShootBalls();
        // }
        // if (waveSpawner == null)
        // {
        //     Enemy enemy = GameObject.FindObjectOfType<Enemy>();
        //     if (enemy != null)
        //     {
        //         // FindNearestEnemy();
        //         ShootBalls();
        //     }
        // }

        // if (!target)
        // {
        //     ShootBalls();
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }

    void ShootBalls()
    {
        // Vector2 direction = (Vector2)target.position - rb.position;

        // direction.Normalize();

        // float rotateAmount = Vector3.Cross(direction, transform.up).z;

        // rb.angularVelocity = -rotateAmount * 200;

        // rb.velocity = transform.up * speed;

        // transform.position = Vector2.MoveTowards(transform.position, offsetPosition, speed * Time.deltaTime);
    }

    // public static Vector2 GetVectorToPoint(Vector3 source, Vector3 destination)
    // {
    //     Vector2 source2D = new Vector2(source.x, source.y);
    //     Vector2 destination2D = new Vector2(destination.x, destination.y);

    //     return (destination2D - source2D).normalized;
    // }

    // public void FindNearestEnemy()
    // {
    //     float nearestEnemyDistance = float.MaxValue;

    //     Enemy[] enemyArray = FindObjectsOfType<Enemy>();

    //     foreach (Enemy enemy in enemyArray)
    //     {
    //         enemies.Add(enemy.transform);
    //     }

    //     foreach (Transform enemy in enemies)
    //     {
    //         float currEnemyDistance;
    //         if (enemy != null)
    //         {
    //             offsetPosition = new Vector2(enemy.position.x, enemy.position.y + enemy.GetComponentInChildren<BoxCollider2D>().offset.y);

    //             currEnemyDistance = Vector2.Distance(transform.position, enemy.position);

    //             if (nearestEnemyDistance > currEnemyDistance)
    //             {
    //                 nearestEnemyDistance = currEnemyDistance;
    //                 target = enemy;

    //                 // transform.position = Vector2.MoveTowards(transform.position, offsetPosition, speed);
    //             }
    //         }
    //     }
    // }
}
