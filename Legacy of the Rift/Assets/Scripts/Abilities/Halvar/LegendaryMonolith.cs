using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendaryMonolith : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;

    public float maxLifeSpan = 8f;
    public float currentLifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentLifeSpan = maxLifeSpan;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("ROCK OUCH");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeSpan -= Time.deltaTime;
        if (currentLifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }
}
