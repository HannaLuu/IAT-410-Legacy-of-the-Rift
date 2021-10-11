using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalvarAttacks : AttackBaseClass
{
    public float attackRange = 1f;
    public int attackDamage = 100;

    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        attackCooldown = 2f;
        // nextAttackTime = 0f;

        abilityCooldown = 6f;
        // nextAbilityTime = 0f;

        ultCooldown = 12f;
        // nextUltTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                // nextAttackTime = Time.time + 1f / attackCooldown;
            }
        }

        // if (Time.time >= nextAbilityTime)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                ActivateAbility();
                //zealBar.SpendZeal1(zealCost);
                //animator.SetTrigger("Attack");
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                // nextAbilityTime = Time.time + 1f / abilityCooldown;
            }
        }
    }

    // Hands of Stone
    public override void Attack()
    {
        //Play Attack Animation
        animator.SetTrigger("Attack");

        //Detect Enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    // Defense of the Ancients
    public override void ActivateAbility()
    {
        //manaBar.SpendZeal1(zealCost);
        Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation);
    }

    // Guardian of the Rock
    public override void ActivateUlt()
    {
        Debug.Log("No Guardian of the Rock ability Yet!");
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
