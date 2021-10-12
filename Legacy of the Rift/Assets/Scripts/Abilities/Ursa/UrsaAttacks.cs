using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// THIS CLASS INHERITS FROM THE ABSTRACT CLASS AttackBaseClass
public class UrsaAttacks : AttackBaseClass
{
    public PlayerZeal playerZeal;

    public int abilityZealCost = 25;

    // Start is called before the first frame update
    void Start()
    {
        attackCooldown = 2f;
        isAttackReady = true;

        abilityCooldown = 4f;
        isAbilityReady = true;

        ultCooldown = 6f;
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
                animator.SetTrigger("Attack");
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
            }
        }

        if (isAbilityReady)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                ActivateAbility();
                playerZeal.SpendZeal(abilityZealCost);
                //animator.SetTrigger("Attack");
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
                    playerZeal.SpendZeal(ultZealCost);
                    //animator.SetTrigger("Attack");
                    //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                }
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
        Instantiate(ultPrefab, ultPoint.position, ultPoint.rotation);
    }
}
