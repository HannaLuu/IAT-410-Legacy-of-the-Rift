using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 100;

    public Rigidbody2D rb;

    //public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
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
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
        //Instantiate(impactEffect, transform.position, transform.rotation);


    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
