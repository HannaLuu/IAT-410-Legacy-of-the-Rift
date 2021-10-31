using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LokirAttacks : AttackBaseClass
{
    // Start is called before the first frame update
    public Rigidbody2D rb;

    public int attackDamage = 100;
    public int overzealRegenAmount = 5;

    public int abilityZealCost = 25;

    public int ultClones = 3;
    private int clonesSpawned = 0;

    GameObject spectralWarlock = null;
    Vector3 teleportPos;

    public LayerMask enemyLayers;

    public PlayerZeal playerZeal;

    public bool ignoreEnemyCollision;

    public List<Transform> ultPoints = new List<Transform>();

    public static event EventHandler OnDashCollide;

    public bool teleported = false;

    //public static event EventHandler OnAttack;

    //public static event EventHandler OnAbility;

    //public static event EventHandler OnUltimate;

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
                if(playerZeal.canSpendZeal == true)
                {
                    ActivateAbility();
                } else
                {
                    Debug.Log("You've Run Out of Zeal! Wait to Regen!");
                }
                //animator.SetTrigger("Attack");
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
            } else if (Input.GetButtonDown("Fire2") && spectralWarlock != null)
            {
                teleportPos = spectralWarlock.GetComponent<SpectralWarlock>().transform.position;
                Teleport();
                teleported = true;
            }
        }

        if (isUltReady && isUltUnlocked)
        {
            if (playerZeal.fullyZealous == true)
            {
                if (Input.GetButtonDown("Fire3"))
                {
                    playerZeal.SpendZeal(ultZealCost);
                    if (playerZeal.canSpendZeal == true)
                    {
                        ActivateUlt();
                        //animator.SetTrigger("Attack");
                        //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                        clonesSpawned = 0;
                    } else
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
        //OnAttack?.Invoke(this, EventArgs.Empty);
        //Play Attack Animation
        animator.SetTrigger("Attack");
        attackActivated = true;
    }

    // Spectral Warlock
    public override void ActivateAbility()
    {
        //OnAbility?.Invoke(this, EventArgs.Empty);
        abilityActivated = true;
        spectralWarlock = Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation) as GameObject;
    }

    // Spectral Barrage
    public override void ActivateUlt()
    {
        //OnUltimate?.Invoke(this, EventArgs.Empty);
        ultActivated = true;
        foreach (Transform spawnPoint in ultPoints)
        {
            Instantiate(ultPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
    void Teleport()
    {
        transform.position = teleportPos;
        Destroy(spectralWarlock);
    }

    IEnumerator Dash(float distanceMultiplier)
    {
        float speed = 400f;

        rb.velocity = new Vector2(speed * distanceMultiplier, 0f);

        ignoreEnemyCollision = true;
        Physics2D.IgnoreLayerCollision(7, 6, true);


        yield return new WaitForSeconds(0.3f);


        ignoreEnemyCollision = false;
        Physics2D.IgnoreLayerCollision(7, 6, false);

    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Enemy") && ignoreEnemyCollision == true)
        {
            OnDashCollide?.Invoke(this, EventArgs.Empty);
            otherCollider.GetComponentInParent<Enemy>().TakeDamage(attackDamage);
            Debug.Log("DASH THROUGH DAMAGE");

            if (playerZeal.isOverzealous == true && playerZeal.currentZeal < playerZeal.maxZeal)
            {
                playerZeal.AddOverzeal(overzealRegenAmount);
            }

        }
    }



    public void NormalLaceration()
    {
        Attack();
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
}
