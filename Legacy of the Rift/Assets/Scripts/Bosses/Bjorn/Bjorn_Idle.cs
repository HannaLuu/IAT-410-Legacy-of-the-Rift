using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bjorn_Idle : StateMachineBehaviour
{
    public float minAttackTime;
    public float maxAttackTime;

    float attackTimer = 0;

    string[] attackTriggers = { "basic_attack", "homing_attack", "asteroid_attack" };

    Enemy enemy;


    void TriggerRandomAttack(Animator animator)
    {
        System.Random rnd = new System.Random();
        int triggerInList = rnd.Next(attackTriggers.Length);
        string attackTrigger = attackTriggers[triggerInList];
        animator.SetTrigger(attackTrigger);
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.LookAtPlayer();

        if (attackTimer <= 0 && animator.GetComponent<BjornAttacks>().bjorn == BjornAttacks.BjornState.APPEARING)
        {
            TriggerRandomAttack(animator);
            attackTimer = Random.Range(minAttackTime, maxAttackTime);
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }

        // Increase Bjorn attack speed
        if (enemy.currentHealth <= 300)
        {
            animator.SetBool("malakai_unleashed", true);
            Debug.Log("Malakai is unleashed!");
        }

    }
}

