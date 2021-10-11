using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralBarrage : MonoBehaviour
{
    public int health = 100;
    public int attackDamage = 10;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    public Transform attackPoint;

    //temporary disappear after x time code because I can't figure out how to get enemies to attack the clones LOL
    public float maxLifeSpan = 8f;
    public float currentLifeSpan;

    void Start()
    {
        currentLifeSpan = maxLifeSpan;
    }

    void Update()
    {
        currentLifeSpan -= Time.deltaTime;
        if (currentLifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayer);
        if (colInfo != null)
        {
            FindObjectOfType<Enemy>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //EnemyCounter.enemiesKilled = EnemyCounter.enemiesKilled += 1;
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
