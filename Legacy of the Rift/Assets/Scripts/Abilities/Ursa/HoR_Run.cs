using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoR_Run : StateMachineBehaviour
{
    public float speed = 2.5f;

    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        WaveSpawner waveSpawner = GameObject.FindObjectOfType<WaveSpawner>();
        if (waveSpawner != null && waveSpawner.EnemyIsAlive() == true)
        {
            Transform enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        }
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        WaveSpawner waveSpawner = GameObject.FindObjectOfType<WaveSpawner>();
        if (waveSpawner != null && waveSpawner.EnemyIsAlive() == false)
        {
            animator.SetBool("EnemyDetected", false);
        }

        if (waveSpawner != null && waveSpawner.EnemyIsAlive() == true)
        {
            Transform enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
            Vector2 target = new Vector2(enemy.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }

        if(waveSpawner == null)
        {
            Enemy enemyObj = GameObject.FindObjectOfType<Enemy>();
            if (enemyObj == null)
            {
                animator.SetBool("EnemyDetected", false);
            }
            if (enemyObj != null)
            {
                Transform enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
                Vector2 target = new Vector2(enemy.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }

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
