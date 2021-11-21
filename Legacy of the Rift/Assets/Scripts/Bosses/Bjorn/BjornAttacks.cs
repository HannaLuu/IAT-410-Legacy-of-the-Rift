using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BjornAttacks : Enemy
{

    public int health;

    public Transform[] tpPoints;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("Teleport", 0, 10); //calls ChangePosition() every 10 secs
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }

    void Teleport()
    {
        Transform _sp = tpPoints[Random.Range(0, tpPoints.Length)];

        Vector2 target = new Vector2(_sp.position.x, _sp.position.y);

        Vector2 newPos = Vector2.MoveTowards(_sp.position, target, Time.deltaTime);

        rb.MovePosition(newPos);
    }


}
