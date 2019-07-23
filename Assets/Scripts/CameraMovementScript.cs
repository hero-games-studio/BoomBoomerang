using System.Collections;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private float cameraRotationSpeedMultiplier = 0.2f;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private GameObject PressIndicator;
    public static bool isTapped = false;
    private float angleSpace = 60;
    private float minAngle, maxAngle;
    private RaycastHit hit;
    private Vector3 firstDownPosition;

    private Vector3 initialForward;
    public float pullingDivider = 7.5f;
    public float minimumAxisMultiplier = 0.25f;
    public float maximumAxisMultiplier = 2.5f;

    void Start()
    {
        float startrot = playerObject.transform.eulerAngles.y;
        minAngle = startrot - angleSpace / 2;
        maxAngle = startrot + angleSpace / 2f;
        initialForward = playerObject.transform.forward;
    }
    void Update()
    {
        touchProcess();
    }



    //Get mouse position for player rotation
    private IEnumerator mouseSwirl()
    {
        PressIndicator.transform.position = getRaycastWorldPos();
        while (true)
        {


            Vector3 subtraction = hit.point - firstDownPosition;
            float angle = Vector3.SignedAngle(subtraction, initialForward, Vector3.up);
            playerObject.transform.eulerAngles = new Vector3(0, 90 - angle, 0);


            if (GameManagerScript.hasWeapon)
            {
                float deltaPosition = Vector3.Magnitude(firstDownPosition - getRaycastWorldPos());
                GameManagerScript.axisMultiplier = Mathf.Clamp(deltaPosition / 10f, 0.25f, 2.5f);
            }
            yield return null;
        }
    }

    //Get raycast point in the world space for trajectory pointing player rotating and throwing boomerang
    private Vector3 getRaycastWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask);
        return hit.point;
    }

    //Mouse input controls for using in unity for test purpose
    private void mouseProcess()
    {
        //Mouse pressed
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManagerScript.hasWeapon)
            {
                GameManagerScript.triggerPulling();
                firstDownPosition = getRaycastWorldPos();
                GameManagerScript.setShowTrajectory(true);
            }
            StartCoroutine("mouseSwirl");
        }
        //Mouse released
        else if (Input.GetMouseButtonUp(0))
        {
            if (GameManagerScript.hasWeapon)
            {
                //Trigger throw animation from game manager
                GameManagerScript.triggerThrowing();
                GameManagerScript.setShowTrajectory(false);
                PressIndicator.transform.position = new Vector3(-100, 100, -100);
            }
            StopCoroutine("mouseSwirl");
        }
    }

    //Touch controls
    public bool wasTouched;
    private void touchProcess()
    {
        if (!GameManagerScript.isLevelFinished)
        {
            if (Input.touchCount > 0)
            {
                wasTouched = true;
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && GameManagerScript.hasWeapon)
                {
                    PressIndicator.transform.position = getRaycastWorldPos();
                    GameManagerScript.triggerPulling();
                    firstDownPosition = getRaycastWorldPos();
                    GameManagerScript.setShowTrajectory(true);
                }
                Vector3 subtraction = hit.point - firstDownPosition;
                float angle = Vector3.SignedAngle(subtraction, initialForward, Vector3.up);
                playerObject.transform.eulerAngles = new Vector3(0, 90 - angle, 0);

                if (GameManagerScript.hasWeapon)
                {
                    float deltaPosition = Vector3.Magnitude(firstDownPosition - getRaycastWorldPos());
                    GameManagerScript.axisMultiplier = Mathf.Clamp(deltaPosition / pullingDivider, minimumAxisMultiplier, maximumAxisMultiplier);
                }
            }
            else if (wasTouched && GameManagerScript.hasWeapon)
            {
                wasTouched = false;
                GameManagerScript.triggerThrowing();
                GameManagerScript.setShowTrajectory(false);
                PressIndicator.transform.position = new Vector3(-100, 100, -100);
            }
            else if (wasTouched)
            {
                wasTouched = false;
            }
        }
    }


    //Calculate 2d position difference for rotation
    private float calculateRotation(Vector2 beganPos, Vector2 currentPos)
    {
        float val = -(beganPos.x - currentPos.x);
        return val;
    }

    //Later use for limiting rotation of player object
    private Vector3 getLimitedRotation(Vector3 rotation)
    {
        Debug.Log(rotation);
        if (rotation.y < minAngle)
        {
            return new Vector3(0, minAngle, 0);
        }
        else if (rotation.y > maxAngle)
        {
            return new Vector3(0, maxAngle, 0);
        }
        else return rotation;
    }
}
