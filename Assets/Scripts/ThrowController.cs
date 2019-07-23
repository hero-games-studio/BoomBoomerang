using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ThrowController : MonoBehaviour
{

    private Animator animator;
    public WeaponScript weaponScript;
    public TrailRenderer trailRenderer;
    public Transform hand;
    private Vector3 origLocPos;
    private Vector3 origLocRot;

    void Start()
    {
        WeaponCatch();
        animator = gameObject.GetComponent<Animator>();
        weaponScript.transform.position = hand.position;
        weaponScript.transform.rotation = hand.rotation;
        origLocPos = weaponScript.transform.position;
        origLocRot = weaponScript.transform.localEulerAngles;
    }


    public void WeaponThrow()
    {
        GameManagerScript.enableTrailRenderer();
        weaponScript.transform.parent = null;
        GameManagerScript.hasWeapon = false;
        GameManagerScript.enableSpinning();
        weaponScript.moveWeapon();
    }

    public void WeaponCatch()
    {
        weaponScript.transform.parent = hand;
        GameManagerScript.disableSpinning();
        GameManagerScript.disableTrailRenderer();
        GameManagerScript.setShowTrajectory(false);
        weaponScript.transform.localEulerAngles = origLocRot;
        weaponScript.transform.localPosition = origLocPos;
        GameManagerScript.hasWeapon = true;
    }
    private void checkAnimator()
    {
        if (animator == null)
            animator = gameObject.GetComponent<Animator>();
    }
}
