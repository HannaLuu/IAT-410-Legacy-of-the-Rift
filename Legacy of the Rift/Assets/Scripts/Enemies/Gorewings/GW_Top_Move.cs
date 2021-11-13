using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GW_Top_Move : StateMachineBehaviour
{
    public float currSpeed;
    public float normalSpeed;
    public float slowedSpeed;

    public float chaseRange, attackRange;

    Transform player;
    Rigidbody2D rb;
    Enemy enemyScript;
    Transform returnToPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemyScript = animator.GetComponent<Enemy>();
        returnToPos = GameObject.FindGameObjectWithTag("GW_ReturnPoint").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.position, rb.position) <= chaseRange)
        {
            Vector2 target = new Vector2(player.position.x, player.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, currSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
                animator.SetTrigger("Attack");
            }
        } else
        {
            Vector2 target = new Vector2(returnToPos.position.x, returnToPos.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, currSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }

        if (enemyScript.isSlowed == true)
        {
            currSpeed = slowedSpeed;
        }

        else
        {
            currSpeed = normalSpeed;
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