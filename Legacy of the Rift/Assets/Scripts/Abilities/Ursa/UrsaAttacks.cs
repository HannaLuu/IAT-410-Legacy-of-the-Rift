using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrsaAttacks : MonoBehaviour
{
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public float abilityRate = 4f;
    float nextAbilityTime = 0f;

    public float ultRate = 6f;
    float nextUltTime = 0f;

    //public float manaCost = 20;

    public Transform firePoint;
    public Transform wolfPoint;
    public Transform bearPoint;

    public GameObject bulletPrefab;
    public GameObject wolfPrefab;
    public GameObject bearPrefab;

    //public Teleport teleportScript;
    public Animator animator;
    //public ManaBar manaBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1") && manaBar.CanSpendMana(manaBar.manaAttackCost) == false)
        //{
        //    FindObjectOfType<AudioManager>().Play("OutOfMana");
        //}

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                //manaBar.SpendMana1(manaCost);
                animator.SetTrigger("Attack");
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Time.time >= nextAbilityTime)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Ability();
                //manaBar.SpendMana1(manaCost);
                //animator.SetTrigger("Attack");
                //FindObjectOfType<AudioManager>().Play("PlayerAttack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    //shooting logic
    void Shoot()
    {
        //manaBar.SpendMana1(manaCost);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void Ability()
    {
        //manaBar.SpendMana1(manaCost);
        Instantiate(wolfPrefab, wolfPoint.position, wolfPoint.rotation);
    }
    void Ult()
    {
        //manaBar.SpendMana1(manaCost);
        Instantiate(wolfPrefab, wolfPoint.position, wolfPoint.rotation);
    }
}
