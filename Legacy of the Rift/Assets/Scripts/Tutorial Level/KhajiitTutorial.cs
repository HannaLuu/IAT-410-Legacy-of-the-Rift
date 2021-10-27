using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class KhajiitTutorial : MonoBehaviour
{
    public Animator animator;

    private bool facingRight = true;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            if (facingRight == false)
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = true;
            }
            animator.SetBool("isRun", true);
        } else if (Input.GetKey(KeyCode.A))
        {
            if (facingRight == true)
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = false;
            }
            animator.SetBool("isRun", true);
        } else
        {
            animator.SetBool("isRun", false);
        }
    }
}
