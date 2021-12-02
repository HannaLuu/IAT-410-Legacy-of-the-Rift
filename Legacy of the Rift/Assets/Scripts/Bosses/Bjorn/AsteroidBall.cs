using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBall : MonoBehaviour
{
    // public float speed = 20f;
    public float minSpeed;
    public float maxSpeed;

    public int damage = 100;

    public GameObject impactEffect;

    public GameObject damagePopupPrefab;

    public Transform target;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        rb.velocity = -transform.up * Random.Range(minSpeed, maxSpeed);
        // StartCoroutine(BallLifeTime());
    }

    // public static Quaternion LookAtTarget(Vector2 rotation)
    // {
    //     return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    // }

    IEnumerator BallLifeTime()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Lokir") || hitInfo.gameObject.CompareTag("Halvar") || hitInfo.gameObject.CompareTag("Ursa"))
        {
            FindObjectOfType<PlayerHealth>().TakeDamage(damage);
            Instantiate(impactEffect, transform.position, transform.rotation);

            //damage popup
            GameObject damagePopup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
            DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
            damagePopupScript.Setup(damage);

            Destroy(gameObject);
        }

        if (hitInfo.gameObject.CompareTag("Monolith"))
        {
            FindObjectOfType<LegendaryMonolith>().TakeDamage(damage);
            Instantiate(impactEffect, transform.position, transform.rotation);

            //damage popup
            GameObject damagePopup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
            DamagePopup damagePopupScript = damagePopup.GetComponent<DamagePopup>();
            damagePopupScript.Setup(damage);

            Destroy(gameObject);
        }

        if (hitInfo.gameObject.CompareTag("Ground"))
        {
            enabled = false;
            Destroy(gameObject);
        }
    }



}
