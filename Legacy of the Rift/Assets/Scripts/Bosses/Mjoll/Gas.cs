using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    public float damage;

    public float maxLifeSpan;
    public float currentLifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        currentLifeSpan = maxLifeSpan;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Lokir") || collision.gameObject.CompareTag("Halvar") || collision.gameObject.CompareTag("Ursa"))
        {
            FindObjectOfType<PlayerHealth>().TakeDamage(damage);
        }
    }
}
