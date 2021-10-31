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

    public UrsaAttacks ursaAttackScript;

    //public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        ursaAttackScript = GameObject.FindGameObjectWithTag("Ursa").GetComponent<UrsaAttacks>();
        playerZeal = GameObject.FindGameObjectWithTag("PlayerHandler").GetComponent<PlayerZeal>();
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
            if (playerZeal.isOverzealous == true && playerZeal.currentZeal < playerZeal.maxZeal)
            {
                playerZeal.AddOverzeal(ursaAttackScript.overzealRegenAmount);
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
