using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalvarAttacks : MonoBehaviour
{
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public float abilityRate = 4f;
    float nextAbilityTime = 0f;

    public float ultRate = 6f;
    float nextUltTime = 0f;

    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 1f;
    public int attackDamage = 100;

    public Transform wallPoint;

    public GameObject wallPrefab;

    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                HandsOfStone();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void HandsOfStone()
    {
        //Play Attack Animation
        animator.SetTrigger("Attack");

        //Detect Enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
