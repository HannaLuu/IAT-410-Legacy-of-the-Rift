using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendaryMonolith : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;

    public float maxLifeSpan = 8f;
    public float currentLifeSpan;

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
        currentLifeSpan -= Time.deltaTime;
        if (currentLifeSpan <= 0)
        {
            Destroy(gameObject);
        }

        if(currentHealth <= 75 && currentHealth > 50)
        {
            animator.SetTrigger("is75");
        }

        if (currentHealth <= 50 && currentHealth > 25)
        {
            animator.SetTrigger("is50");
        }

        if (currentHealth <= 25 && currentHealth > 0)
        {
            animator.SetTrigger("is25");
        }
    }
}
