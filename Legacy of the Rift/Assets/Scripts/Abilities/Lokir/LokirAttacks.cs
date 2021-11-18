using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LokirAttacks : AttackBaseClass
{
    // Start is called before the first frame update
    public Rigidbody2D rb;

    public int attackDamage = 100;

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

    public Vector2 positionToMoveTo;


    //public float speed = 400f;
    //public float mx;
    private float dashTime;
    public float startDashTime;

    //old ult code
    //public int ultClones = 3;
    //private int clonesSpawned = 0;
    //public List<Transform> ultPoints = new List<Transform>();

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

            else if (Input.GetButtonDown("Fire1"))
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

                //animator.SetTrigger("Attack");
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
            }
        }
        else if (Input.GetButtonDown("Fire2") && spectralWarlock != null)
        {
            teleportPos = spectralWarlock.GetComponent<SpectralWarlock>().transform.position;
            Teleport();
            teleported = true;
        }

        if (isUltReady && isUltUnlocked)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                if (playerZeal.fullyZealous == true)
                {
                    playerZeal.SpendZeal(100);
                    playerZeal.SpendOverzeal(ultZealCost);
                    if (playerZeal.canSpendZeal == true)
                    {
                        ActivateUlt();
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
        //Play Attack Animation
        animator.SetTrigger("Attack");
        attackActivated = true;

        Collider2D enemyColInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);
        if (enemyColInfo != null)
        {
            Instantiate(impactEffect, attackPoint.position, attackPoint.rotation);
            enemyColInfo.GetComponentInParent<Enemy>().TakeDamage(attackDamage);
        }
    }

    // Spectral Warlock
    public override void ActivateAbility()
    {
        abilityActivated = true;
        spectralWarlock = Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation) as GameObject;
    }

    // Spectral Barrage
    public override void ActivateUlt()
    {
        ultActivated = true;
        Instantiate(lokirUltPrefab, lokirUltPoint.position, lokirUltPoint.rotation);
        Instantiate(halvarUltPrefab, halvarUltPointL.position, halvarUltPointL.rotation);
        Instantiate(halvarUltPrefab, halvarUltPointR.position, halvarUltPointR.rotation);
        Instantiate(ursaUltPrefab, ursaUltPoint.position, ursaUltPoint.rotation);
        FindObjectOfType<PlayerHealth>().Heal(100);

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

    // private void FixedUpdate()
    // {
    //     if (!isDashing)
    //     {
    //         rb.velocity = new Vector2(mx * speed, rb.velocity.y);
    //     }
    // }

    IEnumerator Dash(float distanceMultiplier)
    {
        float speed = 400f;

        //float newPosition = Mathf.SmoothDamp(transform.position.x, 0f, ref speed, 2);
        //transform.position = new Vector2(newPosition, 0f);

        //Vector2 destination;

        //Moves the GameObject from it's current position to destination over time
        //transform.position = Vector2.Lerp(transform.position, transform.position * distanceMultiplier, 20 * Time.deltaTime);

        //rb.velocity = new Vector2(speed * distanceMultiplier, 0f);
        //rb.AddForce(transform.forward * distanceMultiplier, ForceMode2D.Impulse);



        //rb.velocity = Vector2.Lerp(transform.position, transform.position.x * distanceMultiplier, speed * Time.deltaTime);
        //transform.position = 
        //rb.velocity = Vector2.LerpUnclamped
        //rb.AddForce(new Vector2(speed * distanceMultiplier, 0f), ForceMode2D.Impulse);
        //rb.velocity = Vector2.ClampMagnitude(rb.velocity, 100);
        //rb.AddForce = new Vector2(speed * distanceMultiplier, 0f), ForceMode2D.Impulse);


        rb.velocity = new Vector2(speed * distanceMultiplier, 0f);

        ignoreEnemyCollision = true;
        Physics2D.IgnoreLayerCollision(7, 6, true);


        yield return new WaitForSeconds(0.4f);


        rb.velocity = Vector2.zero;

        ignoreEnemyCollision = false;
        Physics2D.IgnoreLayerCollision(7, 6, false);

    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Enemy") && ignoreEnemyCollision == true)
        {
            //impact VFX
            Instantiate(impactEffect, transform.position, transform.rotation);

            //damage popup
            GameObject damagePopup = Instantiate(damagePopupPrefab, otherCollider.transform.position, Quaternion.identity);
            DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
            damagePopupScript.Setup(attackDamage);


            otherCollider.GetComponentInParent<Enemy>().TakeDamage(attackDamage);
            playerZeal.AddOverzeal(attackDamage);

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
        StartCoroutine(Dash(-1f));
    }

    public void LacerateRight()
    {
        Attack();
        StartCoroutine(Dash(1f));
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
