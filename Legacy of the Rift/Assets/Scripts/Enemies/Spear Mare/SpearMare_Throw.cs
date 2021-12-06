using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearMare_Throw : MonoBehaviour
{
    public Rigidbody2D rb;

    public int attackDamage = 10;

    public Transform spearPoint;

    public GameObject spearPrefab;

    public void Spear()
    {
        Instantiate(spearPrefab, spearPoint.position, spearPoint.rotation);
    }
}
