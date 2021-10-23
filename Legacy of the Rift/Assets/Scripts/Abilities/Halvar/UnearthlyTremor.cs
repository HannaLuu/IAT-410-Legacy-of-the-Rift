using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnearthlyTremor : MonoBehaviour
{
    public Rigidbody2D rb;

    public float maxLifeSpan = 1f;
    public float currentLifeSpan;

    public int damage = 100;

    // Start is called before the first frame update
    void Start()
    {
        currentLifeSpan = maxLifeSpan;
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeSpan -= Time.deltaTime;
        if (currentLifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
