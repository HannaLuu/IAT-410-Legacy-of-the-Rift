using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public float speed = 20f;
    public int damage = 100;

    public float slowAmount = 4f;
    float slowTime = 0f;

    public Rigidbody2D rb;

    public PlayerZeal playerZeal;
    public PlayerZeal2 playerZeal2;

    public UrsaAttacks ursaAttackScript;

    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        ursaAttackScript = GameObject.FindGameObjectWithTag("Ursa").GetComponent<UrsaAttacks>();
        playerZeal = GameObject.FindGameObjectWithTag("PlayerHandler").GetComponent<PlayerZeal>();
        playerZeal2 = GameObject.FindGameObjectWithTag("PlayerHandler").GetComponent<PlayerZeal2>();
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
            hitInfo.GetComponentInParent<Enemy>().SlowMe();
            Instantiate(impactEffect, transform.position, transform.rotation);
            currentHealth -= 1;
            if (playerZeal != null && playerZeal.isOverzealous == true)
            {
                playerZeal.AddOverzeal(ursaAttackScript.overzealRegenAmount);
            }
            if (playerZeal2 != null && playerZeal2.isOverzealous == true)
            {
                playerZeal2.AddOverzeal(ursaAttackScript.overzealRegenAmount);
            }
        }
        if (hitInfo.gameObject.CompareTag("Ground"))
        {
            enabled = false;
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
