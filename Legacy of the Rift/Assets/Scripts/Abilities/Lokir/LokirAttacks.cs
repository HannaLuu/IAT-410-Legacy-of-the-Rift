using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LokirAttacks : AttackBaseClass
{
    // Start is called before the first frame update
    public Rigidbody2D rb;

    public float attackRange = 1f;
    public int attackDamage = 100;

    public int ultClones = 3;
    private int clonesSpawned = 0;

    GameObject spectralWarlock;
    Vector3 teleportPos;

    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        attackCooldown = 1f;
        isAttackReady = true;
        // nextAttackTime = 0f;

        abilityCooldown = 5f;
        isAbilityReady = true;
        // nextAbilityTime = 0f;

        ultCooldown = 10f;
        isUltReady = true;
        // nextUltTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttackReady) {
            if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.A))
            {
                LacerateLeft();
                StartCoroutine(BasicCooldown());
            }
            
            else if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.D))
            {
                LacerateRight();
                StartCoroutine(BasicCooldown());
            }
            
            else if (Input.GetButtonDown("Fire1"))
            {
                NormalLaceration();
                StartCoroutine(BasicCooldown());
            }
        }
        
        if (isAbilityReady) {
            if (Input.GetButtonDown("Fire2"))
            {
                ActivateAbility();
                //zealBar.SpendZeal1(zealCost);
                //animator.SetTrigger("Attack");
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                StartCoroutine(AbilityCooldown());
            }
        }
        
        if (isUltReady) {
            if (Input.GetButtonDown("Fire3"))
            {
                ActivateUlt();
                //zealBar.SpendZeal1(zealCost);
                //animator.SetTrigger("Attack");
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                clonesSpawned = 0;
                StartCoroutine(UltCooldown());
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
        
        // if (Time.time >= nextAttackTime)
        // {
        //     // if (Input.GetButtonDown("Fire1"))
        //     // {
        //
        //     // }
        //
        //     if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.A))
        //     {
        //
        //         //StartCoroutine(Dash(1f));
        //         LacerateLeft();
        //
        //     }
        //
        //     else if (Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.D))
        //     {
        //
        //         //StartCoroutine(Dash(-1f));
        //         LacerateRight();
        //
        //     }
        //
        //     else if (Input.GetButtonDown("Fire1"))
        //     {
        //         // Attack();
        //         // nextAttackTime = Time.time + 1f / attackRate;
        //         // Debug.Log("Attacking.");
        //         NormalLaceration();
        //     }
        // }
        //
        // if (Time.time >= nextAbilityTime)
        // {
        //     if (Input.GetButtonDown("Fire2"))
        //     {
        //         ActivateAbility();
        //         //zealBar.SpendZeal1(zealCost);
        //         //animator.SetTrigger("Attack");
        //         //FindObjectOfType<AudioManager>().Play("PlayerAttack");
        //         nextAbilityTime = Time.time + 1f / abilityRate;
        //     }
        // }
        //
        // if (Time.time >= nextUltTime)
        // {
        //     if (Input.GetButtonDown("Fire3"))
        //     {
        //         ActivateUlt();
        //         //zealBar.SpendZeal1(zealCost);
        //         //animator.SetTrigger("Attack");
        //         //FindObjectOfType<AudioManager>().Play("PlayerAttack");
        //         clonesSpawned = 0;
        //         nextUltTime = Time.time + 1f / ultRate;
        //     }
        // }
        //
        // if (spectralWarlock != null)
        // {
        //     teleportPos = spectralWarlock.GetComponent<SpectralWarlock>().transform.position;
        //     if (Input.GetKeyDown(KeyCode.LeftShift))
        //     {
        //         Teleport();
        //     }
        // }
    }

    private void FixedUpdate()
    {
        // if (!isDashing)
        // {
        //     rb.velocity = new Vector2(mx * speed, rb.velocity.y);
        // }
    }

    // Spectral Laceration
    public override void Attack()
    {
        //Play Attack Animation
        animator.SetTrigger("Attack");

        //Detect Enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("HIT");
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    // Spectral Warlock
    public override void ActivateAbility()
    {
        spectralWarlock = Instantiate(abilityPrefab, abilityPoint.position, abilityPoint.rotation) as GameObject;
    }

    // Spectral Barrage
    public override void ActivateUlt()
    {
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

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator Dash(float direction)
    {

        //isDashing = true;
        rb.velocity = new Vector2(400f * direction, 0f);


        yield return new WaitForSeconds(0.3f);

    }

    public void NormalLaceration()
    {
        Attack();
        // nextAttackTime = Time.time + 1f / attackCooldown;
        Debug.Log("Attacking.");
    }

    public void LacerateLeft()
    {
        Attack();
        // nextAttackTime = Time.time + 1f / attackCooldown;
        StartCoroutine(Dash(-1f));
        Debug.Log("Dashing Left.");
    }

    public void LacerateRight()
    {
        Attack();
        // nextAttackTime = Time.time + 1f / attackCooldown;
        StartCoroutine(Dash(1f));
        Debug.Log("Dashing Right.");
    }
}
