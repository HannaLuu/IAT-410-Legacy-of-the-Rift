using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBaseClass : MonoBehaviour
{
    //Tutorial Boolean
    public bool enemyCollided;

    // Attack Rate, Time, Overzealous Regen Amount
    public bool isAttackReady;
    public bool attackActivated;
    public float maxAttackCooldown;
    public float currAttackCooldown;

    // Ability Rate and Time
    public bool isAbilityReady;
    public bool abilityActivated;
    public float maxAbilityCooldown;
    public float currAbilityCooldown;

    // Ult Rate, Time, Cost
    public bool isUltUnlocked;
    public bool isUltReady;
    public bool ultActivated;
    public float maxUltCooldown;
    public float currUltCooldown;
    public int ultZealCost;

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

    public IEnumerator BasicCooldown()
    {
        isAttackReady = false;
        currAttackCooldown = 0;
        while (currAttackCooldown < maxAttackCooldown)
        {
            currAttackCooldown += Time.deltaTime;
            if (currAttackCooldown >= maxAttackCooldown)
            {
                isAttackReady = true;
                currAttackCooldown = maxAttackCooldown;
            }
            yield return null;
        }
    }

    public IEnumerator AbilityCooldown()
    {
        isAbilityReady = false;
        currAbilityCooldown = 0;
        while (currAbilityCooldown < maxAbilityCooldown)
        {
            currAbilityCooldown += Time.deltaTime;
            if (currAbilityCooldown >= maxAbilityCooldown)
            {
                isAbilityReady = true;
                currAbilityCooldown = maxAbilityCooldown;
            }
            yield return null;
        }
    }

    public IEnumerator UltCooldown()
    {
        isUltReady = false;
        currUltCooldown = 0;
        while (currUltCooldown < maxUltCooldown)
        {
            currUltCooldown += Time.deltaTime;
            if (currUltCooldown >= maxUltCooldown)
            {
                isUltReady = true;
                currUltCooldown = maxUltCooldown;
            }
            yield return null;
        }
    }

}
