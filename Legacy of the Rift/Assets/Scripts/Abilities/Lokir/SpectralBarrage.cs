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

    public bool isFlipped = false;
    private List<Transform> enemies = new List<Transform>();
    public Transform nearestEnemy;

    public GameObject impactEffect;
    public GameObject damagePopupPrefab;

    public GameObject smokeParticle;

    void Start()
    {
        currentLifeSpan = maxLifeSpan;
        Instantiate(smokeParticle, transform.position, transform.rotation);
        //Physics2D.IgnoreLayerCollision(8, 8, true);
    }

    void Update()
    {
        currentLifeSpan -= Time.deltaTime;
        if (currentLifeSpan <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayer);
        if (colInfo != null)
        {
            Instantiate(impactEffect, attackPoint.position, attackPoint.rotation);
            FindObjectOfType<Enemy>().TakeDamage(attackDamage);

            //damage popup
            GameObject damagePopup = Instantiate(damagePopupPrefab, colInfo.transform.position, Quaternion.identity);
            DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
            damagePopupScript.Setup(attackDamage);
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
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(smokeParticle, transform.position, transform.rotation);
    }

    public void LookAtEnemies()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > nearestEnemy.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (transform.position.x < nearestEnemy.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }

    public void FindNearestEnemy()
    {
        float nearestEnemyDistance = float.MaxValue;

        Enemy[] enemyArray = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemyArray)
        {
            enemies.Add(enemy.transform);
        }

        foreach (Transform enemy in enemies)
        {
            float currEnemyDistance;
            if(enemy != null)
            {
                currEnemyDistance = Vector2.Distance(transform.position, enemy.position);
                if (nearestEnemyDistance > currEnemyDistance)
                {
                    nearestEnemyDistance = currEnemyDistance;
                    nearestEnemy = enemy;
                }
            }
        }
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
