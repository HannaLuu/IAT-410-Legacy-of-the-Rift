using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float maxHealth = 100;
    public float currentHealth;

    public HealthBar healthBar;

    public float damageReceivedMultiplier;

    //public GameObject deathEffect;
    public Transform player;

    public bool isFlipped = false;

    public bool isSlowed = false;
    public bool isDead;

    public Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        damageReceivedMultiplier = 1f;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage * damageReceivedMultiplier;

        healthBar.SetHealth(currentHealth);

        Debug.Log(damage * damageReceivedMultiplier);

        animator.SetTrigger("Hit");

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void SlowMe()
    {
        StartCoroutine(Slowed());
    }

    public void Die() {
        isDead = true;
        //EnemyCounter.enemiesKilled = EnemyCounter.enemiesKilled += 1;
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        WaveSpawner waveSpawner = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveSpawner>();
        waveSpawner.DecrementEnemyCount();
        Debug.Log(gameObject.name + "DIED! " + waveSpawner.enemyCount);
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
        healthBar.gameObject.transform.rotation = Quaternion.identity;
    }

    public void LookOppositePlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }

    IEnumerator Slowed()
    {
        isSlowed = true;

        yield return new WaitForSeconds(3f);

        isSlowed = false;

    }
}
