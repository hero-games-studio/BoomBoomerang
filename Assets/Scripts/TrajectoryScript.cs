using UnityEngine;

public class TrajectoryScript : MonoBehaviour
{
    public GameObject dot;
    public GameObject dotParent;
    public GameObject playerObject;
    public float maxDotAngle = 200;
    public int dotCount;
    private GameObject[] dots;
    public bool showTrajectoryActive = false;
    private Vector3 originalDotPos;
    private Quaternion throwStartRot;
    public Vector3 dotColor;

    void Start()
    {
        dots = new GameObject[dotCount];
        originalDotPos = new Vector3(-100, 100, -100);
        createDots(originalDotPos);
    }
    void Update()
    {
        if (showTrajectoryActive)
        {
            throwStartRot = playerObject.transform.rotation;
            showTrajectory();
        }
    }

    private void createDots(Vector3 pos)
    {
        for (float i = 0; i < dotCount; i++)
        {
            float alpha = 1 - (i / dotCount);
            dots[(int)i] = Instantiate(dot, pos, Quaternion.Euler(0, 0, 0), dotParent.transform);
            dots[(int)i].GetComponent<MeshRenderer>().material.SetVector("_alpha", new Vector4(dotColor.x, dotColor.y, dotColor.z, alpha));
        }
    }

    public void resetDots()
    {
        for (int i = 0; i < dotCount; i++)
        {
            dots[i].transform.position = originalDotPos;
        }
    }


    public void showTrajectory()
    {
        float axisMultiplier = GameManagerScript.axisMultiplier;

        float incrementationValue = maxDotAngle / dotCount;

        for (int i = 0; i < dotCount; i++)
        {
            Vector3 result = calcPos(i * incrementationValue) - new Vector3(10, 0, 0) * axisMultiplier;
            Quaternion rotatedVector = Quaternion.Euler(0, throwStartRot.eulerAngles.y + 90, 0);
            Vector3 direction = rotatedVector * result;
            dots[i].transform.position = direction+correctionVector;
        }
    }

    public Vector3 correctionVector;
    private Vector3 calcPos(float time)
    {
        time = time * Mathf.Deg2Rad;
        float zVal = Mathf.Sin(time);
        for (int i = 0; i < GameManagerScript.tearDropSineMultiplier; i++)
        {
            zVal *= Mathf.Sin(time / 2);
        }
        Vector3 output = new Vector3(Mathf.Cos(time) * GameManagerScript.axisMultiplier,
                             0.25f,
                             zVal * GameManagerScript.axisMultiplier / 2) * 10;
        return output + correctionVector;
    }
}