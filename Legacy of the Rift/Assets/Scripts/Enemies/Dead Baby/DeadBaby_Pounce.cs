using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBaby_Pounce : MonoBehaviour
{
    public Rigidbody2D rb;

    public int attackDamage = 10;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    public Enemy babyEnemyScript;

    public Transform attackPoint;

    public bool ignorePlayerCollision;

    public void Attack()
    {
        if(babyEnemyScript.isFlipped == false)
        {
            StartCoroutine(Pounce(-1f, 15f));
        }

        if (babyEnemyScript.isFlipped == true)
        {
            StartCoroutine(Pounce(1f, 15f));
        }

        Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (colInfo != null)
        {
            FindObjectOfType<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    IEnumerator Pounce(float distanceMultiplier, float jumpHeight)
    {
        float speed = 15f;

        rb.velocity = new Vector2(speed * distanceMultiplier, jumpHeight);

        ignorePlayerCollision = true;
        Physics2D.IgnoreLayerCollision(6, 7, true);


        yield return new WaitForSeconds(0.3f);


        ignorePlayerCollision = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);

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
