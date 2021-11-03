using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeath : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void OnCollisionEnter2D(Collision2D fallen)
    {
        if(fallen.gameObject.tag == "Enemy")
        {
            fallen.gameObject.GetComponentInParent<Enemy>().Die();
        }
        if (fallen.gameObject.tag == "Ursa" || fallen.gameObject.tag == "Lokir" || fallen.gameObject.tag == "Halvar")
        {
            playerHealth.Die();
        }
    }
}
