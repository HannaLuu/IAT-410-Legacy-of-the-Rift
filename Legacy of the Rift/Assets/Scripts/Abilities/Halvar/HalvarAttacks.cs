using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalvarAttacks : AttackBaseClass
{
    public float attackRange = 1f;
    public int attackDamage = 100;

    public int abilityZealCost = 25;

    public LayerMask enemyLayers;

    public PlayerZeal playerZeal;

    // Start is called before the first frame update
    void Start()
    {
        attackCooldown = 2f;
        isAttackReady = true;

        abilityCooldown = 6f;
        isAbilityReady = true;

        ultCooldown = 12f;
        isUltReady = true;
        ultZealCost = 150;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttackReady)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
            }
        }

        if (isAbilityReady)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                animator.SetTrigger("Ability");
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
            }
        }

        if (isUltReady)
        {
            if(playerZeal.isOverzealous == true)
            {
                if (Input.GetButtonDown("Fire3"))
                {
                    ActivateUlt();
                }
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
        playerZeal.SpendZeal(abilityZealCost);
        Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation);
    }

    // Guardian of the Rock
    public override void ActivateUlt()
    {
        Debug.Log("No Guardian of the Rock ability Yet!");
        //playerZeal.SpendZeal(ultZealCost);
        //animator.SetTrigger("Attack");
        //FindObjectOfType<AudioManager>().Play("PlayerAttack");
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
