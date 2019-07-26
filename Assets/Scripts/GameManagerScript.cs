using UnityEngine;
public class GameManagerScript : MonoBehaviour
{
    #region non-static
    //Non static variables
    [SerializeField]
    private SceneManagerScript sceneManagerNonStatic;
    private CameraMovementScript cameraMovementScriptNonStatic;
    [SerializeField]
    private AnimationControllerScript animControllerNonStatic;
    [SerializeField]
    private ThrowController throwControllerNonStatic;
    [SerializeField]
    private int tearDropSineMultiplierNonStatic;
    [SerializeField]
    private TrajectoryScript trajectoryNonStatic;

    [SerializeField]
    private WeaponScript weaponScriptNonStatic;
    [SerializeField]
    private TrailRenderer weaponTrailRendererNonStatic;
    #endregion
    #region static 
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
    #endregion
    void Start()
    {
        //Screen scaling removed if there is a bug remember to add it back
        weaponTrailRenderer = weaponTrailRendererNonStatic;
        weaponScript = weaponScriptNonStatic;
        Application.targetFrameRate = 60;
        sceneManagerScript = sceneManagerNonStatic;
        throwControllerScript = throwControllerNonStatic;
        cameraMovementScript = cameraMovementScriptNonStatic;
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
        sceneManagerScript.incrementDestroyedObstacle();
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
