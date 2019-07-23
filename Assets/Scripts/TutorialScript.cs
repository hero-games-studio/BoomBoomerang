using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TutorialScript : MonoBehaviour
{
    public GameObject holdTutorial;
    public GameObject releaseTutorial;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isReleased = false;
            Invoke("Invokable", 0.2f);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isReleased = true;
        }

        else if(!Input.GetMouseButton(0))
        {
            disableHoldTutorial();
            disableReleaseTutorial();
        }
    }

    private bool isReleased;

    private void Invokable()
    {
        if (!isReleased)
        {
            holdTutorial.SetActive(true);
            Invoke("doubleInvokable",1f);
        }
    }

    private void doubleInvokable()
    {
        if (!isReleased)
        {
            releaseTutorial.SetActive(true);
            isTutorialFinished = true;
        }
    }

    private bool isTutorialFinished = false;

    private void disableHoldTutorial()
    {
        if (!isTutorialFinished)
        {
            holdTutorial.SetActive(false);
        }
    }

    private void disableReleaseTutorial()
    {
        if (!isTutorialFinished)
        {
            releaseTutorial.SetActive(false);
        }
    }
}
