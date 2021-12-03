using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarbingerOfLife : MonoBehaviour
{
    public float abilityRange = 0.5f;
    public Transform abilityPoint;

    public float startHealTimer;
    private float healTimer;
    public float healAmount = 10;
    public LayerMask playerLayer;

    public float maxLifeSpan = 8f;
    public float currentLifeSpan;

    public Animator animator;

    public GameObject healingPopupPrefab;

    public bool isColliding;

    private Transform collisionPos;

    public GameObject smokeParticle;

    void Start()
    {
        currentLifeSpan = maxLifeSpan;
        healTimer = startHealTimer;
        isColliding = false;
        animator = gameObject.GetComponent<Animator>();
        Instantiate(smokeParticle, transform.position, transform.rotation);
    }

    void Update()
    {
        checkPlayerCollision();

        if (isColliding && healTimer <= 0)
        {
            healTimer = startHealTimer;
            Heal();
        }
        else
        {
            healTimer -= Time.deltaTime;
        }

        isColliding = false;

        currentLifeSpan -= Time.deltaTime;
        if (currentLifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(smokeParticle, transform.position, transform.rotation);
    }

    public void checkPlayerCollision()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(abilityPoint.position, abilityRange, playerLayer);
        if (colInfo != null)
        {
            isColliding = true;
            collisionPos = colInfo.transform;
        }
    }

    public void Heal()
    {
        GameObject healingPopup = Instantiate(healingPopupPrefab, collisionPos.transform.position, Quaternion.identity);
        HealingPopup healingPopupScript = healingPopup.GetComponent<HealingPopup>();
        healingPopupScript.Setup(healAmount);
        FindObjectOfType<PlayerHealth>().Heal(healAmount);
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
