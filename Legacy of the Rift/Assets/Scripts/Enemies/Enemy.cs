using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    public HealthBar healthBar;

    public float damageReceivedMultiplier;

    //public GameObject deathEffect;
    public Transform player;

    public bool isFlipped = false;

    public bool isSlowed = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        damageReceivedMultiplier = 1f;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage * damageReceivedMultiplier;
        StartCoroutine(Slowed());

        healthBar.SetHealth(currentHealth);

        Debug.Log(damage * damageReceivedMultiplier);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SlowMe()
    {
        StartCoroutine(Slowed());
    }

    public void Die()
    {
        //EnemyCounter.enemiesKilled = EnemyCounter.enemiesKilled += 1;
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    IEnumerator Slowed()
    {
        isSlowed = true;

        yield return new WaitForSeconds(3f);

        isSlowed = false;

    }
}
