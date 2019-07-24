using System.Collections;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float rotationSpeed;
    private float passedTimeTillThrow = 360f;
    public float timeMultiplier = 650;
    private Quaternion throwStartRot;
    public GameObject playerObject;

    private IEnumerator spinningCoroutine;

    private float originalTimeMultiplier;

    void Start()
    {
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

    private Vector3 startPos;
    private Vector3 startRot;
    public void moveWeapon()
    {
        passedTimeTillThrow = 360f;
        startPos = gameObject.transform.position;
        startRot = gameObject.transform.rotation.eulerAngles;
        StartCoroutine(incrementWeaponPos());
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

            if (passedTimeTillThrow <= 60 && !isTriggered)
            {
                GameManagerScript.triggerIdle();
                isTriggered = true;
            }
            if (passedTimeTillThrow <= 0)
            {
                passedTimeTillThrow = 360f;
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

    public Vector3 correctionVector;
    private Vector3 getPos(float time)
    {
        Vector3 result = calcPos(time);

        result += new Vector3((-10) * GameManagerScript.axisMultiplier, 0, 0);
        Quaternion rotatedVector = Quaternion.Euler(0, throwStartRot.eulerAngles.y + 90, 0);
        Vector3 direction = rotatedVector * result;

        return direction + correctionVector;
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

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<TreeCutScript>() != null)
        {

        }
    }
}
