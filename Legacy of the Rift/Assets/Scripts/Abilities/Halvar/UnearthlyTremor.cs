using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnearthlyTremor : MonoBehaviour
{
    public Rigidbody2D rb;

    public int damage = 100;

    public GameObject impactEffect;
    public GameObject damagePopupPrefab;

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            hitInfo.GetComponentInParent<Enemy>().TakeDamage(damage);
            hitInfo.GetComponentInParent<Enemy>().SlowMe();

            //damage popup
            GameObject damagePopup = Instantiate(damagePopupPrefab, hitInfo.transform.position, Quaternion.identity);
            DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
            damagePopupScript.Setup(damage);

            Instantiate(impactEffect, hitInfo.gameObject.transform.position, hitInfo.gameObject.transform.rotation);
        }
    }
}
