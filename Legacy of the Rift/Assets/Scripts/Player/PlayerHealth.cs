using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;
    private PlayerSwitching playerSwitchScript;
    public Animator animator;

    public HealthBar healthBar;

    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerSwitchScript = GetComponent<PlayerSwitching>();
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (playerSwitchScript.currHero == PlayerSwitching.Hero.Lokir)
        {
            animator = playerSwitchScript.Lokir.GetComponent<Animator>();
            animator.SetTrigger("Hit");
        }
        if (playerSwitchScript.currHero == PlayerSwitching.Hero.Halvar)
        {
            animator = playerSwitchScript.Halvar.GetComponent<Animator>();
            animator.SetTrigger("Hit");
        }
        if (playerSwitchScript.currHero == PlayerSwitching.Hero.Ursa)
        {
            animator = playerSwitchScript.Ursa.GetComponent<Animator>();
            animator.SetTrigger("Hit");
        }
        healthBar.SetHealth(currentHealth);
        //FindObjectOfType<AudioManager>().Play("BjornHit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);
        //FindObjectOfType<AudioManager>().Play("BjornHit");
    }

    public void Die()
    {
        gameOverUI.SetActive(true);
    }
}
