using UnityEngine;
public class GameManagerScript : MonoBehaviour
{
    //Non static variables
    [SerializeField]
    private SceneManagerScript sceneManager;
    private CameraMovementScript cameraController;
    [SerializeField]
    private AnimationControllerScript animControllerNonStatic;
    [SerializeField]
    private ThrowController throwController;
    private RectTransform panelTransform;
    [SerializeField]
    private int tearDropSineMultiplierNonStatic;
    [SerializeField]
    private TrajectoryScript trajectoryNonStatic;

    [SerializeField]
    private WeaponScript weaponScriptNonStatic;
    [SerializeField]
    private TrailRenderer trailRenderer;

    //Camera controller script as static
    public static CameraMovementScript cameraMovementScript;

    //Variables for Throwing and trajectory displaying with static property for static access
    public static ThrowController throwControllerScript;
    public static int tearDropSineMultiplier = 3;
    private static TrajectoryScript trajectory;
    public static float axisMultiplier = 1;
    public static bool hasWeapon;
    public static bool isLevelFinished = false;
    public static WeaponScript weaponScript;
    public static TrailRenderer weaponTrailRenderer;
    public static AnimationControllerScript animController;


    //Variables for scene manager for static access
    public static SceneManagerScript sceneManagerScript;
    private static RectTransform levelFinishedPanel;

    private bool isScreenScaled = false;
    void Start()
    {
        if (!isScreenScaled)
        {
            Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow, 0);
            isScreenScaled = true;
        }
        weaponTrailRenderer = trailRenderer;
        weaponScript = weaponScriptNonStatic;
        Application.targetFrameRate = 60;
        levelFinishedPanel = panelTransform;
        sceneManagerScript = sceneManager;
        throwControllerScript = throwController;
        cameraMovementScript = cameraController;
        trajectory = trajectoryNonStatic;
        tearDropSineMultiplier = tearDropSineMultiplierNonStatic;
        animController = animControllerNonStatic;
    }
    public static void setShowTrajectory(bool set)
    {
        if (trajectory == null)
        {
            trajectory = FindObjectOfType<TrajectoryScript>();
        }
        trajectory.showTrajectoryActive = set;
        if (!set)
        {
            trajectory.resetDots();
        }
    }

    public static void triggerPulling()
    {
        animController.triggerPulling();
    }


    public static void triggerThrowing()
    {
        animController.triggerThrow();
    }
    public static void triggerIdle()
    {
        animController.triggerIdle();
    }

    public static void obstacleDestroyed()
    {
        sceneManagerScript.addTreeCut();
        sceneManagerScript.checkSceneLoadCondition();
    }

    public static void enableSpinning()
    {
        weaponScript.enableSpinning();
    }

    public static void disableSpinning()
    {
        if (weaponScript == null)
        {
            weaponScript = FindObjectOfType<WeaponScript>();
        }
        weaponScript.disableSpinning();
    }
    public static void catchWeapon()
    {
        throwControllerScript.WeaponCatch();
    }
    public static void enableTrailRenderer()
    {
        if (weaponTrailRenderer == null)
        {
            weaponTrailRenderer = FindObjectOfType<TrailRenderer>();
        }
        weaponTrailRenderer.enabled = true;
    }

    public static void disableTrailRenderer()
    {
        if (weaponTrailRenderer == null)
        {
            weaponTrailRenderer = FindObjectOfType<TrailRenderer>();
        }
        weaponTrailRenderer.enabled = false;
    }
}
