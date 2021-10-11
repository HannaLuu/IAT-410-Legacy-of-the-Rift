using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// THIS CLASS INHERITS FROM THE ABSTRACT CLASS AttackBaseClass
public class UrsaAttacks : AttackBaseClass
{
    // Start is called before the first frame update
    void Start()
    {
        attackCooldown = 2f;
        // nextAttackTime = 0f;

        abilityCooldown = 4f;
        // nextAbilityTime = 0f;

        ultCooldown = 6f;
        // nextUltTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1") && manaBar.CanSpendZeal(zealBar.zealAttackCost) == false)
        //{
        //    FindObjectOfType<AudioManager>().Play("OutOfZeal");
        //}

        // if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                //zealBar.SpendZea1(manaCost);
                animator.SetTrigger("Attack");
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
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

    // Munir's Arrow
    public override void Attack()
    {
        //manaBar.SpendZeal1(zealCost);
        Instantiate(attackPrefab, attackPoint.position, attackPoint.rotation);
    }

    // Harbinger of Life
    public override void ActivateAbility()
    {
        //manaBar.SpendZeal1(zealCost);
        Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation);
    }

    // Herald of Ruin
    public override void ActivateUlt()
    {
        //manaBar.SpendZeal1(zealCost);
        Instantiate(ultPoint, ultPoint.position, ultPoint.rotation);
    }
}
