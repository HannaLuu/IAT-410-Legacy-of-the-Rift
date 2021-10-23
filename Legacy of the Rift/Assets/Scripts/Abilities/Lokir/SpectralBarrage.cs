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

    WaveSpawner waveSpawner;

    //public Transform enemy;

    void Start()
    {
        currentLifeSpan = maxLifeSpan;
        //Physics2D.IgnoreLayerCollision(8, 8, true);
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

    public void removeClones()
    {
        Destroy(gameObject);
        // Debug.Log("KLFSDJK:JDSF");
    }

    //public void LookAtEnemies()
    //{
    //    Vector3 flipped = transform.localScale;
    //    flipped.z *= -1f;

    //    if (transform.position.x > player.position.x && isFlipped)
    //    {
    //        transform.localScale = flipped;
    //        transform.Rotate(0f, 180f, 0f);
    //        isFlipped = false;
    //    }
    //    else if (transform.position.x < player.position.x && !isFlipped)
    //    {
    //        transform.localScale = flipped;
    //        transform.Rotate(0f, 180f, 0f);
    //        isFlipped = true;
    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
