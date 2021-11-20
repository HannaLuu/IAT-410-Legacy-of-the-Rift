using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public float speed = 20f;
    //public int damage = 100;

    public int minAttackDamage;
    public int maxAttackDamage;
    public int damageDone;


    public float slowAmount = 4f;
    float slowTime = 0f;

    public Rigidbody2D rb;

    public PlayerZeal playerZeal;

    public UrsaAttacks ursaAttackScript;

    public GameObject impactEffect;
    public GameObject damagePopupPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ursaAttackScript = GameObject.FindGameObjectWithTag("Ursa").GetComponent<UrsaAttacks>();
        playerZeal = GameObject.FindGameObjectWithTag("PlayerHandler").GetComponent<PlayerZeal>();
        currentHealth = maxHealth;
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            damageDone = Random.Range(minAttackDamage, maxAttackDamage);
            hitInfo.GetComponentInParent<Enemy>().TakeDamage(damageDone);
            hitInfo.GetComponentInParent<Enemy>().SlowMe();

            //imapct VFX
            Instantiate(impactEffect, transform.position, transform.rotation);

            //damage popup
            GameObject damagePopup = Instantiate(damagePopupPrefab, hitInfo.transform.position, Quaternion.identity);
            DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
            damagePopupScript.Setup(damageDone);

            currentHealth -= 1;
            playerZeal.AddOverzeal(damageDone);

            //old overzeal mechanic
            //if (playerZeal.isOverzealous == true)
            //{
            //    playerZeal.AddOverzeal(ursaAttackScript.overzealRegenAmount);
            //}
        }
        if (hitInfo.gameObject.CompareTag("Ground"))
        {
            enabled = false;
            Destroy(gameObject);
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);
    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
