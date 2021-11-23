using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


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

        dontMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody2D>();

        attackActivated = false;
        abilityActivated = false;
        ultActivated = false;

        if (isAttackReady)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                attackActivated = true;
                dontMove = true;
                rb.velocity = new Vector2(0, 0);
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
                    dontMove = true;
                    rb.velocity = new Vector2(0, 0);
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
                ultReadyGlow.SetActive(true);
                if (Input.GetButtonDown("Fire3"))
                {
                    playerZeal.SpendZeal(100);
                    playerZeal.SpendOverzeal(ultZealCost);
                    if (playerZeal.canSpendZeal == true)
                    {
                        ultActivated = true;
                        dontMove = true;
                        rb.velocity = new Vector2(0, 0);
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

        if (!isUltReady)
        {
            ultReadyGlow.SetActive(false);
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
    public void SpectralBarrage()
    {
        ultActivated = true;
        rb.velocity = new Vector2(0, 0);
        Instantiate(lokirUltPrefab, lokirUltPoint.position, lokirUltPoint.rotation);
    }

    public void HalvarUlt()
    {
        rb.velocity = new Vector2(0, 0);
        Instantiate(halvarUltPrefab, halvarUltPointL.position, halvarUltPointL.rotation);
        Instantiate(halvarUltPrefab, halvarUltPointR.position, halvarUltPointR.rotation);
    }

    public void UrsaUlt()
    {
        rb.velocity = new Vector2(0, 0);
        Instantiate(ursaUltPrefab, ursaUltPoint.position, ursaUltPoint.rotation);
        FindObjectOfType<PlayerHealth>().Heal(100);
    }

    public override void ActivateUlt()
    {

        //old ult code
        //foreach (Transform spawnPoint in ultPoints)
        //{
        //    Instantiate(ultPrefab, spawnPoint.position, spawnPoint.rotation);

    }
}
