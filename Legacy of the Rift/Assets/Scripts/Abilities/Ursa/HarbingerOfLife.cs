using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarbingerOfLife : MonoBehaviour
{
    public float abilityRange = 0.5f;
    public Transform abilityPoint;
    public float healAmount = 10;
    public LayerMask playerLayer;

    public int maxLifeSpan = 1000;
    public int currentLifeSpan;

    void Start()
    {
        currentLifeSpan = maxLifeSpan;
    }

    void Update()
    {
        Heal();
        currentLifeSpan -= 1;
        if(currentLifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(abilityPoint.position, abilityRange, playerLayer);
        if (colInfo != null)
        {
            Debug.Log("Healing");
            FindObjectOfType<PlayerHealth>().Heal(healAmount);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (abilityPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(abilityPoint.position, abilityRange);
    }
}
