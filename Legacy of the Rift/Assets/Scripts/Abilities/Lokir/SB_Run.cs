using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SB_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 3f;

    Transform enemy;
    Rigidbody2D rb;
    SpectralBarrage spectralBarrage;

    // SpectralBarrage clones;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spectralBarrage = animator.GetComponent<SpectralBarrage>();
        WaveSpawner waveSpawner = GameObject.FindObjectOfType<WaveSpawner>();
        if (waveSpawner != null && waveSpawner.EnemyIsAlive() == true)
        {
            spectralBarrage.FindNearestEnemy();
            enemy = spectralBarrage.nearestEnemy;
            spectralBarrage.LookAtEnemies();
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
            spectralBarrage.FindNearestEnemy();
            enemy = spectralBarrage.nearestEnemy;
            spectralBarrage.LookAtEnemies();

            if (enemy.position.x < rb.position.x)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }

            if (Vector2.Distance(enemy.position, rb.position) <= attackRange)
            {
                animator.SetTrigger("Attack");
            }
        }

        if (waveSpawner == null)
        {
            Enemy enemyObj = GameObject.FindObjectOfType<Enemy>();
            if (enemyObj == null)
            {
                animator.SetBool("EnemyDetected", false);
            }
            if (enemyObj != null)
            {
                spectralBarrage.FindNearestEnemy();
                enemy = spectralBarrage.nearestEnemy;
                spectralBarrage.LookAtEnemies();

                if (enemy.position.x < rb.position.x)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }

                if (Vector2.Distance(enemy.position, rb.position) <= attackRange)
                {
                    animator.SetTrigger("Attack");
                }
            }
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
