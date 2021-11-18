using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Run : StateMachineBehaviour
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

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemyScript = animator.GetComponent<Enemy>();
        // Physics2D.gravity = new Vector2(0, -100f);

        timeBtwAttacks = startTimeBtwAttacks;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyScript.LookAtPlayer();

        // Vector2 target = new Vector2(player.position.x, rb.position.y);
        // Vector2 newPos = Vector2.MoveTowards(rb.position, target, currSpeed * Time.fixedDeltaTime);
        // newPos = new Vector2(newPos.x, 0);
        // rb.MovePosition(newPos);

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
        if (delay == true && timeBtwAttacks <= 0)
        {
            animator.SetTrigger("Attack");
        } else
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
