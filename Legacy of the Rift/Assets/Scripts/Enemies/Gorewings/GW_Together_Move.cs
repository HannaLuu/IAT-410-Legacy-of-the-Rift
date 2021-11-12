using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GW_Together_Move : StateMachineBehaviour
{
    public float currSpeed;
    public float normalSpeed;
    public float slowedSpeed;

    public float dropRange;

    Transform player;
    Rigidbody2D rb;
    Enemy enemyScript;

    Transform waypoint1, waypoint2, currWaypoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waypoint1 = GameObject.FindGameObjectWithTag("GW_MovePoint1").transform;
        waypoint2 = GameObject.FindGameObjectWithTag("GW_MovePoint2").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemyScript = animator.GetComponent<Enemy>();

        currWaypoint = waypoint1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (currWaypoint == waypoint1)
        {
            Vector2 target = new Vector2(waypoint1.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, currSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            if(rb.position.x == waypoint1.position.x)
            {
                currWaypoint = waypoint2;
            }
        }
        if (currWaypoint == waypoint2)
        {
            Vector2 target = new Vector2(waypoint2.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, currSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            if (rb.position.x == waypoint2.position.x)
            {
                currWaypoint = waypoint1;
            }
        }

        if (Vector2.Distance(player.position, rb.position) <= dropRange)
        {
            animator.SetBool("drop", true);
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
