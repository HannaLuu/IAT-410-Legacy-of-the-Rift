using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralWarlock : MonoBehaviour
{
    public float maxLifeSpan = 18f;
    public float currentLifeSpan;

    public float attackRate = 4f;
    public float nextAttackTime = 0f;

    public GameObject attackPrefab;
    public Transform attackPoint;
    public Animator animator;
    public SpriteRenderer sr;

    public Transform nearestEnemy;
    public ParticleSystem pSystem;

    private List<Transform> enemies = new List<Transform>();

    Vector2 vectorToEnemy;

    private Vector2 offsetPosition;
    private LokirAttacks _lokirAttacks;


    public bool isFlipped = false;

    // Start is called before the first frame update
    void Start() {
        _lokirAttacks = GameObject.FindGameObjectWithTag("Lokir").GetComponentInParent<LokirAttacks>();
        currentLifeSpan = maxLifeSpan;
        //_lokirAttacks.OnTeleport += SelfDestructSubscriber;
        if(_lokirAttacks == null)
        {
            Debug.Log("FUCK ME ARSEHOLE");
        }
    }

    public void SelfDestructSubscriber(object sender, EventArgs e) {
        StartCoroutine(SelfDestruct());
    }

    private IEnumerator SelfDestruct() {
        pSystem.Play();
        sr.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(pSystem.main.duration);
        if (_lokirAttacks.OnTeleport != null) _lokirAttacks.OnTeleport -= SelfDestructSubscriber;
        Destroy(gameObject);
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
    public void AttackEnemy()
    {
        if (nearestEnemy == null)
            return;
        GameObject attackObject = Instantiate(attackPrefab, attackPoint.position, attackPoint.rotation);


        vectorToEnemy = VectorHelper.GetVectorToPoint(transform.position, nearestEnemy.position);

        attackObject.GetComponent<SWProjectile>().SetDirection(vectorToEnemy);
    }

    public void LookAtEnemy()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > nearestEnemy.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (transform.position.x < nearestEnemy.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }

    public void FindNearestEnemy()
    {
        float nearestEnemyDistance = float.MaxValue;

        Enemy[] enemyArray = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemyArray)
        {
            enemies.Add(enemy.transform);
        }

        foreach (Transform enemy in enemies)
        {
            float currEnemyDistance;


            if (enemy != null)
            {
                currEnemyDistance = Vector2.Distance(transform.position, enemy.position);
                if (nearestEnemyDistance > currEnemyDistance)
                {
                    nearestEnemyDistance = currEnemyDistance;
                    nearestEnemy = enemy;
                }
            }
        }
    }
}
