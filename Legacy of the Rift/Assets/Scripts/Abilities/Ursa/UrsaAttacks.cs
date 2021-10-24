using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// THIS CLASS INHERITS FROM THE ABSTRACT CLASS AttackBaseClass
public class UrsaAttacks : AttackBaseClass
{
    public PlayerZeal playerZeal;
    public int overzealRegenAmount = 5;

    public int abilityZealCost = 25;

    // Start is called before the first frame update
    void Start()
    {
        isAttackReady = true;
        currAttackCooldown = maxAttackCooldown;

        isAbilityReady = true;
        currAbilityCooldown = maxAbilityCooldown;

        isUltReady = true;
        currUltCooldown = maxUltCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        attackActivated = false;
        abilityActivated = false;
        ultActivated = false;

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
                    //animator.SetTrigger("Attack");
                    //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                }
            }
        }
    }

    // Munir's Arrow
    public override void Attack()
    {
        attackActivated = true;
        Instantiate(attackPrefab, attackPoint.position, attackPoint.rotation);
        if (playerZeal.isOverzealous == true)
        {
            playerZeal.AddOverzeal(overzealRegenAmount);
        }
    }

    // Harbinger of Life
    public override void ActivateAbility()
    {
        abilityActivated = true;
        playerZeal.SpendZeal(abilityZealCost);
        Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation);
    }

    // Herald of Ruin
    public override void ActivateUlt()
    {
        ultActivated = true;
        playerZeal.SpendZeal(ultZealCost);
        Instantiate(ultPrefab, ultPoint.position, ultPoint.rotation);
    }
}
