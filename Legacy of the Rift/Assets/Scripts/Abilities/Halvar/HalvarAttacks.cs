using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HalvarAttacks : AttackBaseClass
{
    public Vector3 attackRange;
    // public int attackDamage = 100;

    public int minAttackDamage;
    public int maxAttackDamage;
    public int damageDone;

    public int overzealRegenAmount = 5;

    public int abilityZealCost = 25;

    public LayerMask enemyLayers;

    public PlayerZeal playerZeal;

    public Transform ultPoint2;

    public GameObject impactEffect;

    public GameObject healingPopupPrefab;

    public Animator halvarAbilityBarAnimator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        isAttackReady = true;
        currAttackCooldown = maxAttackCooldown;

        isAbilityReady = true;
        currAbilityCooldown = maxAbilityCooldown;

        isUltReady = true;
        currUltCooldown = maxUltCooldown;

        enemyCollided = false;

        dontMove = false;
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
                dontMove = true;
                attackActivated = true;
                //Play Attack Animation
                rb.velocity = new Vector2(0, rb.velocity.y);
                FindObjectOfType<AudioManager>().Play("Halvar Hammer Swing");
                animator.SetTrigger("Attack");
            }
        }

        if (isAbilityReady)
        {
            playerZeal.CheckEnoughZeal(abilityZealCost);
            if (playerZeal.canSpendZeal == true)
            {
                halvarAbilityBarAnimator.SetBool("abilityReady", true);
                if (Input.GetButtonDown("Fire2") && dontMove == false)
                {
                    dontMove = true;
                    FindObjectOfType<AudioManager>().Play("Halvar Rock Wall Emerge");
                    animator.SetTrigger("Ability");
                }
            } else
            {
                halvarAbilityBarAnimator.SetBool("abilityReady", false);
            }
        } else
        {
            halvarAbilityBarAnimator.SetBool("abilityReady", true);
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
                        dontMove = true;
                        animator.SetTrigger("Ultimate");
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

    // Hands of Stone
    public override void Attack()
    {
        //Detect Enemies in range of attack
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, 0, enemyLayers);

        //Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            damageDone = Random.Range(minAttackDamage, maxAttackDamage);
            enemy.GetComponentInParent<Enemy>().TakeDamage(damageDone);
            Instantiate(impactEffect, enemy.gameObject.transform.position, enemy.gameObject.transform.rotation);

            //damage popup
            GameObject damagePopup = Instantiate(damagePopupPrefab, enemy.transform.position, Quaternion.identity);
            DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
            damagePopupScript.Setup(damageDone);

            enemyCollided = true;

            playerZeal.AddOverzeal(damageDone);

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
        rb.velocity = new Vector2(0, 0);
        playerZeal.SpendZeal(abilityZealCost);
        Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation);
    }

    // Guardian of the Rock
    public void SpectralBarrage()
    {
        ultActivated = true;
        rb.velocity = new Vector2(0, 0);
        Instantiate(lokirUltPrefab, lokirUltPoint.position, Quaternion.identity);
        FindObjectOfType<PlayerHealth>().Heal(100);
    }

    public void PlayHalvarUltSound()
    {
        FindObjectOfType<AudioManager>().Play("Halvar Rock Wall Emerge");
    }

    public void HalvarUlt()
    {
        rb.velocity = new Vector2(0, 0);
        Instantiate(halvarUltPrefab, halvarUltPointL.position, halvarUltPointL.rotation);
        Instantiate(halvarUltPrefab, halvarUltPointR.position, halvarUltPointR.rotation);
        FindObjectOfType<PlayerHealth>().Heal(100);
    }

    public void PlayUrsaUltSound()
    {
        FindObjectOfType<AudioManager>().Play("Ursa Spawn Bear");
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


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }
}
