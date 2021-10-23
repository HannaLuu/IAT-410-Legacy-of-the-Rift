using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// THIS CLASS INHERITS FROM THE ABSTRACT CLASS AttackBaseClass
public class UrsaAttacks : AttackBaseClass
{
    public PlayerZeal playerZeal;
    public int overzealRegenAmount = 5;

    public int abilityZealCost = 25;

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
        if (playerZeal.isOverzealous == true)
        {
            playerZeal.AddOverzeal(overzealRegenAmount);
        }
        StartCoroutine(BasicCooldown());
    }

    // Harbinger of Life
    public override void ActivateAbility()
    {
        //manaBar.SpendZeal1(zealCost);
        Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation);
        StartCoroutine(AbilityCooldown());
    }

    // Herald of Ruin
    public override void ActivateUlt()
    {
        //manaBar.SpendZeal1(zealCost);
        Instantiate(ultPrefab, ultPoint.position, ultPoint.rotation);
        StartCoroutine(UltCooldown());
    }
}
