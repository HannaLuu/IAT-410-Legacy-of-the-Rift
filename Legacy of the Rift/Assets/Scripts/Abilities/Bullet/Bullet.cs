using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public float speed = 20f;
    public int damage = 100;

    public float slowAmount = 4f;
    float slowTime = 0f;

    public Rigidbody2D rb;

    //public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //TrainingDummy td = hitInfo.GetComponent<TrainingDummy>();
        //if (td != null)
        //{
        //    td.TakeDamage(damage);
        //}

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            currentHealth -= 1;
            enemy.TakeDamage(damage);
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);


    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
