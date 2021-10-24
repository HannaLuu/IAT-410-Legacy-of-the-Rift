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

    GameObject spectralWarlock;
    Vector3 teleportPos;

    public LayerMask enemyLayers;

    public PlayerZeal playerZeal;

    public CooldownBar abilityCooldownBar;
    public CooldownBar ultCooldownBar;

    public bool ignoreEnemyCollision;




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
            if (playerZeal.isOverzealous == true)
            {
                if (Input.GetButtonDown("Fire3"))
                {
                    ActivateUlt();
                    playerZeal.SpendZeal(ultZealCost);
                    //animator.SetTrigger("Attack");
                    //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                    clonesSpawned = 0;
                }
            }
        }

        if (spectralWarlock != null)
        {
            teleportPos = spectralWarlock.GetComponent<SpectralWarlock>().transform.position;
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Teleport();
            }
        }

    }

    // Spectral Laceration
    public override void Attack()
    {
        //Play Attack Animation
        animator.SetTrigger("Attack");

        StartCoroutine(BasicCooldown());
    }

    // Spectral Warlock
    public override void ActivateAbility()
    {
        spectralWarlock = Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation) as GameObject;
        StartCoroutine(AbilityCooldown());
    }

    // Spectral Barrage
    public override void ActivateUlt()
    {
        StartCoroutine(UltCooldown());
        while (clonesSpawned < 3)
        {
            clonesSpawned += 1;
            Instantiate(ultPrefab, ultPoint.position, ultPoint.rotation);
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

        yield return new WaitForSeconds(0.3f);

        ignoreEnemyCollision = false;

    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Enemy") && ignoreEnemyCollision == true)
        {
            otherCollider.GetComponentInParent<Enemy>().TakeDamage(attackDamage);
            //otherCollider.GetComponent<Enemy>().TakeDamage(attackDamage);
            if (playerZeal.isOverzealous == true)
            {
                playerZeal.AddOverzeal(overzealRegenAmount);
            }
            Physics2D.IgnoreLayerCollision(7, 6, true);

        }
        else if (otherCollider.gameObject.CompareTag("Enemy") && ignoreEnemyCollision == false)
        {
            Physics2D.IgnoreLayerCollision(7, 6, false);
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
