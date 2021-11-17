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
                attackActivated = true;
                animator.SetTrigger("Attack");
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
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
                    //animator.SetTrigger("Attack");
                    //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                }
                else
                {
                    Debug.Log("You've Run Out of Zeal! Wait to Regen!");
                }

            }
        }

        if (isUltReady && isUltUnlocked)
        {
            if (playerZeal.fullyZealous == true)
            {
                if (Input.GetButtonDown("Fire3"))
                {
                    playerZeal.SpendZeal(100);
                    playerZeal.SpendOverzeal(ultZealCost);
                    if (playerZeal.canSpendZeal == true)
                    {
                        ultActivated = true;
                        animator.SetTrigger("Ultimate");
                        //animator.SetTrigger("Attack");
                        //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                    }
                    else
                    {
                        Debug.Log("You've Run Out of Zeal! Wait to Regen!");
                    }
                }
            }
        }
    }

    // Munir's Arrow
    public override void Attack()
    {
        Instantiate(attackPrefab, attackPoint.position, attackPoint.rotation);
    }

    // Harbinger of Life
    public override void ActivateAbility()
    {
        Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation);
    }

    // Herald of Ruin
    public override void ActivateUlt()
    {
        Instantiate(lokirUltPrefab, lokirUltPoint.position, lokirUltPoint.rotation);
        Instantiate(halvarUltPrefab, halvarUltPointL.position, halvarUltPointL.rotation);
        Instantiate(halvarUltPrefab, halvarUltPointR.position, halvarUltPointR.rotation);
        Instantiate(ursaUltPrefab, ursaUltPoint.position, ursaUltPoint.rotation);
        FindObjectOfType<PlayerHealth>().Heal(100);

        //old ult code
        //Instantiate(ultPrefab, ultPoint.position, ultPoint.rotation);
    }
}
