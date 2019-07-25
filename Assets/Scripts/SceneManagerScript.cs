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
    [SerializeField]
    private TMP_Text text2;
    [SerializeField]
    private Slider slider;

    public GameObject levelFinished;
    public GameObject[] confettiSpawnPoints;

    [SerializeField]
    private GameObject confetti;

    private int totalTreeCount;
    private int cuttedTreeCount = 0;
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
        totalTreeCount = FindObjectsOfType<BaseObstacle>().Length;
        Debug.Log(totalTreeCount);
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != sceneCount - 1)
        {
            text1.text = SceneManager.GetActiveScene().buildIndex.ToString();
            text2.text = (SceneManager.GetActiveScene().buildIndex + 1).ToString();
        }
        else
        {
            //asdfasdf

            text1.text = SceneManager.GetActiveScene().buildIndex.ToString();
            text2.text = "0";

        }
        SaveSystem.save(SceneManager.GetActiveScene().buildIndex);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, Application.version, SceneManager.GetActiveScene().buildIndex);
        GameManagerScript.isLevelFinished = false;
        sceneCount = SceneManager.sceneCountInBuildSettings;
        if (totalTreeCount == 0)
        {
            totalTreeCount = FindObjectsOfType<BaseObstacle>().Length;
        }
        slider.value = cuttedTreeCount / totalTreeCount;
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
        if (cuttedTreeCount == totalTreeCount)
        {
            loadNextScene();
        }
    }

    public void addTreeCut()
    {
        cuttedTreeCount++;
        slider.value = (float)cuttedTreeCount / (float)totalTreeCount;
    }

    private void invokableLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void saveLevelBuildIndex()
    {

    }
}
