using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;
using UnityEngine.UI;
using TMPro;
public class SceneManagerScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text1;
    public GameObject levelFinished;
    public GameObject[] confettiSpawnPoints;

    [SerializeField]
    private GameObject confetti;

    private int totalObstacleCountInScene;
    private int destroyedObstacleCount = 0;
    public float confettiScale = 4.5f;
    private int sceneCount;
    private static int tryCount = 1;
    public static int boomerangThrowCounter
    {
        get
        {
            return tryCount;
        }
    }
    public static void incrementThrowCounter()
    {
        tryCount++;
    }

    private void Awake()
    {
        GameAnalytics.Initialize();
        totalObstacleCountInScene = FindObjectsOfType<BaseObstacle>().Length;
        Debug.Log(totalObstacleCountInScene);
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != sceneCount - 1)
        {
            text1.text = SceneManager.GetActiveScene().buildIndex.ToString();
        }
        else
        {
            text1.text = SceneManager.GetActiveScene().buildIndex.ToString();
        }
        SaveSystem.save(SceneManager.GetActiveScene().buildIndex);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, Application.version, SceneManager.GetActiveScene().buildIndex);
        GameManagerScript.isLevelFinished = false;
        sceneCount = SceneManager.sceneCountInBuildSettings;
        if (totalObstacleCountInScene == 0)
        {
            totalObstacleCountInScene = FindObjectsOfType<BaseObstacle>().Length;
        }
    }
    private void loadNextScene()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Application.version, SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex != sceneCount - 1)
        {
            loadProcess();
            Invoke("invokableLoad", 2.5f);
        }
        else
        {
            loadProcess();
            Invoke("InvokableReload", 2.5f);
        }
    }

    private void loadProcess()
    {
        foreach (GameObject temp in confettiSpawnPoints)
        {
            GameObject newConfetti = Instantiate(confetti, temp.transform.position, Quaternion.Euler(-90, 0, 0), gameObject.transform);
            newConfetti.transform.localScale = new Vector3(confettiScale, confettiScale, confettiScale);
        }
        levelFinished.SetActive(true);
        GameManagerScript.isLevelFinished = true;
    }

    private void InvokableReload()
    {
        SceneManager.LoadScene(1);
    }

    public void checkSceneLoadCondition()
    {
        if (destroyedObstacleCount == totalObstacleCountInScene)
        {
            loadNextScene();
        }
    }

    public void incrementDestroyedObstacle()
    {
        destroyedObstacleCount++;
    }

    private void invokableLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
