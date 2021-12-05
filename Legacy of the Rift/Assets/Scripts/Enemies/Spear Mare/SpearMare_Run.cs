using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearMare_Run : StateMachineBehaviour
{
    public float currSpeed;
    public float normalSpeed;
    public float slowedSpeed;

    public float attackRange;
    public float meleeAttackRange;

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
        randomScript = animator.GetComponent<RandomSound>();
        source = animator.GetComponent<AudioSource>();
        // Physics2D.gravity = new Vector2(0, -100f);

        timeBtwAttacks = startTimeBtwAttacks;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyScript.LookAtPlayer();
        currSpeed = enemyScript.isSlowed ? slowedSpeed : normalSpeed;

        if (player.position.x < rb.position.x) rb.velocity = new Vector2(-currSpeed, rb.velocity.y);
        else rb.velocity = new Vector2(currSpeed, rb.velocity.y);

        GameObject legendaryMonolithObject = GameObject.FindGameObjectWithTag("Monolith");
        if (legendaryMonolithObject != null) {
            var distance = Vector2.Distance(legendaryMonolithObject.transform.position, rb.position);
            if (timeBtwAttacks <= 0 && distance > meleeAttackRange && distance <= attackRange)
            {
                animator.SetTrigger("Throw");
                timeBtwAttacks = startTimeBtwAttacks;
            } 
            else if (distance < meleeAttackRange) {
                source.clip = randomScript.GetRandomAudioClip();
                source.Play();
                animator.SetTrigger("Attack");
            }
            else if (timeBtwAttacks > 0) {
                timeBtwAttacks -= Time.deltaTime;
            }
        }

        var playerDistance = Vector2.Distance(player.position, rb.position);

        if (timeBtwAttacks <= 0 && playerDistance > meleeAttackRange && playerDistance <= attackRange)
        {
            animator.SetTrigger("Throw");
            timeBtwAttacks = startTimeBtwAttacks;
        } 
        else if (playerDistance < meleeAttackRange) {
            source.clip = randomScript.GetRandomAudioClip();
            source.Play();
            animator.SetTrigger("Attack");
        }
        else if (timeBtwAttacks > 0) {
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
