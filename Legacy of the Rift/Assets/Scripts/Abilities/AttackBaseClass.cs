using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AttackBaseClass : MonoBehaviour
{
    // Attack Rate and Time
    public float attackRate;
    protected float nextAttackTime;

    // Ability Rate and Time
    public float abilityRate;
    protected float nextAbilityTime;

    // Ult Rate and Time
    public float ultRate;
    protected float nextUltTime;

    // Zeal Crap
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

}
