using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBaseClass : MonoBehaviour
{
    // Attack Rate and Time
    public bool isAttackReady;
    public float attackCooldown;

    // Ability Rate and Time
    public bool isAbilityReady;
    public float abilityCooldown;

    // Ult Rate and Time
    public bool isUltReady;
    public float ultCooldown;

    // Zeal Crapshitstuff
    public float zealCost;

    // Animator
    public Animator animator;

    // Attack Transforms
    public Transform attackPoint;
    public Transform abilityPoint;
    public Transform ultPoint;

    // Attack Prefabs
    public GameObject attackPrefab;
    public GameObject abilityPrefab;
    public GameObject ultPrefab;

    // Abstract Methods
    public abstract void Attack();

    public abstract void ActivateAbility();

    public abstract void ActivateUlt();
    
    protected IEnumerator BasicCooldown() {
        isAttackReady = false;
        yield return new WaitForSeconds(attackCooldown);
        isAttackReady = true;
    }
    
    protected IEnumerator AbilityCooldown() {
        isAbilityReady = false;
        yield return new WaitForSeconds(abilityCooldown);
        isAbilityReady = true;
    }
    
    protected IEnumerator UltCooldown() {
        isUltReady = false;
        yield return new WaitForSeconds(ultCooldown);
        isUltReady = true;
    }

}
