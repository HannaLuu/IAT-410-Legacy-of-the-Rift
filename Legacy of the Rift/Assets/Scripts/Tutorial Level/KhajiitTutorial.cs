using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class KhajiitTutorial : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public Flowchart flowchart;
    public PlayerSwitching playerSwitching;

    public GameObject interactKey;
    public GameObject textPanel;
    public GameObject choicePanel;

    public Gate1Tutorial gate1;

    public GameObject waveManager, waveSpawnPoints;

    public Transform player;

    private bool isFlipped = false;

    private bool canInteract = false;

    private bool once, stinkey, omg, zzz;

    private bool firstEnemyKilled = false;

    public GameObject kids;

    public GameObject enemiesLeftText, wavesCompleteText;

    void Start()
    {
        interactKey.SetActive(false);
        waveManager.SetActive(false);
        waveSpawnPoints.SetActive(false);
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerSwitching = GameObject.FindGameObjectWithTag("PlayerHandler").GetComponent<PlayerSwitching>();
        canInteract = false;
        once = false;
        firstEnemyKilled = false;
        stinkey = false;
        omg = false;
        zzz = false;

        enemiesLeftText.SetActive(true);
        wavesCompleteText.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(waveManager.GetComponent<WaveSpawner>().allWavesCompleted == true)
        {
            enemiesLeftText.SetActive(false);
            wavesCompleteText.SetActive(true);
        }

        if (waveManager.GetComponent<WaveSpawner>().allWavesCompleted == false)
        {
            enemiesLeftText.SetActive(true);
            wavesCompleteText.SetActive(false);
        }

        if (flowchart.GetBooleanVariable("Tutorial") == false && flowchart.GetBooleanVariable("lol") == false)
        {
            LookAtPlayer();
        }

        // Player Skips Tutorial
        if (flowchart.GetBooleanVariable("Tutorial") == false && flowchart.GetBooleanVariable("lol") == true)
        {
            if (zzz == false)
            {
                LookOppositePlayer();
            }
            if (zzz == true)
            {
                LookAtPlayer();
            }
            Vector2 target = new Vector2(97.16f, rb.transform.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.transform.position, target, 6 * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            animator.SetBool("isRun", true);
            if (Vector2.Distance(rb.position, target) <= 0)
            {
                zzz = true;
                animator.SetBool("isRun", false);
            }

            if (gate1.pastGate1 && once == false)
            {
                waveSpawnPoints.SetActive(true);
                waveManager.SetActive(true);
                kids.SetActive(true);
                once = true;
            }

            if (kids.activeSelf == true)
            {
                flowchart.SetBooleanVariable("pickedUpKids", false);
            }
            if (kids.activeSelf == false)
            {
                flowchart.SetBooleanVariable("pickedUpKids", true);
            }

            if (Input.GetKeyDown(KeyCode.F) && canInteract)
            {
                flowchart.ExecuteBlock("F On Door to Proceed");
            }
        }

        //Player Wants Tutorial
        if (flowchart.GetBooleanVariable("Tutorial") == true)
        {
            LookAtPlayer();
            Vector2 target = new Vector2(-21.99f, rb.transform.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.transform.position, target, 7 * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            animator.SetBool("isRun", true);
            if (Vector2.Distance(rb.position, target) <= 0)
            {
                animator.SetBool("isRun", false);
                if(playerSwitching.currHero == PlayerSwitching.Hero.Lokir)
                {
                    flowchart.ExecuteBlock("Lokir Hint 2");
                }

                if (flowchart.GetBooleanVariable("SpawnTutorialEnemy") == true && once == false)
                {
                    waveSpawnPoints.SetActive(true);
                    waveManager.SetActive(true);
                    once = true;
                }
                if (waveManager.GetComponent<WaveSpawner>().nextWave == 1 && waveManager.GetComponent<WaveSpawner>().EnemyIsAlive() == false)
                {
                    waveManager.SetActive(false);
                    firstEnemyKilled = true;
                }
                if (firstEnemyKilled == true && playerSwitching.currHero != PlayerSwitching.Hero.Ursa)
                {
                    flowchart.ExecuteBlock("Hint 3");
                }
            }

            if (flowchart.GetBooleanVariable("Section3") == true)
            {
                Vector2 target2 = new Vector2(1.2f, rb.transform.position.y);
                Vector2 newPos2 = Vector2.MoveTowards(rb.transform.position, target2, 7 * Time.fixedDeltaTime);
                rb.MovePosition(newPos2);
                animator.SetBool("isRun", true);
                if (Vector2.Distance(rb.position, target2) <= 0)
                {
                    animator.SetBool("isRun", false);
                    if(!stinkey)
                    {
                        waveManager.SetActive(true);
                        flowchart.ExecuteBlock("Hint 4");
                        stinkey = true;
                    }
                    if(stinkey && waveManager.activeSelf == false && omg == false)
                    {
                        flowchart.ExecuteBlock("Hint 5");
                        kids.SetActive(true);
                        omg = true;
                    }
                }
            }

            if(flowchart.GetBooleanVariable("WaitByDoor") == true)
            {
                Vector2 target2 = new Vector2(97.16f, rb.transform.position.y);
                Vector2 newPos2 = Vector2.MoveTowards(rb.transform.position, target2, 8 * Time.fixedDeltaTime);
                rb.MovePosition(newPos2);
                animator.SetBool("isRun", true);
                if (Vector2.Distance(rb.position, target2) <= 0)
                {
                    animator.SetBool("isRun", false);
                }
                if (kids.activeSelf == true)
                {
                    flowchart.SetBooleanVariable("pickedUpKids", false);
                }
                if (kids.activeSelf == false)
                {
                    flowchart.SetBooleanVariable("pickedUpKids", true);
                }
                if (Input.GetKeyDown(KeyCode.F) && canInteract)
                {
                    flowchart.ExecuteBlock("F On Door to Proceed");
                }
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
