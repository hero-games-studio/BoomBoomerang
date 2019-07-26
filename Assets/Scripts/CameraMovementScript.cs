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
    private RaycastHit hit;
    private Vector3 firstDownPosition;

    private Vector3 initialForward;
    public bool wasTouched;
    public float pullingDivider = 7.5f;
    public static float minimumAxisMultiplier = 0.5f;
    public static float maximumAxisMultiplier = 4.5f;

    void Start()
    {
        float startrot = playerObject.transform.eulerAngles.y;
        initialForward = playerObject.transform.forward;
    }
    void Update()
    {
        /*
#if UNITY_EDITOR
        mouseProcess();
#endif
#if UNITY_ANDROID
        touchProcess();
#endif
#if UNITY_IPHONE
        touchProcess();
#endif
    */
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
}
