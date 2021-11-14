using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalvarAttacks : AttackBaseClass
{
    public Vector3 attackRange;
    public int attackDamage = 100;
    public int overzealRegenAmount = 5;

    public int abilityZealCost = 25;

    public LayerMask enemyLayers;

    public PlayerZeal playerZeal;

    public Transform ultPoint2;

    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        isAttackReady = true;
        currAttackCooldown = maxAttackCooldown;

        isAbilityReady = true;
        currAbilityCooldown = maxAbilityCooldown;

        isUltReady = true;
        currUltCooldown = maxUltCooldown;

        enemyCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        attackActivated = false;
        abilityActivated = false;
        ultActivated = false;
        enemyCollided = false;

        if (isAttackReady)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                attackActivated = true;
                //Play Attack Animation
                animator.SetTrigger("Attack");
            }
        }

        if (isAbilityReady)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                playerZeal.SpendZeal(abilityZealCost);
                if (playerZeal.canSpendZeal == true)
                {
                    abilityActivated = true;
                    animator.SetTrigger("Ability");
                }
                else
                {
                    Debug.Log("You've Run Out of Zeal! Wait to Regen!");
                }

                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
            }
        }

        if (isUltReady && isUltUnlocked)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                if (playerZeal.fullyZealous == true)
                {
                    playerZeal.SpendZeal(ultZealCost);
                    if (playerZeal.canSpendZeal == true)
                    {
                        ultActivated = true;
                        animator.SetTrigger("Ultimate");
                    }
                    else
                    {
                        Debug.Log("You've Run Out of Zeal! Wait to Regen!");
                    }
                }
            }

        }
    }

    // Hands of Stone
    public override void Attack()
    {

        //Detect Enemies in range of attack
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, 0, enemyLayers);

        //Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponentInParent<Enemy>().TakeDamage(attackDamage);
            Instantiate(impactEffect, enemy.gameObject.transform.position, enemy.gameObject.transform.rotation);
            enemyCollided = true;
            playerZeal.AddOverzeal(attackDamage);

            //old overzeal mechanic
            //if (playerZeal.isOverzealous == true)
            //{
            //    playerZeal.AddOverzeal(overzealRegenAmount);
            //}
        }
    }

    // Defense of the Ancients
    public override void ActivateAbility()
    {
        abilityActivated = true;
        Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation);
    }

    // Guardian of the Rock
    public override void ActivateUlt()
    {
        ultActivated = true;
        Instantiate(lokirUltPrefab, lokirUltPoint.position, lokirUltPoint.rotation);
        Instantiate(halvarUltPrefab, halvarUltPointL.position, halvarUltPointL.rotation);
        Instantiate(halvarUltPrefab, halvarUltPointR.position, halvarUltPointR.rotation);
        Instantiate(ursaUltPrefab, ursaUltPoint.position, ursaUltPoint.rotation);
        FindObjectOfType<PlayerHealth>().Heal(100);

        //old ult code
        //Instantiate(ultPrefab, ultPoint.position, ultPoint.rotation);
        //Instantiate(ultPrefab, ultPoint2.position, ultPoint2.rotation);
        //FindObjectOfType<AudioManager>().Play("PlayerAttack");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }
}
