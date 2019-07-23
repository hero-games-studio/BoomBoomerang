using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
//using GameAnalyticsSDK;
public class SceneManagerScript : MonoBehaviour
{
    public GameObject levelFinished;
    public GameObject[] confettiSpawnPoints;

    [SerializeField]
    private GameObject confetti;

    private int totalTreeCount;
    private int cuttedTreeCount = 0;
    public float confettiScale = 4.5f;
    private int sceneCount;

    private void Awake()
    {
       // GameAnalytics.Initialize();
    }

    void Start()
    {
     //   GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, Application.version, SceneManager.GetActiveScene().buildIndex);
        GameManagerScript.isLevelFinished = false;
        sceneCount = SceneManager.sceneCountInBuildSettings;
        totalTreeCount = FindObjectsOfType<BaseObstacle>().Length;
    }
    private void loadNextScene()
    {
     //   GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Application.version, SceneManager.GetActiveScene().buildIndex);
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
        SceneManager.LoadScene(0);
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
    }

    private void invokableLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
