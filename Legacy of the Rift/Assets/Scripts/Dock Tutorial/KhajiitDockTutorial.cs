using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class KhajiitDockTutorial : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public Flowchart flowchart;
    //public PlayerSwitching playerSwitching;

    public GameObject interactKey;
    public GameObject textPanel;
    public GameObject choicePanel;

    public Gate1Tutorial gate1;

    public GameObject waveManager, waveSpawnPoints;

    public Transform player;

    private bool isFlipped = false;

    private bool canInteract = false;

    private bool once, stinkey, omg;

    private bool firstEnemyKilled = false;

    void Start()
    {
        interactKey.SetActive(false);
        waveManager.SetActive(false);
        waveSpawnPoints.SetActive(false);
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //playerSwitching = GameObject.FindGameObjectWithTag("PlayerHandler").GetComponent<PlayerSwitching>();
        canInteract = false;
        once = false;
        firstEnemyKilled = false;
        stinkey = false;
        omg = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (flowchart.GetBooleanVariable("Tutorial") == false && flowchart.GetBooleanVariable("lol") == false)
        {
            LookAtPlayer();
        }

        // Player Skips Tutorial
        if (flowchart.GetBooleanVariable("Tutorial") == false && flowchart.GetBooleanVariable("lol") == true)
        {
            LookOppositePlayer();
            Vector2 target = new Vector2(-79.93f, rb.transform.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.transform.position, target, 7 * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            animator.SetBool("isRun", true);
            if (Vector2.Distance(rb.position, target) <= 0)
            {
                animator.SetBool("isRun", false);
            }
            if (once == false)
            {
                waveSpawnPoints.SetActive(true);
                waveManager.SetActive(true);
                once = true;
            }
        }

        //Player Wants Tutorial
        if (flowchart.GetBooleanVariable("Tutorial") == true)
        {
            LookOppositePlayer();
            Vector2 target = new Vector2(-79.93f, rb.transform.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.transform.position, target, 7 * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            animator.SetBool("isRun", true);
            if (Vector2.Distance(rb.position, target) <= 0)
            {
                animator.SetBool("isRun", false);
            }
            if (stinkey == false)
            {
                waveSpawnPoints.SetActive(true);
                waveManager.SetActive(true);
                stinkey = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Lokir" || player.tag == "Halvar" || player.tag == "Ursa")
        {
            interactKey.SetActive(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.tag == "Lokir" || player.tag == "Halvar" || player.tag == "Ursa")
        {
            interactKey.SetActive(false);
            canInteract = false;
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        textPanel.transform.rotation = Quaternion.identity;
        choicePanel.transform.rotation = Quaternion.identity;
        interactKey.transform.rotation = Quaternion.identity;
    }

    public void LookOppositePlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        textPanel.transform.rotation = Quaternion.identity;
        choicePanel.transform.rotation = Quaternion.identity;
        interactKey.transform.rotation = Quaternion.identity;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
