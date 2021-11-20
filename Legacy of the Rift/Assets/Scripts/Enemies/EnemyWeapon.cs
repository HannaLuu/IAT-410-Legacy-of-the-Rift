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

    public GameObject damagePopupPrefab;

    public GameObject impactEffect;

    public void Attack()
    {
        Collider2D playerColInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (playerColInfo != null)
        {
            Instantiate(impactEffect, attackPoint.position, attackPoint.rotation);
            FindObjectOfType<PlayerHealth>().TakeDamage(attackDamage);

            //damage popup
            GameObject damagePopup = Instantiate(damagePopupPrefab, playerColInfo.transform.position, Quaternion.identity);
            DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
            damagePopupScript.Setup(attackDamage);
        }
        
        Collider2D monolithColInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, monolithLayer);
        if (monolithColInfo != null)
        {
            Instantiate(impactEffect, attackPoint.position, attackPoint.rotation);
            FindObjectOfType<LegendaryMonolith>().TakeDamage(attackDamage);

            //damage popup
            GameObject damagePopup = Instantiate(damagePopupPrefab, monolithColInfo.transform.position, Quaternion.identity);
            DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
            damagePopupScript.Setup(attackDamage);
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
