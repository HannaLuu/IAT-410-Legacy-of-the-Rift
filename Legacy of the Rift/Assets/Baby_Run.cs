using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby_Run : StateMachineBehaviour
{
    public float currSpeed;
    public float normalSpeed;
    public float slowedSpeed;

    public float attackRange;

    public bool delay = false;
    private float timeBtwAttacks;
    public float startTimeBtwAttacks;

    Transform player;
    Rigidbody2D rb;
    Enemy enemyScript;

    RandomSound randomScript;
    AudioSource source;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemyScript = animator.GetComponent<Enemy>();
        // Physics2D.gravity = new Vector2(0, -100f);
        randomScript = animator.GetComponent<RandomSound>();
        source = animator.GetComponent<AudioSource>();

        timeBtwAttacks = startTimeBtwAttacks;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (source.isPlaying == false)
        {
            source.clip = randomScript.GetRandomAudioClip();
            source.loop = false;
            source.Play();
        }

        enemyScript.LookAtPlayer();

        if (player.position.x < rb.position.x)
        {
            rb.velocity = new Vector2(-currSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(currSpeed, rb.velocity.y);
        }

        GameObject legendaryMonolithObject = GameObject.FindGameObjectWithTag("Monolith");
        if (legendaryMonolithObject != null)
        {
            if (delay == false && Vector2.Distance(legendaryMonolithObject.transform.position, rb.position) <= attackRange)
            {
                animator.SetTrigger("Attack");
            }
        }

        if (delay == false && Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }

        if (enemyScript.isSlowed == true)
        {
            currSpeed = slowedSpeed;
        }

        else
        {
            currSpeed = normalSpeed;
        }

        //Time Between Attack animations code if needed
        if (delay == true && timeBtwAttacks <= 0 && Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            timeBtwAttacks = startTimeBtwAttacks;
            animator.SetTrigger("Attack");
        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
