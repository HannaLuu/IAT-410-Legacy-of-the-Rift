using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mjoll_AvoidLeftWall : StateMachineBehaviour
{
    GameObject leftWall;
    Rigidbody2D rb;
    Enemy enemyScript;

    Transform player;

    public float speed = 5f;
    public float wallSpace = 10f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftWall = GameObject.FindGameObjectWithTag("LeftWall");
        enemyScript = animator.GetComponent<Enemy>();
        rb = animator.GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player.position.x > rb.position.x)
        {
            enemyScript.LookAtPlayer();
        }
        else
        {
            enemyScript.LookOppositePlayer();
        }

        if (Vector2.Distance(rb.position, leftWall.transform.position) <= wallSpace)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            animator.SetBool("avoidLeftWall", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
