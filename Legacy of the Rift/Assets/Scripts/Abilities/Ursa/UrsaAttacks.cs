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

    public GameObject healingPopupPrefab;

    public Animator ursaAbilityBarAnimator;

    public GameObject lokirAbilityAudio, halvarUltAudio;

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
                rb.velocity = new Vector2(0, rb.velocity.y);
                FindObjectOfType<AudioManager>().Play("Ursa Shoot Arrow");
                animator.SetTrigger("Attack");
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
            }


        }

        if (isAbilityReady)
        {
            playerZeal.CheckEnoughZeal(abilityZealCost);
            if (playerZeal.canSpendZeal == true)
            {
                ursaAbilityBarAnimator.SetBool("abilityReady", true);
                if (Input.GetButtonDown("Fire2") && dontMove == false)
                {
                    abilityActivated = true;
                    dontMove = true;
                    rb.velocity = new Vector2(0, 0);
                    FindObjectOfType<AudioManager>().Play("Ursa Healing VO");
                    animator.SetTrigger("Ability");
                    //animator.SetTrigger("Attack");
                    //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                }
            } else
            {
                ursaAbilityBarAnimator.SetBool("abilityReady", false);
            }
        } else
        {
            ursaAbilityBarAnimator.SetBool("abilityReady", false);
        }

        if (isUltReady && isUltUnlocked)
        {
            if (playerZeal.fullyZealous == true)
            {
                ultReadyGlow.SetActive(true);
                if (Input.GetButtonDown("Fire3") && dontMove == false)
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

        if (playerZeal.fullyZealous == false)
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
        playerZeal.SpendZeal(abilityZealCost);
        Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation);
    }


    public void PlayLokirUltSound()
    {
        var source = lokirAbilityAudio.GetComponent<AudioSource>();
        source.clip = lokirAbilityAudio.GetComponent<RandomSound>().GetRandomAudioClip();
        source.Play();
    }

    public void SpectralBarrage()
    {
        ultActivated = true;
        rb.velocity = new Vector2(0, 0);
        Instantiate(lokirUltPrefab, lokirUltPoint.position, lokirUltPoint.rotation);

        //healing popup
        GameObject healingPopup = Instantiate(healingPopupPrefab, transform.position, Quaternion.identity);
        HealingPopup healingPopupScript = healingPopup.GetComponent<HealingPopup>();
        healingPopupScript.Setup(100);
        FindObjectOfType<PlayerHealth>().Heal(100);
    }

    public void PlayHalvarUltSound()
    {
        var source = halvarUltAudio.GetComponent<AudioSource>();
        source.clip = halvarUltAudio.GetComponent<RandomSound>().GetRandomAudioClip();
        source.Play();
        FindObjectOfType<AudioManager>().Play("Halvar Rock Wall Emerge");
    }

    public void HalvarUlt()
    {
        rb.velocity = new Vector2(0, 0);
        Instantiate(halvarUltPrefab, halvarUltPointL.position, halvarUltPointL.rotation);
        Instantiate(halvarUltPrefab, halvarUltPointR.position, halvarUltPointR.rotation);

        //healing popup
        GameObject healingPopup = Instantiate(healingPopupPrefab, transform.position, Quaternion.identity);
        HealingPopup healingPopupScript = healingPopup.GetComponent<HealingPopup>();
        healingPopupScript.Setup(100);
        FindObjectOfType<PlayerHealth>().Heal(100);
    }

    public void PlayUrsaUltSound()
    {
        FindObjectOfType<AudioManager>().Play("Ursa Invoking Spirit VO");
        FindObjectOfType<AudioManager>().Play("Ursa Spawn Bear");
    }

    public void UrsaUlt()
    {
        rb.velocity = new Vector2(0, 0);
        Instantiate(ursaUltPrefab, ursaUltPoint.position, ursaUltPoint.rotation);

        //healing popup
        GameObject healingPopup = Instantiate(healingPopupPrefab, transform.position, Quaternion.identity);
        HealingPopup healingPopupScript = healingPopup.GetComponent<HealingPopup>();
        healingPopupScript.Setup(100);
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
