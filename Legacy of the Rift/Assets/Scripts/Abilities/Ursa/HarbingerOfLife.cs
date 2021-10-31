using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarbingerOfLife : MonoBehaviour
{
    public float abilityRange = 0.5f;
    public Transform abilityPoint;
    public float healAmount = 10;
    public LayerMask playerLayer;

    public float maxLifeSpan = 8f;
    public float currentLifeSpan;

    public Animator animator;

    void Start()
    {
        currentLifeSpan = maxLifeSpan;
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Heal();
        currentLifeSpan -= Time.deltaTime;
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

    public void DoneAnimation()
    {
        animator.SetBool("Idle", true);
    }
}
