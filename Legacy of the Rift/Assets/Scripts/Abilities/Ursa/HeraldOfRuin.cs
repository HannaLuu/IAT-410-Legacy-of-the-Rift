using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeraldOfRuin : MonoBehaviour
{
    public Transform attackPoint;
    public int attackDamage = 30;
    public float collisionRange = 0.5f;
    public float explosionRange = 6f;
    public LayerMask enemyLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if an explosion range is set
        if(explosionRange > 0)
        {
            //collect all objects the explosion collides with
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRange);
            //for each object in the list
            foreach(var hitCollider in hitColliders)
            {
                //if the object has an enemy script, store it
                var enemy = hitCollider.GetComponent<Enemy>();
                if (enemy)
                {
                    //calculate falloff distance for damage, the father the enemy is from center of explosion, the less damage it takes
                    var closestPoint = hitCollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closestPoint, transform.position);

                    var damagePercent = Mathf.InverseLerp(explosionRange, 0, distance);
                    enemy.TakeDamage(damagePercent * attackDamage);
                    //destroy itself after explosion done
                    Destroy(gameObject);
                }
            }
            //if the explosion range is not set, it acts like a charge attack on a single enemy
        } else
        {
            Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, collisionRange, enemyLayer);
            if (colInfo != null)
            {
                FindObjectOfType<Enemy>().TakeDamage(attackDamage);
            }
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, collisionRange);
    }
}
