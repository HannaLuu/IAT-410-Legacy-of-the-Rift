using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    //public float manaCost = 20;

    public Transform firePoint;
    public GameObject bulletPrefab;
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
    }

    //shooting logic
    void Shoot()
    {
        //manaBar.SpendMana1(manaCost);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
