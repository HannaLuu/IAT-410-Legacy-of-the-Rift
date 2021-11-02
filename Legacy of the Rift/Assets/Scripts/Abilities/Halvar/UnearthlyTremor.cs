using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnearthlyTremor : MonoBehaviour
{
    public Rigidbody2D rb;

    public int damage = 100;

    public GameObject impactEffect;

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            hitInfo.GetComponentInParent<Enemy>().TakeDamage(damage);
            Instantiate(impactEffect, hitInfo.gameObject.transform.position, hitInfo.gameObject.transform.rotation);
        }
    }
}
