using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class KhajiitTutorial : MonoBehaviour
{
    public Animator khajiitAnimator, cameraAnimator;

    private bool facingRight = true;

    private void Start()
    {
        khajiitAnimator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //if(cameraAnimator.GetCurrentAnimatorStateInfo(0).IsName("Phase 2"))
        //{

        //}
    }
}
