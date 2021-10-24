using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalvarAttacks : AttackBaseClass
{
    public float attackRange = 1f;
    public int attackDamage = 100;
    public int overzealRegenAmount = 5;

    public int abilityZealCost = 25;

    public LayerMask enemyLayers;

    public PlayerZeal playerZeal;

    public Transform ultPoint2;

    public CooldownBar abilityCooldownBar;
    public CooldownBar ultCooldownBar;

    // Start is called before the first frame update
    void Start()
    {
        isAttackReady = true;
        currAttackCooldown = maxAttackCooldown;

        isAbilityReady = true;
        currAbilityCooldown = maxAbilityCooldown;
        abilityCooldownBar.SetMaxCooldown(maxAbilityCooldown);

        isUltReady = true;
        currUltCooldown = maxUltCooldown;
        ultCooldownBar.SetMaxCooldown(maxUltCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        abilityCooldownBar.SetCooldown(currAbilityCooldown);
        ultCooldownBar.SetCooldown(currUltCooldown);

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
                    animator.SetTrigger("Ultimate");
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
            enemy.GetComponentInParent<Enemy>().TakeDamage(attackDamage);
            if (playerZeal.isOverzealous == true)
            {
                playerZeal.AddOverzeal(overzealRegenAmount);
            }
        }

        StartCoroutine(BasicCooldown());
    }

    // Defense of the Ancients
    public override void ActivateAbility()
    {
        playerZeal.SpendZeal(abilityZealCost);
        Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation);
        StartCoroutine(AbilityCooldown());
    }

    // Guardian of the Rock
    public override void ActivateUlt()
    {
        playerZeal.SpendZeal(ultZealCost);
        Instantiate(ultPrefab, ultPoint.position, ultPoint.rotation);
        Instantiate(ultPrefab, ultPoint2.position, ultPoint2.rotation);
        //animator.SetTrigger("Attack");
        //FindObjectOfType<AudioManager>().Play("PlayerAttack");
        StartCoroutine(UltCooldown());
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
