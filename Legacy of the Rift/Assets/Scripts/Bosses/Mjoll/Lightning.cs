using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lokir") || collision.gameObject.CompareTag("Halvar") || collision.gameObject.CompareTag("Ursa"))
        {
            FindObjectOfType<PlayerHealth>().TakeDamage(damage);
        }
    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
