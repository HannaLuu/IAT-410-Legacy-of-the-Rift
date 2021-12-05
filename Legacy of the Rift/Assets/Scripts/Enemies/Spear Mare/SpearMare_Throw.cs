using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearMare_Throw : MonoBehaviour
{
    public Rigidbody2D rb;

    public int attackDamage = 10;

    public Transform spearPoint;

    public GameObject spearPrefab;

    AudioSource source;
    public AudioClip spearThrowSound;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySpearThrowSound()
    {
        source.clip = spearThrowSound;
        source.Play();
    }

    public void Spear()
    {
        Instantiate(spearPrefab, spearPoint.position, spearPoint.rotation);
    }
}
