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

    private bool debuffApplied;

    public GameObject impactEffect;
    public GameObject explosionEffect, explosionParticle;
    public GameObject damagePopupPrefab;

    public void Explode()
    {
        //if an explosion range is set
        if (explosionRange > 0)
        {
            //collect all objects the explosion collides with
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRange);
            //for each object in the list
            foreach (var hitCollider in hitColliders)
            {
                //if the object has an enemy script, store it
                var enemy = hitCollider.GetComponentInParent<Enemy>();
                if (enemy)
                {
                    Instantiate(impactEffect, transform.position, transform.rotation);
                    impactEffect.transform.localScale = new Vector3(4, 4, 4);
                    //calculate falloff distance for damage, the father the enemy is from center of explosion, the less damage it takes
                    var closestPoint = hitCollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closestPoint, transform.position);

                    var damagePercent = Mathf.InverseLerp(explosionRange, 0, distance);
                    var explosionDamage = damagePercent * attackDamage;
                    enemy.TakeDamage(explosionDamage);

                    //damage popup
                    GameObject damagePopup = Instantiate(damagePopupPrefab, enemy.transform.position, Quaternion.identity);
                    DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
                    damagePopupScript.Setup((int) explosionDamage);

                    // Apply Damage Absorption Debuff. Legacies deal 25% more damage for 20 secs
                    StartCoroutine(DebuffTimer());
                    if (debuffApplied == true)
                    {
                        enemy.damageReceivedMultiplier = 1.25f;
                    }

                    else
                    {
                        enemy.damageReceivedMultiplier = 1f;
                    }
                }
            }
            //destroy itself after explosion done
            Destroy(gameObject);

            //if the explosion range is not set, it acts like a charge attack on a single enemy
        }
        else
        {
            Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, collisionRange, enemyLayer);
            if (colInfo.gameObject.CompareTag("Enemy"))
            {
                colInfo.GetComponentInParent<Enemy>().TakeDamage(attackDamage);

                //damage popup
                GameObject damagePopup = Instantiate(damagePopupPrefab, colInfo.transform.position, Quaternion.identity);
                DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
                damagePopupScript.Setup(attackDamage);
            }
        }
    }

    private void OnDestroy()
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Instantiate(explosionParticle, transform.position, transform.rotation);
    }

    //old ult code
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //if an explosion range is set
    //    if (explosionRange > 0)
    //    {
    //        //collect all objects the explosion collides with
    //        var hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRange);
    //        //for each object in the list
    //        foreach (var hitCollider in hitColliders)
    //        {
    //            //if the object has an enemy script, store it
    //            var enemy = hitCollider.GetComponentInParent<Enemy>();
    //            if (enemy)
    //            {
    //                Instantiate(impactEffect, transform.position, transform.rotation);
    //                impactEffect.transform.localScale = new Vector3(4, 4, 4);
    //                //calculate falloff distance for damage, the father the enemy is from center of explosion, the less damage it takes
    //                var closestPoint = hitCollider.ClosestPoint(transform.position);
    //                var distance = Vector3.Distance(closestPoint, transform.position);

    //                var damagePercent = Mathf.InverseLerp(explosionRange, 0, distance);
    //                enemy.TakeDamage(damagePercent * attackDamage);

    //                // Apply Damage Absorption Debuff. Legacies deal 25% more damage for 20 secs
    //                StartCoroutine(DebuffTimer());
    //                if (debuffApplied == true)
    //                {
    //                    enemy.damageReceivedMultiplier = 1.25f;
    //                }

    //                else
    //                {
    //                    enemy.damageReceivedMultiplier = 1f;
    //                }

    //                //destroy itself after explosion done
    //                Destroy(gameObject);
    //            }
    //        }
    //        //if the explosion range is not set, it acts like a charge attack on a single enemy
    //    }
    //    else
    //    {
    //        Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, collisionRange, enemyLayer);
    //        if (colInfo.gameObject.CompareTag("Enemy"))
    //        {
    //            Instantiate(impactEffect, transform.position, transform.rotation);
    //            colInfo.GetComponentInParent<Enemy>().TakeDamage(attackDamage);
    //        }
    //    }

    //}

    IEnumerator DebuffTimer()
    {
        debuffApplied = true;

        yield return new WaitForSeconds(20f);

        debuffApplied = false;

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, collisionRange);
        Gizmos.DrawWireSphere(attackPoint.position, explosionRange);
    }
}


