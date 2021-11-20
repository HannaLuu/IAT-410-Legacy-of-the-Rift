using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 100;

    public GameObject impactEffect;

    public GameObject damagePopupPrefab;

    //Target Way
    //private Transform player;
    //private Vector2 target;

    public GameObject target;
    private float startX;
    private float startY;
    private float targetX;
    private float targetY;
    private float dist;
    private float nextX;
    private float baseY;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        target = GameObject.FindGameObjectWithTag("Player");
        targetX = target.transform.position.x;
        targetY = target.transform.position.y;
    }

    private void Update()
    {
        dist = targetX - startX;
        nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
        baseY = Mathf.Lerp(startY, targetY, (nextX - startX) / dist);
        height = 2 * (nextX - startX) * (nextX - targetX) / (-0.25f * dist * dist);

        Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;

        if (transform.position.x == targetX)
        {
            Destroy(gameObject);
        }
    }

    public static Quaternion LookAtTarget(Vector2 rotation)
    {
        return Quaternion.Euler(0,0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
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

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
