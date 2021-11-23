using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LokirAttacks : AttackBaseClass
{
    public int minAttackDamage;
    public int maxAttackDamage;
    public int damageDone;

    //old overzeal mechanic
    //public int overzealRegenAmount = 5;

    public int abilityZealCost = 25;

    GameObject spectralWarlock = null;
    Vector3 teleportPos;

    public float attackRange;
    public LayerMask enemyLayers;

    public PlayerZeal playerZeal;

    public bool ignoreEnemyCollision;

    public bool teleported = false;

    //damage feedback
    public GameObject impactEffect;

    PlayerSwitching playerSwitching;

    private float timeBetweenTrail;
    public float startTimeBetweenTrail;

    public GameObject trail;

    //old ult code
    //public int ultClones = 3;
    //private int clonesSpawned = 0;
    //public List<Transform> ultPoints = new List<Transform>();

    // Start is called before the first frame update

    private void Awake()
    {
        playerSwitching = GetComponent<PlayerSwitching>();
    }

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
        teleported = false;

        if (isAttackReady)
        {
            if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.A))
            {
                LacerateLeft();
            }

            else if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.D))
            {
                LacerateRight();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                NormalLaceration();
            }
        }

        if (isAbilityReady)
        {
            if (Input.GetButtonDown("Fire2") && spectralWarlock == null)
            {

                playerZeal.SpendZeal(abilityZealCost);
                if (playerZeal.canSpendZeal == true)
                {
                    ActivateAbility();
                }
                else
                {
                    Debug.Log("You've Run Out of Zeal! Wait to Regen!");
                }

            }
        }
        else if (Input.GetButtonDown("Fire2") && spectralWarlock != null)
        {
            teleportPos = spectralWarlock.GetComponent<SpectralWarlock>().transform.position;
            Teleport();
            teleported = true;
        }


        Debug.Log(isUltReady);
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
                        dontMove = true;
                        animator.SetTrigger("Ultimate");
                        //animator.SetTrigger("Attack");
                        //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                        //clonesSpawned = 0;
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

        // When player is dashing, create trail
        if (ignoreEnemyCollision)
        {
            // Trail
            if (timeBetweenTrail <= 0)
            {

                GameObject instance = (GameObject)Instantiate(trail, transform.position, transform.rotation);
                Destroy(instance, 0.4f);
                timeBetweenTrail = startTimeBetweenTrail;
            }
            else
            {
                timeBetweenTrail -= Time.deltaTime;
            }
        }
    }

    // Spectral Laceration
    public override void Attack()
    {
        //Play Attack Animation
        animator.SetTrigger("Attack");
        attackActivated = true;
    }

    public void NormalAttack()
    {
        damageDone = Random.Range(minAttackDamage, maxAttackDamage);
        //Play Attack Animation
        animator.SetTrigger("Attack");
        attackActivated = true;

        Collider2D enemyColInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);
        if (enemyColInfo != null)
        {
            Instantiate(impactEffect, attackPoint.position, attackPoint.rotation);
            enemyColInfo.GetComponentInParent<Enemy>().TakeDamage(damageDone);
        }
    }

    // Spectral Warlock
    public override void ActivateAbility()
    {
        abilityActivated = true;
        spectralWarlock = Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation) as GameObject;
    }

    // Spectral Barrage

    public void SpectralBarrage()
    {
        ultActivated = true;
        rb.velocity = new Vector2(0,0);
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
        //}
    }
    void Teleport()
    {
        transform.position = teleportPos;
        Destroy(spectralWarlock);
    }

    IEnumerator Dash(Vector2 moveDirection)
    {
        float speed = 500f;

        Vector2 targetVelocity = moveDirection * speed;

        //Find the change of velocity needed to reach target
        Vector2 velocityChange = targetVelocity - rb.velocity;

        //Convert to acceleration, which is change of velocity over time
        Vector2 acceleration = velocityChange / Time.fixedDeltaTime;

        //Clamp it to your maximum acceleration magnitude
        acceleration = Vector3.ClampMagnitude(acceleration, 1000);

        //Then AddForce
        rb.AddForce(acceleration, ForceMode2D.Impulse);

        ignoreEnemyCollision = true;
        Physics2D.IgnoreLayerCollision(7, 6, true); // Ignore col with enemies
        Physics2D.IgnoreLayerCollision(7, 14, true); // Ignore col with Bjorn's balls

        yield return new WaitForSeconds(0.4f);

        ignoreEnemyCollision = false;
        Physics2D.IgnoreLayerCollision(7, 6, false);
        Physics2D.IgnoreLayerCollision(7, 14, true);

    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Enemy") && ignoreEnemyCollision == true)
        {
            damageDone = Random.Range(minAttackDamage, maxAttackDamage);

            //impact VFX
            Instantiate(impactEffect, transform.position, transform.rotation);

            //damage popup
            GameObject damagePopup = Instantiate(damagePopupPrefab, otherCollider.transform.position, Quaternion.identity);
            DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
            damagePopupScript.Setup(damageDone);


            otherCollider.GetComponentInParent<Enemy>().TakeDamage(damageDone);
            playerZeal.AddOverzeal(damageDone);

            //old overzeal mechanic
            //if (playerZeal.isOverzealous == true)
            //{
            //    playerZeal.AddOverzeal(attackDamage);
            //}
        }
    }



    public void NormalLaceration()
    {
        NormalAttack();
    }

    public void LacerateLeft()
    {
        Attack();
        StartCoroutine(Dash(Vector2.left));
    }

    public void LacerateRight()
    {
        Attack();
        StartCoroutine(Dash(Vector2.right));
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
