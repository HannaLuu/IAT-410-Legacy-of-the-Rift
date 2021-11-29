using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendaryMonolith : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;

    public float maxLifeSpan = 8f;
    public float currentLifeSpan;

    public float lostHealthOverTime = 0.5f;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentLifeSpan = maxLifeSpan;
        animator = this.GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DoneAnimation()
    {
        animator.SetBool("Spawned", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        currentHealth -= lostHealthOverTime;

        if(currentHealth <= (maxHealth * 0.75) && currentHealth > (maxHealth * 0.5))
        {
            animator.SetTrigger("is75");
        }

        if (currentHealth <= (maxHealth * 0.5) && currentHealth > (maxHealth * 0.25))
        {
            animator.SetTrigger("is50");
        }

        if (currentHealth <= (maxHealth * 0.25) && currentHealth > 0)
        {
            animator.SetTrigger("is25");
        }
    }
}
