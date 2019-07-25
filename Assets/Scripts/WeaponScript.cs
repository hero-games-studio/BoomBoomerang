using System.Collections;
using UnityEngine;
using DG.Tweening;

public class WeaponScript : MonoBehaviour
{
    public float rotationSpeed;
    private float passedTimeTillThrow = 360f;
    public float timeMultiplier = 750;
    public float extraTimeMultiplier = 400;
    private Quaternion throwStartRot;
    public GameObject playerObject;
    private GameObject parent;
    private CapsuleCollider capsuleCollider;
    private IEnumerator spinningCoroutine;
    private float originalTimeMultiplier;
    public static bool isCrashed = false;
    private Vector3 startPos;
    private Vector3 startRot;

    void Start()
    {
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        parent = transform.parent.gameObject;
        originalTimeMultiplier = timeMultiplier;
        spinningCoroutine = spinBoomerang();
    }
    public void enableSpinning()
    {
        if (spinningCoroutine != null)
        {
            StartCoroutine(spinningCoroutine);
        }
    }
    public void disableSpinning()
    {
        if (spinningCoroutine != null)
        {
            StopCoroutine(spinningCoroutine);
        }
    }
    IEnumerator spinBoomerang()
    {
        while (true)
        {
            transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    public void moveWeapon()
    {
        passedTimeTillThrow = 360f;
        startPos = gameObject.transform.position;
        startRot = gameObject.transform.rotation.eulerAngles;
        calculateTimeMultiplier();
        StartCoroutine(incrementWeaponPos());
    }

    private void calculateTimeMultiplier()
    {
        float maxVal = CameraMovementScript.maximumAxisMultiplier - CameraMovementScript.minimumAxisMultiplier;
        float currentVal = 1 / GameManagerScript.axisMultiplier;
        float extraSpeed = currentVal * extraTimeMultiplier;
        timeMultiplier += extraSpeed;
    }

    private bool isMoving = false;

    void Update()
    {
        if (!isMoving)
        {
            transform.position = transform.parent.transform.position;
            transform.rotation = transform.parent.transform.rotation;
        }
    }


    IEnumerator incrementWeaponPos()
    {
        isMoving = true;
        GameManagerScript.setShowTrajectory(false);
        throwStartRot = playerObject.transform.rotation;
        bool isTriggered = false;
        while (true)
        {
            if (isCrashed)
            {
                returnFromCrash();
                timeMultiplier = originalTimeMultiplier;
                isCrashed = false;

                yield break;
            }
            if (passedTimeTillThrow <= 60 && !isTriggered)
            {
                GameManagerScript.triggerIdle();
                isTriggered = true;
            }
            if (passedTimeTillThrow <= 0)
            {
                passedTimeTillThrow = 360f;
                timeMultiplier = originalTimeMultiplier;
                disableSpinning();
                GameManagerScript.catchWeapon();
                GameManagerScript.setShowTrajectory(false);
                isMoving = false;
                yield break;
            }
            else
            {
                passedTimeTillThrow -= 0.005f * timeMultiplier;
                gameObject.transform.position = getPos(passedTimeTillThrow);
                yield return new WaitForSecondsRealtime(0.005f);
            }
        }
    }

    public void returnFromCrash()
    {
        transform.DOMove(parent.transform.position, 1.5f);
        capsuleCollider.enabled = false;
        Invoke("invokeCrashFinish", 1.5f);
    }

    private void invokeCrashFinish()
    {
        GameManagerScript.triggerIdle();
        GameManagerScript.catchWeapon();
        isMoving = false;
        capsuleCollider.enabled = true;
    }
    private Vector3 getPos(float time)
    {
        Vector3 result = calcPos(time);

        result += new Vector3((-10) * GameManagerScript.axisMultiplier, 0, 0);
        Quaternion rotatedVector = Quaternion.Euler(0, throwStartRot.eulerAngles.y + 90, 0);
        Vector3 direction = rotatedVector * result;

        return direction;
    }
    private Vector3 calcPos(float time)
    {
        time = time * Mathf.Deg2Rad;
        float zVal;
        zVal = Mathf.Sin(time);
        for (int i = 0; i < GameManagerScript.tearDropSineMultiplier; i++)
        {
            zVal *= Mathf.Sin(time / 2);
        }
        return new Vector3(Mathf.Cos(time) * GameManagerScript.axisMultiplier, 0.15f, zVal * GameManagerScript.axisMultiplier / 2) * 10;
    }
}
