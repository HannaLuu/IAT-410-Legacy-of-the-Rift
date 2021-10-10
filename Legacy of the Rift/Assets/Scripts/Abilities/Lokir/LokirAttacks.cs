using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LokirAttacks : AttackBaseClass
{
    // Start is called before the first frame update
    private Rigidbody2D rb;

    public float attackRange = 1f;
    public int attackDamage = 100;

    GameObject spectralWarlock;
    Vector3 teleportPos;

    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        attackRate = 2f;
        nextAttackTime = 0f;

        abilityRate = 20f;
        nextAbilityTime = 0f;

        ultRate = 25f;
        nextUltTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Time.time >= nextAbilityTime)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                ActivateAbility();
                //zealBar.SpendZeal1(zealCost);
                //animator.SetTrigger("Attack");
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                nextAbilityTime = Time.time + 1f / abilityRate;
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
        Debug.Log("No Spectral Barrage ability Yet!");
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
}