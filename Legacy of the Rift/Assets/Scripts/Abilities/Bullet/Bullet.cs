using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 100;

    public Rigidbody2D rb;

    public GameObject impactEffect;
    public GameObject damagePopupPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            hitInfo.GetComponentInParent<Enemy>().TakeDamage(damage);

            //damage popup
            GameObject damagePopup = Instantiate(damagePopupPrefab, hitInfo.transform.position, Quaternion.identity);
            DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
            damagePopupScript.Setup(damage);

            FindObjectOfType<PlayerZeal>().AddOverzeal(damage);
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
