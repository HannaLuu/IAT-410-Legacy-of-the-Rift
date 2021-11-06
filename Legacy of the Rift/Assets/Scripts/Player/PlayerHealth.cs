using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;

    public HealthBar healthBar;

    public GameObject gameOverUI;
    public GameObject dialogueUI;
    
    public static EventHandler OnDeath;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        //FindObjectOfType<AudioManager>().Play("BjornHit");

        if (currentHealth <= 0)
        {
            StartCoroutine(DelayGameOver());
            
        }
    }

    IEnumerator DelayGameOver() {
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        foreach (var e in enemies) {
            Destroy(e);
        }
        OnDeath?.Invoke(this,EventArgs.Empty);
        dialogueUI.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        Die();
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
