using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    public float damage;

    public float maxLifeSpan;
    public float currentLifeSpan;

    public float startDamageTimer;
    private float damageTimer;

    public GameObject damagePopupPrefab;

    public bool isColliding;

    private Transform collisionPos;

    // Start is called before the first frame update
    void Start()
    {
        currentLifeSpan = maxLifeSpan;
        damageTimer = startDamageTimer;
        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeSpan -= Time.deltaTime;
        if (currentLifeSpan <= 0)
        {
            Destroy(gameObject);
        }

        if (isColliding && damageTimer <= 0)
        {
            damageTimer = startDamageTimer;
        } else
        {
            damageTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Lokir") || collision.gameObject.CompareTag("Halvar") || collision.gameObject.CompareTag("Ursa"))
        {
            isColliding = true;
            collisionPos = collision.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lokir") || collision.gameObject.CompareTag("Halvar") || collision.gameObject.CompareTag("Ursa"))
        {
            if (damageTimer <= 0)
            {
                GameObject damagePopup = Instantiate(damagePopupPrefab, collision.transform.position, Quaternion.identity);
                DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
                damagePopupScript.Setup(damage);
                FindObjectOfType<PlayerHealth>().TakeDamage(damage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lokir") || collision.gameObject.CompareTag("Halvar") || collision.gameObject.CompareTag("Ursa"))
        {
            isColliding = false;
        }
    }
}
