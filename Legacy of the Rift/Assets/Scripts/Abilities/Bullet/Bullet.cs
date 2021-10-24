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
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            hitInfo.GetComponentInParent<Enemy>().TakeDamage(damage);
            currentHealth -= 1;

        }

        //Instantiate(impactEffect, transform.position, transform.rotation);
    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
