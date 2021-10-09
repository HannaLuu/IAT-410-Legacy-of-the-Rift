using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public LayerMask monolithLayer;

    public Transform attackPoint;

    public void Attack()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (colInfo != null)
        {
            FindObjectOfType<PlayerHealth>().TakeDamage(attackDamage);
        }
        Collider2D colMonolith = Physics2D.OverlapCircle(attackPoint.position, attackRange, monolithLayer);
        if (colMonolith != null)
        {
            Debug.Log("ROCK COLLIDING");
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
