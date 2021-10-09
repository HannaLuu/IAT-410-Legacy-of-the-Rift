using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralWarlock : MonoBehaviour
{
    public float maxLifeSpan = 18f;
    public float currentLifeSpan;

    public float attackRate = 4f;
    public float nextAttackTime = 0f;

    public GameObject attackPrefab;
    public Transform attackPoint;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentLifeSpan = maxLifeSpan;
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeSpan -= Time.deltaTime;

        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger("Attack");
            Attack();
            //FindObjectOfType<AudioManager>().Play("PlayerAttack");
            nextAttackTime = Time.time + 1f / attackRate;
        }

        if (currentLifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Attack()
    {
        //manaBar.SpendZeal1(zealCost);
        Instantiate(attackPrefab, attackPoint.position, attackPoint.rotation);
    }
}
