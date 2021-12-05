using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBall : MonoBehaviour
{
    public float speed = 30f;
    public int damage = 100;

    public GameObject impactEffect;

    public GameObject damagePopupPrefab;

    public Transform target;

    private Rigidbody2D rb;

    public RandomSound randomHomingImpactSound;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * 200;

        rb.velocity = transform.up * speed;
    }

    private void Update()
    {
        // dist = targetX - startX;
        // nextX = Mathf.MoveTowards(targetX, targetY, speed * Time.deltaTime);
        // // baseY = Mathf.Lerp(startY, targetY, (nextX - startX) / dist);
        // // height = 5 * (nextX - startX) * (nextX - targetX) / (-0.25f * dist * dist);

        // Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
        // transform.rotation = LookAtTarget(movePosition - transform.position);
        // transform.position = movePosition;

        // if (transform.position.x == target)
        // {
        //     Destroy(gameObject);
        // }
        StartCoroutine(BallLifeTime());

    }

    public static Quaternion LookAtTarget(Vector2 rotation)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
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

            //play impact sound
            source.clip = randomHomingImpactSound.GetRandomAudioClip();
            source.Play();

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

            //play impact sound
            source.clip = randomHomingImpactSound.GetRandomAudioClip();
            source.Play();

            Destroy(gameObject);
        }

        if (hitInfo.gameObject.CompareTag("Ground"))
        {
            //play impact sound
            source.clip = randomHomingImpactSound.GetRandomAudioClip();
            source.Play();

            enabled = false;
            Destroy(gameObject);
        }
    }

    IEnumerator BallLifeTime()
    {
        yield return new WaitForSeconds(3f);
        //play impact sound
        source.clip = randomHomingImpactSound.GetRandomAudioClip();
        source.Play();

        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }


}
