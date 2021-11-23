using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SW_Idle : StateMachineBehaviour
{

    Transform enemy;
    Rigidbody2D rb;
    SpectralWarlock spectralWarlock;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    private float timeBtwAttacks;
    public float startTimebtwAttacks;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spectralWarlock = animator.GetComponent<SpectralWarlock>();
        WaveSpawner waveSpawner = GameObject.FindObjectOfType<WaveSpawner>();
        if (waveSpawner != null && waveSpawner.EnemyIsAlive() == true)
        {
            enemy = spectralWarlock.nearestEnemy;
        }
        rb = animator.GetComponent<Rigidbody2D>();

        timeBtwAttacks = startTimebtwAttacks;
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

            Debug.Log("Enemies Detected.");

            enemy = spectralWarlock.nearestEnemy;
            spectralWarlock.LookAtEnemy();

            if (timeBtwAttacks <= 0)
            {
                timeBtwAttacks = startTimebtwAttacks;
                animator.SetTrigger("Attack");
            }
            else
            {
                timeBtwAttacks -= Time.deltaTime;
            }
        }

        if (waveSpawner == null)
        {
            Enemy enemyObj = GameObject.FindObjectOfType<Enemy>();
            if (enemyObj == null)
            {

            }
            if (enemyObj != null)
            {
                enemy = spectralWarlock.nearestEnemy;
                spectralWarlock.LookAtEnemy();

                if (timeBtwAttacks <= 0)
                {

                    timeBtwAttacks = startTimebtwAttacks;
                    animator.SetTrigger("Attack");
                }
                else
                {
                    timeBtwAttacks -= Time.deltaTime;
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
