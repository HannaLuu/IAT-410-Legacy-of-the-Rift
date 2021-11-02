using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWProjectile : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 100;

    public Rigidbody2D rb;

    public GameObject impactEffect;

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        Enemy enemy = hitInfo.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);


    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
