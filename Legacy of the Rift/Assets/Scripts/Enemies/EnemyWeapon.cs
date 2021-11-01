using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Rigidbody2D rb;

    public int attackDamage = 10;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public LayerMask monolithLayer;

    public Transform attackPoint;

    public void Attack()
    {
        Collider2D playerColInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (playerColInfo != null)
        {
            FindObjectOfType<PlayerHealth>().TakeDamage(attackDamage);
        }
        
        Collider2D monolithColInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, monolithLayer);
        if (monolithColInfo != null)
        {
            FindObjectOfType<LegendaryMonolith>().TakeDamage(attackDamage);
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
