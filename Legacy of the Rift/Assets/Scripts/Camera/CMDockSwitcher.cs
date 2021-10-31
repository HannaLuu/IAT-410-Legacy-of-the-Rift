using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMDockSwitcher : MonoBehaviour
{
    public Animator animator;

    public bool phase1, phase2;

    public GameObject waveManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        phase1 = true;
    }

    private void Update()
    {
        if (phase1)
        {
            animator.Play("Phase 1");
            waveManager.SetActive(false);
        }
    }

    // Update is called once per frame
    public void SwitchCamera()
    {
        if (phase1)
        {
            animator.Play("Phase 1");
            waveManager.SetActive(false);
        }
        if (phase2)
        {
            animator.Play("Phase 2");
            waveManager.SetActive(true);
        }
    }
}
