using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMTutorialSwitcher : MonoBehaviour
{
    public Animator khajiitAnimator, cameraAnimator;

    public bool phase1, phase2, phase3, phase4;

    public bool fuck = false;

    public float timeToWait = 6f;

    public GameObject Phase1Enemies, Phase2Enemies;

    // Start is called before the first frame update
    void Start()
    {
        cameraAnimator = GetComponent<Animator>();
        phase1 = true;
    }

    public void SwitchCamera()
    {
        if (phase1 && fuck == true)
        {
            cameraAnimator.Play("Phase 1");
            khajiitAnimator.SetBool("isRun", true);
            StartCoroutine(CameraTransitionToPhase1());
        }
        if (phase2)
        {
            cameraAnimator.Play("Phase 2");
            khajiitAnimator.SetBool("isRun", true);
            StartCoroutine(CameraTransitionToPhase2());
        }
        if (phase3)
        {
            cameraAnimator.Play("Phase 3");
            khajiitAnimator.SetBool("isRun", true);
            StartCoroutine(CameraTransitionToPhase3());
        }
        if (phase4)
        {
            cameraAnimator.Play("Phase 4");
            khajiitAnimator.SetBool("isRun", true);
            fuck = true;
            StartCoroutine(CameraTransitionToPhase4());
        }
    }

    public IEnumerator CameraTransitionToPhase1()
    {
        yield return new WaitForSeconds(timeToWait);
        khajiitAnimator.SetBool("isRun", false);
    }

    public IEnumerator CameraTransitionToPhase2()
    {
        yield return new WaitForSeconds(timeToWait);
        khajiitAnimator.SetBool("isRun", false);
        Phase1Enemies.SetActive(false);
        Phase2Enemies.SetActive(true);
    }
    public IEnumerator CameraTransitionToPhase3()
    {
        yield return new WaitForSeconds(timeToWait);
        khajiitAnimator.SetBool("isRun", false);
        Phase1Enemies.SetActive(true);
    }

    public IEnumerator CameraTransitionToPhase4()
    {
        yield return new WaitForSeconds(timeToWait);
        khajiitAnimator.SetBool("isRun", false);
    }
}
