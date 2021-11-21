using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bjorn_Idle : StateMachineBehaviour
{
    public float minAttackTime;
    public float maxAttackTime;

    float attackTimer = 0;

    string[] attackTriggers = { "basic_attack", "homing_attack" };


    void TriggerRandomAttack(Animator animator)
    {
        System.Random rnd = new System.Random();
        int triggerInList = rnd.Next(attackTriggers.Length);
        string attackTrigger = attackTriggers[triggerInList];
        animator.SetTrigger(attackTrigger);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackTimer <= 0)
        {
            TriggerRandomAttack(animator);
            attackTimer = Random.Range(minAttackTime, maxAttackTime);
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }

    }
}

