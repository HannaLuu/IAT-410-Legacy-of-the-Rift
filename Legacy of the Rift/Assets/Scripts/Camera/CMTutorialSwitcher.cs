using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMTutorialSwitcher : MonoBehaviour
{
    public Animator animator;

    public bool phase1, phase2, phase3, phase4;

    public float timeToWait = 6f;

    public GameObject Phase1Enemies;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        phase1 = true;
    }

    public void SwitchCamera()
    {
        if (phase1)
        {
            animator.Play("Phase 1");
            StartCoroutine(CameraTransition());
        }
        if (phase2)
        {
            animator.Play("Phase 2");
        }
        if (phase3)
        {
            animator.Play("Phase 3");
        }
        if (phase4)
        {
            animator.Play("Phase 4");
        }
    }

    public IEnumerator CameraTransition()
    {
        yield return new WaitForSeconds(timeToWait);
        Phase1Enemies.SetActive(false);
    }
}
