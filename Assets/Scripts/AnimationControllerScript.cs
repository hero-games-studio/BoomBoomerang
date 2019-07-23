using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerScript : MonoBehaviour
{
    public Animator playerAnimator;

    public void triggerPulling()
    {
        playerAnimator.ResetTrigger("throwTrigger");
        playerAnimator.ResetTrigger("idleTrigger");
        playerAnimator.SetTrigger("pullTrigger");
    }

    public void triggerThrow()
    {
        playerAnimator.ResetTrigger("pullTrigger");
        playerAnimator.ResetTrigger("idleTrigger");
        playerAnimator.SetTrigger("throwTrigger");
    }

    public void triggerIdle()
    {
        playerAnimator.ResetTrigger("throwTrigger");
        playerAnimator.ResetTrigger("pullTrigger");
        playerAnimator.SetTrigger("idleTrigger");
    }
}
